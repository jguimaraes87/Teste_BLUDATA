using Domain.Interfaces.Generics;
using Domain.Interfaces.IEmpresa;
using Domain.Interfaces.IFornecedor;
using Domain.Interfaces.InterfaceServices;
using Domain.Interfaces.ITelefones;
using Domain.Interfaces.IUsuarioEmpresa;
using Domain.Services;
using Entities.Entidades;
using Infra.Config;
using Infra.Repositorio;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebApi.Token;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//CONFIGURA플O DE STRING DE CONEXAO + SIGNIN
builder.Services.AddDbContext<ContextBase>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ContextBase>();


//SINGLETON INTERFACE + REPOSITORIO
builder.Services.AddSingleton(typeof(Domain.Interfaces.Generics.RepositoryGenerics<>), typeof(Infra.Repositorio.RepositoryGenerics<>));
builder.Services.AddSingleton<InterfaceEmpresa, RepositorioEmpresa>();
builder.Services.AddSingleton<InterfaceUsuarioEmpresa, RepositorioUsuarioEmpresa>();
builder.Services.AddSingleton<InterfaceFornecedor, RepositorioFornecedor>();
builder.Services.AddSingleton<InterfaceTelefones, RepositorioTelefones>();

//SINGLETON DE SERVICO E DOMINIO
builder.Services.AddSingleton<IEmpresaServices, EmpresaService>();
builder.Services.AddSingleton<IUsuarioEmpresaServices, UsuarioEmpresaService>();
builder.Services.AddSingleton<IFornecedorServices, FornecedorService>();
builder.Services.AddSingleton<ITelefonesServices, TelefoneService>();

//AUTENTICA플O POR TOKEN
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(option =>
             {
                 option.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = false,
                     ValidateAudience = false,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,

                     ValidIssuer = "Teste.Securiry.Bearer",
                     ValidAudience = "Teste.Securiry.Bearer",
                     IssuerSigningKey = JwtSecurityKey.Create("Secret_Key-12345678")
                 };

                 option.Events = new JwtBearerEvents
                 {
                     OnAuthenticationFailed = context =>
                     {
                         Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                         return Task.CompletedTask;
                     },
                     OnTokenValidated = context =>
                     {
                         Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                         return Task.CompletedTask;
                     }
                 };
             });


var app = builder.Build();


//EXECUTA AS MIGRA합ES AO INICIAR A APLICA플O

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ContextBase>();
    context.Database.Migrate(); // Aplica as migra寤es pendentes
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//CONFIGURA플O DO CORS
var devClient = "http://localhost:4200";

app.UseCors(x =>
x.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader()
.WithOrigins(devClient));


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run("http://localhost:5150");

