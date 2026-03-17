using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudentManagementSystem.Domain.Domain;
using StudentManagementSystem.Domain.Interface;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Add DBContext
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("OurConnectionString")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyReactApp", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.WithOrigins("*");
    });
});

//Add domain - Dependency Injection
builder.Services.AddScoped<SMSystemDomain>();

//Add repository - Dependency Injection
builder.Services.AddScoped<ISMSystemRepository, SMSystemRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string tokenKeyString = builder.Configuration.GetSection("AppSetting:TokenKey").Value;
SymmetricSecurityKey tokenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKeyString));
TokenValidationParameters validationParams = new TokenValidationParameters()
{
    IssuerSigningKey = tokenKey,
    ValidateIssuer = false,
    ValidateIssuerSigningKey = false,
    ValidateAudience = false
};

//Adding Athentication and overriding the OnChallange event
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = validationParams;

        options.Events = new JwtBearerEvents
        {
            OnChallenge = context =>
            {
                context.HandleResponse();
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";
                var result = System.Text.Json.JsonSerializer.Serialize(new
                {
                    message = "You are not authorized to access this resource."
                });
                return context.Response.WriteAsync(result);

            }
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("MyReactApp");

app.UseAuthentication();

//Adding custom middleware for 403 response 
app.Use(async (context, next) =>
{
    await next();

    if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
    {
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(new
        {
            message = "You don't have permission for this action."
        }));
    }
});

app.UseAuthorization();

app.MapControllers();

app.Run();
