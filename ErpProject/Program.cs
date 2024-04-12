using ErpProject.Controllers;
using ErpProject.Interface;
using ErpProject.Profile;
using ErpProject.Repository;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
});

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IShippingRepository, ShippingRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthenticationHelper, AuthenticationHelper>();
builder.Services.AddScoped<PasswordHashService>();

builder.Services.AddAutoMapper(typeof(DtoProfile));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
