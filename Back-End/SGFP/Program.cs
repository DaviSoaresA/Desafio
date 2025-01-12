using System.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using SGFP.Domain.Interfaces;
using SGFP.Domain.Services;
using SGFP.Infrastructure.Data;
using SGFP.Infrastructure.Repositories;
using SGFP.Presentation.Controller;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços ao contêiner
// Configurar o DbContext com o banco de dados PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IDbConnection>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    return new NpgsqlConnection(connectionString);
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IFinanceRepository, FinanceRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<FinanceService>();

builder.Services.AddControllers();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

    
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllers();

app.Run();
