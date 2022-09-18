using ECommerceAPI.Application;
using ECommerceAPI.Application.Validators.Products;
using ECommerceAPI.Infrastructure;
using ECommerceAPI.Infrastructure.Filters;
using ECommerceAPI.Infrastructure.Services.Storage.Local;
using ECommerceAPI.Persistence;
using FluentValidation.AspNetCore;

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

	.ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true) //default asp.net filterini kapatýyor (normalde controllera gelmeden filtreyi devreye sokup iþlemi yapýp cliente geri döndürüyor)
	;


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment()) {
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
