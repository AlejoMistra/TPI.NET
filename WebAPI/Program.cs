using Application.Services;
using Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Entity Framework Context
builder.Services.AddDbContext<TPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection" +
    "")));

// Add Dependency Injection
builder.Services.AddScoped<IProfesionalRepository, ProfesionalRepository>();
builder.Services.AddScoped<IProfesionalService, ProfesionalService>();
builder.Services.AddScoped<IEspecialidadRepository, EspecialidadRepository>();
builder.Services.AddScoped<IEspecialidadService, EspecialidadService>();

// Autenticacion JWT: los valores salen de la misma seccion que usa AuthService,
// asi el token que se emite y el que se valida no pueden desincronizarse.
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"]!;
var issuer = jwtSettings["Issuer"];
var audience = jwtSettings["Audience"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = issuer,
            ValidateAudience = true,
            ValidAudience = audience,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            ClockSkew = TimeSpan.Zero
        };
    });

// Policies de autorizacion. Hoy las cuatro exigen lo mismo porque todos los
// usuarios son Administrativo; quedan separadas para poder diferenciarlas despues.
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UsuariosLeer", policy => policy.RequireRole("Administrativo"));
    options.AddPolicy("UsuariosAgregar", policy => policy.RequireRole("Administrativo"));
    options.AddPolicy("UsuariosActualizar", policy => policy.RequireRole("Administrativo"));
    options.AddPolicy("UsuariosEliminar", policy => policy.RequireRole("Administrativo"));
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TPIContext>();
    db.Database.Migrate();   // crea la BD si no existe y aplica migraciones pendientes
}
await SeedData.SeedAsync(app.Services);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

// Autenticacion antes que autorizacion: la primera arma el ClaimsPrincipal,
// la segunda decide sobre el.
app.UseAuthentication();
app.UseAuthorization();

// Map endpoints
app.MapProfesionalEndpoints();
app.MapEspecialidadEndpoints();
app.MapUsuarioEndpoints();
app.MapAuthEndpoints();

await app.RunAsync();
