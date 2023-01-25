using ECommerceAPI.API.Configurations.ColumnWriters;
using ECommerceAPI.Application;
using ECommerceAPI.Application.Validators.Products;
using ECommerceAPI.Infrastructure;
using ECommerceAPI.Infrastructure.Filters;
using ECommerceAPI.Infrastructure.Services.Storage.Local;
using ECommerceAPI.Persistence;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Sinks.PostgreSQL;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();

//builder.Services.AddStorage(StorageType.Local);
//builder.Services.AddStorage<LocalStorage>();

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy =>
    //her önüne gelen girebilir... policy.AllowAnyHeader().AllowAnyHeader().AllowAnyOrigin()
    policy.WithOrigins("http://localhost:4200", "https://localhost:4200")
            .AllowAnyHeader().AllowAnyMethod()
        )
    );

Logger log = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt")
    .WriteTo.PostgreSQL(builder.Configuration.GetConnectionString("PostgreSQL"),
                        "logs",
                        needAutoCreateTable: true,
                        columnOptions: new Dictionary<String, ColumnWriterBase> {
                            { "message", new RenderedMessageColumnWriter() },
                            { "message_template", new MessageTemplateColumnWriter() },
                            { "level", new LevelColumnWriter() },
                            { "time_stamp", new TimestampColumnWriter() },
                            { "exception", new ExceptionColumnWriter() },
                            { "log_event", new LogEventSerializedColumnWriter() },
                            { "user_name", new UsernameColumnWriter() },
                        })
    .WriteTo.Seq(builder.Configuration["Seq:ServerURL"]) //Serilog.Sinks.Seq
    .Enrich.FromLogContext() //özel olarak oluþturulan propertyleri yakalýyor
    .MinimumLevel.Information()
    .CreateLogger();

//Serilog.Sinks.Seq
//docker run --name Seq -e ACCEPT_EULA=Y -p 5341:80 datalust/seq:latest

builder.Host.UseSerilog(log);

builder.Services.AddHttpLogging(logging => {

    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua"); // kullanýcýya dair tüm bilgileri getiriyor
    //logging.ResponseHeaders.Add("");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});

builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
    .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())

    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true) //default asp.net filterini kapatýyor (normalde controllera gelmeden filtreyi devreye sokup iþlemi yapýp cliente geri döndürüyor)
    ;


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin", options => {
        options.TokenValidationParameters = new() {
            ValidateAudience = true, // Oluþturulacak token deðerlerini kimlerin/hangi originlerin/sitelerin kullanýcý belirlediðimiz deðer.  --> www.merhaba.com
            ValidateIssuer = true, // Oluþturulacak token deðerini kimin daðýttýðýný ifade edeceðimiz alandýr. --> www.myapi.com
            ValidateLifetime = true, // Oluþturulacak token deðerinin süresini kontrol edecek doðrulamadýr
            ValidateIssuerSigningKey = true, // Üretilecek token deðerlerinin uygulamamýza ait bir deðer olduðunu ifade eden security key verisinin doðrulanmasýdýr.

            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
            LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires is not null && expires > DateTime.UtcNow,
            NameClaimType = ClaimTypes.Name, //JWT üzerinde name claimine karþýlýk gelen deðeri  User.Identity.Name propertysinden elde edebiliyoruz.
        };
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseStaticFiles();

app.UseSerilogRequestLogging(); //Serilog middleware, kendisinden önceki middlewareler loglanmaz

app.UseHttpLogging(); // yukarýdaki AddHttpLogging() ile istekleri logluyoruz.
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication(); // Authentication ekleniyor.
app.UseAuthorization();

//Serilog harici property
app.Use(async (context, next) => {
    //var username = context.User?.Identity?.Name is not null ? context.User.Identity.Name : null;

    var username = context.User?.Identity?.Name is not null || true ? context.User.Identity.Name : null;
    LogContext.PushProperty("user_name", username);

    await next();
});

app.MapControllers();

app.Run();
