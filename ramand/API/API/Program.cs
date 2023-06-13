﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Infrastructure.DIRegister;
using Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using static Dapper.SqlMapper;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using API;
using Swashbuckle.AspNetCore.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApiVersioning(o=>{
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.DefaultApiVersion = new ApiVersion(1, 0);
    o.ReportApiVersions = true;
    o.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader());
    o.AssumeDefaultVersionWhenUnspecified = true;
});
builder.Services.AddVersionedApiExplorer(
    options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


// نمونه جای صحیح که چند تا اینجکت از جای دیگه می شود 
builder.Services.AddInfrastractureDI();

//به خاطر کمبود وقت اینجا نوشتم 
builder.Services.AddSwaggerGen(swaggerGenOptions =>
{
    var swaggerOptions = new API.SwaggerOptions();
    builder.Configuration.GetSection("Swagger").Bind(swaggerOptions);

    foreach (var currentVersion in swaggerOptions.Versions)
    {
        swaggerGenOptions.SwaggerDoc(currentVersion.Name, new OpenApiInfo
        {
            Title = swaggerOptions.Title,
            Version = currentVersion.Name,
            Description = swaggerOptions.Description
        });
    }

    swaggerGenOptions.DocInclusionPredicate((version, desc) =>
    {
        if (!desc.TryGetMethodInfo(out MethodInfo methodInfo))
        {
            return false;
        }
        var versions = methodInfo.DeclaringType.GetConstructors()
            .SelectMany(constructorInfo => constructorInfo.DeclaringType.CustomAttributes
                .Where(attributeData => attributeData.AttributeType == typeof(ApiVersionAttribute))
                .SelectMany(attributeData => attributeData.ConstructorArguments
                    .Select(attributeTypedArgument => attributeTypedArgument.Value)));

        return versions.Any(v => $"{v}" == version);
    });

    swaggerGenOptions.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));

           });


builder.Services.AddScoped<IAuthenticateService, AuthenticateService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    var swaggerOptions = new API.SwaggerOptions();
    builder.Configuration.GetSection("Swagger").Bind(swaggerOptions);
    app.UseSwagger();
    app.UseSwaggerUI(option =>
    {
        foreach (var currentVersion in swaggerOptions.Versions)
        {
            option.SwaggerEndpoint(currentVersion.UiEndpoint, $"{swaggerOptions.Title} {currentVersion.Name}");
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
