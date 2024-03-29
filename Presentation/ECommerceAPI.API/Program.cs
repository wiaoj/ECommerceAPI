using ECommerceAPI.Application;
using ECommerceAPI.Application.Validators.Products;
using ECommerceAPI.Infrastructure;
using ECommerceAPI.Infrastructure.Filters;
using ECommerceAPI.Infrastructure.Services.Storage.Local;
using ECommerceAPI.Persistence;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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

builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
    .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())

    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true) //default asp.net filterini kapatıyor (normalde controllera gelmeden filtreyi devreye sokup işlemi yapıp cliente geri döndürüyor)
    ;


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin", options => {
        options.TokenValidationParameters = new() {
            ValidateAudience = true, // Oluşturulacak token değerlerini kimlerin/hangi originlerin/sitelerin kullanıcı belirlediğimiz değer.  --> www.merhaba.com
            ValidateIssuer = true, // Oluşturulacak token değerini kimin dağıttığını ifade edeceğimiz alandır. --> www.myapi.com
            ValidateLifetime = true, // Oluşturulacak token değerinin süresini kontrol edecek doğrulamadır
            ValidateIssuerSigningKey = true, // Üretilecek token değerlerinin uygulamamıza ait bir değer olduğunu ifade eden security key verisinin doğrulanmasıdır.

            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"]))
        };
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication(); // Authentication ekleniyor.
app.UseAuthorization();

app.MapControllers();

app.Run();
