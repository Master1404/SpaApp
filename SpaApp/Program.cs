using ExpressMapper;
using SpaApp.Api.DTO;
using SpaApp.Domain.Entities;
using Serilog;
using FluentValidation;
using Serilog.Sinks.Grafana.Loki;
using System.Reflection;
using System.Text;
using Asp.Versioning;
using ExpressMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using SpaApp.Configuration;
using SpaApp;
using SpaApp.Api.Extensions;
using SpaApp.DTO;
using SpaApp.Application.Validators;

var builder = WebApplication.CreateBuilder(args);

// Read API versioning from appsettings.json
var apiVersioningConfig = builder.Configuration.GetSection("ApiVersioning");
int majorVersion = apiVersioningConfig.GetValue<int>("MajorVersion");
/*
// Configure Serilog
builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.GrafanaLoki(
        uri: context.Configuration["Serilog:WriteTo:1:Args:uri"],
        labels: new[] {
            new LokiLabel { Key = "app", Value = "sheliapp-webapi" },
            new LokiLabel { Key = "environment", Value = context.HostingEnvironment.EnvironmentName }
        },
        propertiesAsLabels: new[] { "level" }
    ));

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddSerilog(dispose: true);
});*/


// Register MongoDB class maps
MongoClassMapper.RegisterClassMaps();

// Load environment variables
builder.Configuration.AddEnvironmentVariables();

// Configure CORS to allow all origins (for testing purposes)
// You should restrict this in production
var allowedOrigins = builder.Configuration.GetSection("AllowedCorsOrigins").Get<string[]>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder => builder
            .WithOrigins(allowedOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod());

    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader());
});

// Configure ExpressMapper
Mapper.Register<User, UserResponseModel>();
Mapper.Register<Comment, CommentResponceModel>();

// Configure MongoSettings using environment variables
builder.Services.Configure<MongoSettings>(options =>
{
    options.ConnectionString = Environment.GetEnvironmentVariable("MONGO_CONNECTION_STRING") ?? builder.Configuration.GetSection("MongoSettings")["ConnectionString"] ?? string.Empty;
    options.DatabaseName = Environment.GetEnvironmentVariable("MONGO_DB") ?? builder.Configuration.GetSection("MongoSettings")["DatabaseName"] ?? string.Empty;
});

// Configure JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SpaApp API", Version = "v1" });

    // Configure Swagger to use JWT Authentication
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below. Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });
});

// Register FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<UserValidator>();


builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(majorVersion, 0);
});
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
// Register repos
builder.Services.AddRepositories();

// Register CQRS
builder.Services.AddCqrsHandlers();

// Configure MongoDB
builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});

builder.Services.AddScoped<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    var settings = sp.GetRequiredService<IOptions<MongoSettings>>().Value;
    return client.GetDatabase(settings.DatabaseName);
});

if (!builder.Environment.IsDevelopment())
{
    builder.Services.AddHttpsRedirection(options =>
    {
        options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
        options.HttpsPort = 8081;
    });
};

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", $"SpaApp API V{majorVersion}");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
// Use the CORS policy
var logger = LoggerFactory.Create(config =>
{
    config.AddConsole();
}).CreateLogger("Program");

if (app.Environment.IsDevelopment())
{
    logger.LogInformation("UseCors - AllowAll");
    app.UseCors("AllowAll");
}
else
{
    // logger.LogInformation("UseCors - AllowSpecificOrigins");
    // app.UseCors("AllowSpecificOrigins");
    logger.LogInformation("UseCors - AllowAll");
    app.UseCors("AllowAll");
}

app.MapControllers();
app.Run();
