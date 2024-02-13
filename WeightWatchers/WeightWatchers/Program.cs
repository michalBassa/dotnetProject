using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Subscriber.Core;
using Subscriber.Data;
using Subscriber.WebApi.Config;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;
var setting = configuration.GetSection("setting").Get<Settings>();

builder.Services.AddControllers();
builder.Services.AddSingleton(setting);
builder.Services.ConfigurationService();
builder.Host.UseSerilog((context, configuration) =>
{

    configuration.ReadFrom.Configuration(context.Configuration);

});

builder.Services.AddDbContext<WeightWatchersContext>(option =>
{

    option.UseSqlServer(configuration.GetConnectionString("weightWatchersConnectionString"));
}
       );
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
     options =>
     {
         options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
         {
             Scheme = "Bearer",
             BearerFormat = "JWT",
             In = ParameterLocation.Header,
             Name = "Authorization",
             Description = "Bearer Authentication with JWT Token",
             Type = SecuritySchemeType.Http
         });
         options.AddSecurityRequirement(new OpenApiSecurityRequirement
                   {
                  {
                  new OpenApiSecurityScheme
               {


                       Reference = new OpenApiReference
                                 {
               Id = "Bearer",
                     Type = ReferenceType.SecurityScheme
              }
                               },
                           new List<string>()


}
});
     });
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
   .AddJwtBearer(options =>
   {
       options.TokenValidationParameters = new TokenValidationParameters
       {
           // ValidateIssuer = true,
           // ValidateAudience = true,
           ValidateLifetime = true,
           ValidateIssuerSigningKey = true,
           ValidIssuer = setting.Jwt.Issuer,
           ValidAudience = setting.Jwt.Audience, // Configuration["Jwt:Issuer"],
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(setting.Jwt.Key))
       };
   });
builder.Services.AddAuthorization();

var policy = "policy";
builder.Services.AddCors(option => option.AddPolicy(name: policy, policy =>
{
    policy.AllowAnyOrigin(); policy.AllowAnyHeader(); policy.AllowAnyMethod();
}));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder =>
{
    builder
    .AllowAnyHeader()
    .AllowAnyMethod().
    AllowAnyOrigin();
});

app.UseHttpsRedirection();
app.UseCors(policy);

app.UseSerilogRequestLogging();
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware(typeof(ErrorHandlingMiddleware));

app.Run();
