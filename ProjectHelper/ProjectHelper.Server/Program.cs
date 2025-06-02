using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProjectHelper.Server.Stores;
using ProjectHelper.Server.Servieces;
using System.Text;
using ProjectHelper.Data;
using ProjectHelper.Domain.Users;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDevServer",
        builder =>
        {
            builder
                .WithOrigins(
                    "http://localhost:50689",
                    "http://localhost:64248",
                    "http://localhost:64085"
                ) // Разрешаем запросы с Angular
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1"
    });
});

builder.Services.Configure<MongoDBSettingsModel>(builder.Configuration.GetSection("MongoDB"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
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

builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddSingleton<TokenStore>();
builder.Services.AddScoped<IProductManagerRepository,ProductManagerRepository>();
builder.Services.AddScoped<IDeveloperRepository,DeveloperRepository>();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("./v1/swagger.json", "MyServiceAPI"); });
}

app.UseCors("AllowAngularDevServer"); // Добавляем CORS middleware

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
