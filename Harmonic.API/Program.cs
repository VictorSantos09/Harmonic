using Harmonic.API.Context;
using Harmonic.Infra.Configuration;
using Harmonic.Regras.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddProblemDetails();

//builder.Services.AddInfra();
//builder.Services.AddRegras();

builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseMySql(ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("Development"))));

builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<HarmonicIdentityUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder.Build();

app.MapIdentityApi<HarmonicIdentityUser>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();
