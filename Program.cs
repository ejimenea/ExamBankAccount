using BankAccount.DB;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using System.Reflection;
using BankAccount.Manager;
using FluentValidation;
using BankAccount.Validations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddFluentValidation(a => {
        a.ImplicitlyValidateChildProperties = true;
        a.ImplicitlyValidateRootCollectionElements = true;
        a.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()); 
    });

//builder.Services.AddValidatorsFromAssemblyContaining<CustomerValidator>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AccountDBContext>(db => db.UseSqlite(builder.Configuration.GetConnectionString("BankDB_Connection")));

builder.Services.AddScoped<IBankManager, BankManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
