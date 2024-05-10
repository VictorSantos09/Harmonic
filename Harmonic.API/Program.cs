using FluentValidation;
using Harmonic.API.Context;
using Harmonic.Domain.Configuration;
using Harmonic.Domain.Entities.Pais;
using Harmonic.Infra.Configuration;
using Harmonic.Regras.Configuration;
using Harmonic.Shared.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MySql.Data.MySqlClient;
using System.Data;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Harmonic API", Version = "v1" });

    // Configuração da autenticação Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                new string[] {}
            }
        });
});

builder.Services.AddProblemDetails();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.SignIn.RequireConfirmedPhoneNumber = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedAccount = false;
});

string? connectionString = builder.Configuration.GetConnectionString();

builder.Services.AddTransient<IDbConnection>(x => new MySqlConnection(connectionString));

builder.Services.AddDomain();
builder.Services.AddInfra();
builder.Services.AddRegras();

builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.WithOrigins("*")
           .AllowAnyMethod()
           .AllowAnyHeader();
}));


builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<HarmonicIdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder.Build();

app.MapIdentityApi<HarmonicIdentityUser>();

if (app.Environment.IsDevelopment())
{
    RepositoryConnection.UseDevelopment = true;
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MyPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    //var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    List<string> roles = ["ADMIN"];
    //var user = userManager.Users.First();
    //await userManager.AddToRolesAsync(user, roles);

    List<Claim> claims = [new Claim("ADMIN", "TRUE")];

    foreach (var r in roles)
    {
        IdentityRole role = new(r);
        if (!await roleManager.RoleExistsAsync(r))
        {
            await roleManager.CreateAsync(role);
        }
    }
}

app.Run();
