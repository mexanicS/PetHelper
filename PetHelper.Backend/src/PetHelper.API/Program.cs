using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using PetHelper.Accounts.Application;
using PetHelper.Accounts.Controllers;
using PetHelper.Accounts.Infastructure;
using PetHelper.API;
using PetHelper.API.Extensions;
using PetHelper.API.Middlewares;
using PetHelper.Species.Application;
using PetHelper.Species.Controllers;
using PetHelper.Species.Controllers.Controllers;
using PetHelper.Volunteer.Application;
using PetHelper.Volunteer.Controllers;
using PetHelper.Volunteer.Controllers.Controllers.Pet;
using PetHelper.Volunteer.Controllers.Controllers.Volunteer;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.Debug()
    .WriteTo.Seq(builder.Configuration.GetConnectionString("Seq") 
                 ?? throw new ArgumentNullException($"Seq"))
    .Enrich.WithEnvironmentName()
    .Enrich.WithMachineName()
    .Enrich.WithEnvironmentUserName()
    .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
    .CreateLogger();

// Add services to the container.
builder.Services.AddControllers()
    .AddApplicationPart(typeof(SpeciesController).Assembly)
    .AddApplicationPart(typeof(VolunteerController).Assembly)
    .AddApplicationPart(typeof(PetController).Assembly);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddSerilog();

builder.Services.AddHttpLogging(o =>
{
    o.CombineLogs = true;
});

builder.Services
    .AddVolunteersInfrastructure(builder.Configuration)
    .AddSpeciesInfrastructure(builder.Configuration)
    .AddVolunteersApplication()
    .AddSpeciesApplication()
    .AddAccountsInfrastructure(builder.Configuration)
    .AddAccountsApplication()
    .AddAuthorizationServices(builder.Configuration)
    .AddAccountsPresentation();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);

//AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
var app = builder.Build();
//var accountSeeder = app.Services.GetRequiredService<AccountsSeeder>();
//await accountSeeder.SeedAsync();

app.UseExceptionMiddleware();
app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseStaticFiles();

    //await app.ApplyMigration();
}

/*
app.UseCors(config =>
{
    config.WithOrigins("http://localhost:5221").AllowAnyHeader().AllowAnyMethod();
});*/

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();