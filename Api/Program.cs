using ApiPloomes.Context;
using ApiPloomes.Repositorios.Interface;
using ApiPloomes.Repositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Api.Services.Interface;
using Api.Services;
using Microsoft.OpenApi.Models;
using Api.Validacoes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "HealthWeb", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = @"JWT Authorization header using the Bearer scheme. Enter 'Bearer'[space] and then your token.",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                              new OpenApiSecurityScheme
                              {
                                  Reference = new OpenApiReference
                                  {
                                      Type = ReferenceType.SecurityScheme,
                                      Id = "Bearer"
                                  }
                              },
                             new string[] {}
                        }
                    });
});

builder.Services.AddEntityFrameworkSqlServer()
    .AddDbContext<HealthWebDbContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase"))
    );

builder.Services.AddScoped<IEmpresaRepositorio, EmpresaRepositorio>();
builder.Services.AddScoped<IOperadoraRepositorio, OperadoraRepositorio>();
builder.Services.AddScoped<IMedicoRepositorio, MedicoRepositorio>();
builder.Services.AddScoped<IOperadoraMedicoRepositorio, OperadoraMedicoRepositorio>();
builder.Services.AddScoped<IAutenticadorService, AutenticadorService>();
builder.Services.AddScoped<IValidacoes, Validacoes>();

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}
).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["JwtToken:Issuer"],
        ValidAudience = builder.Configuration["JwtToken:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JwtToken:SecretKey"])),
        ClockSkew = TimeSpan.Zero

    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
