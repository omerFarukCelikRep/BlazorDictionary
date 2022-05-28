using BlazorDictionary.Api.Application.Extensions;
using BlazorDictionary.Api.WebApi.Infrastructure.Extensions;
using BlazorDictionary.Infrastructure.Persistence.Extensions;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                })
                .AddFluentValidation();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureAuthorization(builder.Configuration);

builder.Services.AddApplicationRegistration();
builder.Services.AddInfrastructureRegistration(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.ConfigureExceptionHandling(includeExceptionDetails: app.Environment.IsDevelopment());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
