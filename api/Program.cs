using System.Reflection;
using System.Text.Json.Serialization;
using Gainwell.Controllers.Dapper;
using Gainwell.Data;
using Gainwell.Repositories.Dapper;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using Gainwell.Services;
using Gainwell.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add database contexts
builder.Services.AddSingleton<DapperDbContext>();

// Add repositories
var IRepositoryType = typeof(IRepository);
var repositoryTypes = AppDomain.CurrentDomain.GetAssemblies()
    .SelectMany(assembly => assembly.GetTypes())
    .Where(type => type.IsClass && !type.IsAbstract && IRepositoryType.IsAssignableFrom(type));

foreach (var repositoryType in repositoryTypes) {
    var interfaceType = repositoryType.GetInterfaces().FirstOrDefault(i => i != typeof(IRepository) && typeof(IRepository).IsAssignableFrom(i));
    if (interfaceType != null) builder.Services.AddScoped(interfaceType, repositoryType);
    else builder.Services.AddScoped(repositoryType);
}

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull; });

builder.Services.AddSingleton<AuthenticationService>();
builder.Services.AddSingleton<UserValidationService>();
builder.Services.AddScoped<UserJwtClaimsFilter>();

// Scan and register all repository implementations
builder.Services.Scan(scan => scan
    .FromAssemblies(Assembly.GetExecutingAssembly())
    .AddClasses(classes => classes.AssignableTo(typeof(IRepository<>)))
    .AsImplementedInterfaces()
    .WithScopedLifetime());

// Configure CORS
builder.Services.AddCors(options => {
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:3000")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

// Add Swagger generator
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Gainwell PHM API", Version = "v1" });
    c.EnableAnnotations();
    c.OperationFilter<SummaryOperationFilter>();

    // Configure Swagger to use the JWT token authentication
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        //Type = SecuritySchemeType.Http,
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    }); 
});

// Configure JWT authentication
var secretKey = builder.Configuration["Jwt:SecretKey"];
if (string.IsNullOrEmpty(secretKey)) throw new InvalidOperationException("JWT secret key is not configured");
byte[] key = Encoding.ASCII.GetBytes(secretKey);

// Load dev certificate
var certPath = Path.Combine(builder.Environment.ContentRootPath, "certificates/certificate.p12");
var certPassword = "welcome01";
var certificate = new X509Certificate2(certPath, certPassword);

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
        //IssuerSigningKey = new X509SecurityKey(certificate)
    };
});

// Configure Kestrel for HTTPS
builder.WebHost.ConfigureKestrel(options => {
    options.ListenLocalhost(5188);
    options.ListenLocalhost(7126, listenOptions => { listenOptions.UseHttps(); });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gainwell PHM API v1"));
}

// Use CORS
app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
