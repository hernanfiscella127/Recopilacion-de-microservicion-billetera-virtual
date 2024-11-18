using Account.API.Infrastructure;
using AccountInfrastructure.Command;
using AccountInfrastructure.Query;
using Application.Interfaces.IAccountModel;
using Application.Interfaces.IAccountType;
using Application.Interfaces.IPersonalAccount;
using Application.Interfaces.IStateAccount;
using Application.Interfaces.ITypeCurrency;
using Application.Mappers.IMappers;
using Application.Mappers;
using Application.UseCases;
using Infrastructure.Command;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces.IHttpServices;
using Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Application.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Log.Logger = new LoggerConfiguration()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

Log.Logger.Information("Starting the Web API...");


// Configuracion de JWT
var secretKey = builder.Configuration.GetValue<string>("Jwt:Key");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidIssuer = builder.Configuration["Jwt:Issuer"], // Leer desde appsettings.json
        ValidAudience = builder.Configuration["Jwt:Audience"] // Leer desde appsettings.json
    };
});
builder.Services.AddAuthorization();

//config

builder.Services.AddHttpClient<IUserHttpService, UserHttpService>(client =>
{
    client.BaseAddress = new Uri("https://localhost");
});

builder.Services.AddHttpClient<ITransferHttpService, TransferHttpService>(client =>
{
    client.BaseAddress = new Uri("https://localhost");
});

var connectionString = builder.Configuration["ConnectionString"];
builder.Services.AddDbContext<AccountContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IGenericMapper, GenericMapper>();

builder.Services.AddScoped<IAccountCommand, AccountCommand>();
builder.Services.AddScoped<IAccountQuery, AccountQuery>();
builder.Services.AddScoped<IAccountServices, AccountServices>();

builder.Services.AddScoped<IPersonalAccountCommand, PersonalAccountCommand>();
builder.Services.AddScoped<IPersonalAccountQuery, PersonalAccountQuery>();
builder.Services.AddScoped<IPersonalAccountServices, PersonalAccountServices>();

builder.Services.AddScoped<IAccountTypeQuery, AccountTypeQuery>();
builder.Services.AddScoped<IAccountTypeServices, AccountTypeServices>();

builder.Services.AddScoped<IStateAccountQuery, StateAccountQuery>();
builder.Services.AddScoped<IStateAccountServices, StateAccountServices>();

builder.Services.AddScoped<ITypeCurrencyQuery, TypeCurrencyQuery>();
builder.Services.AddScoped<ITypeCurrencyServices, TypeCurrencyServices>();

builder.Services.AddScoped<IJwtService, JwtService>(provider =>
{
    var config = provider.GetRequiredService<IConfiguration>();
    var key = config.GetValue<string>("Jwt:Key");
    var accessTokenExpirationMinutes = config.GetValue<int>("Jwt:AccessTokenExpirationMinutes");
    var refreshTokenExpirationDays = config.GetValue<int>("Jwt:RefreshTokenExpirationDays");

    return new JwtService(key, accessTokenExpirationMinutes, refreshTokenExpirationDays);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});



var app = builder.Build();

Log.Logger.Information("Web API is configured and ready to handle requests.");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowAll");
app.Run();
