using FlashCards.Api.Bl.Installers;
using FlashCards.Api.Bl.Mappers;
using FlashCards.Api.Dal;
using FlashCards.Api.Dal.Entities;
using FlashCards.Api.Dal.Installers;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddAutoMapper(
    cfg => cfg.LicenseKey = "eyJhbGciOiJSUzI1NiIsImtpZCI6Ikx1Y2t5UGVubnlTb2Z0d2FyZUxpY2Vuc2VLZXkvYmJiMTNhY2I1OTkwNGQ4OWI0Y2IxYzg1ZjA4OGNjZjkiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2x1Y2t5cGVubnlzb2Z0d2FyZS5jb20iLCJhdWQiOiJMdWNreVBlbm55U29mdHdhcmUiLCJleHAiOiIxNzg4NDgwMDAwIiwiaWF0IjoiMTc1Njk5Mjg4MSIsImFjY291bnRfaWQiOiIwMTk5MTRlZWI1MWM3N2JmOWRlOTI0MTlmZTU2MmZkNiIsImN1c3RvbWVyX2lkIjoiY3RtXzAxazRhZXpndmZlbW5xMjBoazljNG14NHF3Iiwic3ViX2lkIjoiLSIsImVkaXRpb24iOiIwIiwidHlwZSI6IjIifQ.fj4kfN4lqx-4c6PLvDLgTUXiRdcqGMXLOfk1W-SKpqCSRi2Ekkuk2yDdjnTlSOS8eOzOW4Ctyn_2CIeYPSj3BFNsqKqy49lI8-zzrk3AsBLR6u3zNorgGcoz9BQxP-A4NBlqZY5U0-NWd5cRNmNJr4nbTa3sVqnA4kN5DLMyBLCtKiQvld0UV_hrMCmCopuvamHLHlGfQu9yxAr944uokQ2ZQSH-sxEb1yJXRWuYv1z6tpwF3hqxq2YFyNzCTKudmDZIK_tx31mxh9nB-nV0GH4R3Ww1wwDRQCM4gkHiivYLy1zGAMTyJrRMbxXCXVhSoe9h0fpnD6V2HeDY7erH1w",
    typeof(CardDetailMapperProfile));

ConfigureDependencies(builder.Services, builder.Configuration);

var app = builder.Build();

UseDevelopmentSettings(app);

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

void UseDevelopmentSettings(WebApplication application)
{
    if (application.Environment.IsDevelopment())
    {
        application.MapOpenApi();
        application.UseSwagger();
        application.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.RoutePrefix = string.Empty;
        });
    }
}

void ConfigureDependencies(IServiceCollection serviceCollection, IConfiguration configuration)
{
    // SQL SERVER
    var connectionStringDal = configuration.GetConnectionString("DefaultConnection")
                              ?? throw new ArgumentException("The connection string for app is missing");
    ApiDalInstaller.Install(serviceCollection, connectionStringDal);
    
    ApiBlInstaller.Install(serviceCollection);
}