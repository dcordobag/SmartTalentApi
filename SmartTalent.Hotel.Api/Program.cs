using Microsoft.OpenApi.Models;
using SmartTalent.Hotel.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Smart Talent Api", Version = "v1" });
    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});


//Custom configuration
builder.Services.AddAuthenticationConfiguration();
builder.Services.AddBusinessRulesServices();
builder.Services.AddDataAccessServices();
builder.Services.AddDbContextServices(builder.Configuration);

var app = builder.Build();
//Custom configuration
app.UseHttpSecurityHeaders();


app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
