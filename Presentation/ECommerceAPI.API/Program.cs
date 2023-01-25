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
    //her �n�ne gelen girebilir... policy.AllowAnyHeader().AllowAnyHeader().AllowAnyOrigin()
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
    .Enrich.FromLogContext() //�zel olarak olu�turulan propertyleri yakal�yor
    .MinimumLevel.Information()
    .CreateLogger();

//Serilog.Sinks.Seq
//docker run --name Seq -e ACCEPT_EULA=Y -p 5341:80 datalust/seq:latest

builder.Host.UseSerilog(log);

builder.Services.AddHttpLogging(logging => {

    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua"); // kullan�c�ya dair t�m bilgileri getiriyor
    //logging.ResponseHeaders.Add("");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});

builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
    .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())

    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true) //default asp.net filterini kapat�yor (normalde controllera gelmeden filtreyi devreye sokup i�lemi yap�p cliente geri d�nd�r�yor)
    ;


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin", options => {
        options.TokenValidationParameters = new() {
            ValidateAudience = true, // Olu�turulacak token de�erlerini kimlerin/hangi originlerin/sitelerin kullan�c� belirledi�imiz de�er.  --> www.merhaba.com
            ValidateIssuer = true, // Olu�turulacak token de�erini kimin da��tt���n� ifade edece�imiz aland�r. --> www.myapi.com
            ValidateLifetime = true, // Olu�turulacak token de�erinin s�resini kontrol edecek do�rulamad�r
            ValidateIssuerSigningKey = true, // �retilecek token de�erlerinin uygulamam�za ait bir de�er oldu�unu ifade eden security key verisinin do�rulanmas�d�r.

            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
            LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires is not null && expires > DateTime.UtcNow,
            NameClaimType = ClaimTypes.Name, //JWT �zerinde name claimine kar��l�k gelen de�eri  User.Identity.Name propertysinden elde edebiliyoruz.
        };
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseStaticFiles();

app.UseSerilogRequestLogging(); //Serilog middleware, kendisinden �nceki middlewareler loglanmaz

app.UseHttpLogging(); // yukar�daki AddHttpLogging() ile istekleri logluyoruz.
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
