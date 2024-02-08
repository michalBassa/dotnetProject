using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using Subscriber.Data;
using Subscriber.WebApi.Config;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;
builder.Services.AddControllers();
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
builder.Services.AddSwaggerGen();

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
app.UseAuthorization();

app.UseSerilogRequestLogging();
app.MapControllers();

app.UseMiddleware(typeof(ErrorHandlingMiddleware));

app.Run();
