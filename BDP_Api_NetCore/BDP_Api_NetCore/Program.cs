using BDP.Application.Command.Request.Login;
using BDP.Infrastructure.Data;
using BDP.Infrastructure.Data.Interface;
using BDP.Infrastructure.Data.Query;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
builder.Configuration.AddJsonFile($"appsettings.Development.json", true, true);
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(GetAllRequestAssembly());
builder.Services.AddAutoMapper(typeof(Program).Assembly);
SetInfrastructureDependency(builder.Services);
ConfigureDatabase(builder.Services);
ConfigureJWT(builder.Services);
AddSwaggerDoc(builder.Services);
ConfigureCors(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseCors("MyPolicy");
app.Run();


Assembly[] GetAllRequestAssembly()
{
    Assembly[] assemblies = {
        typeof(LoginCommandRequest).GetTypeInfo().Assembly
    };
    return assemblies;
}

void SetInfrastructureDependency(IServiceCollection services)
{
    services.AddTransient<IUserRepository, UserRepository>();
    services.AddTransient<IGenericRepository, GenericRepository>();
    services.AddTransient<ITransactionRepository, TransactionRepository>();
}

void ConfigureDatabase(IServiceCollection services)
{
    var databaseSection = builder.Configuration.GetSection("Database");
    var connectionString = databaseSection.GetValue<string>("ConnectionString");
    services.AddDbContext<DatabaseContext>(x => x.UseSqlServer(connectionString));
}

void ConfigureJWT(IServiceCollection services)
{
    services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
        };

    });
}

void AddSwaggerDoc(IServiceCollection services)
{
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "BDP API", Version = "v1" });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme."
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
}

void ConfigureCors(IServiceCollection services)
{
    services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    }));
}

