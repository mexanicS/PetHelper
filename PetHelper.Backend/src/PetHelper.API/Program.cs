using PetHelper.Application.Volunteers;
using PetHelper.Application.Volunteers.CreateVolunteers;
using PetHelper.Infastructure;
using PetHelper.Infastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ApplicationDbContext>();


builder.Services.AddScoped<CreateVolunteerHandler>();
builder.Services.AddScoped<IVolunteersRepository, VolunteersRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
