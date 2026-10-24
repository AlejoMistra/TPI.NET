using Application.Services;
using Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
     {
         options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
         {
             Name = "Authorization",
             Type = SecuritySchemeType.Http,
             Scheme = "bearer",
             BearerFormat = "JWT",
             In = ParameterLocation.Header,
             Description = "JWT Authorization header. Ejemplo: \"Bearer {token}\""
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

// Add Entity Framework Context
builder.Services.AddDbContext<TPIContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(10),
                errorNumbersToAdd: null);
            sqlOptions.CommandTimeout(60);
        }));

// Add Dependency Injection
builder.Services.AddScoped<IProfesionalRepository, ProfesionalRepository>();
builder.Services.AddScoped<IProfesionalService, ProfesionalService>();
builder.Services.AddScoped<IEspecialidadRepository, EspecialidadRepository>();
builder.Services.AddScoped<IEspecialidadService, EspecialidadService>();
builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
builder.Services.AddScoped<IPacienteService, PacienteService>();
builder.Services.AddScoped<ITurnoRepository, TurnoRepository>();
builder.Services.AddScoped<ITurnoService, TurnoService>();
builder.Services.AddScoped<IHistoriaClinicaRepository, HistoriaClinicaRepository>();
builder.Services.AddScoped<IHistoriaClinicaService, HistoriaClinicaService>();
builder.Services.AddScoped<IFacturaRepository, FacturaRepository>();
builder.Services.AddScoped<IFacturaService, FacturaService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<AuthService>();

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
    // Políticas para Especialidades
    options.AddPolicy("EspecialidadesLeer", policy => policy.RequireClaim("permission", "especialidades.leer"));
    options.AddPolicy("EspecialidadesAgregar", policy => policy.RequireClaim("permission", "especialidades.agregar"));
    options.AddPolicy("EspecialidadesActualizar", policy => policy.RequireClaim("permission", "especialidades.actualizar"));
    options.AddPolicy("EspecialidadesEliminar", policy => policy.RequireClaim("permission", "especialidades.eliminar"));

    // Políticas para Profesionales
    options.AddPolicy("ProfesionalesLeer", policy => policy.RequireClaim("permission", "profesionales.leer"));
    options.AddPolicy("ProfesionalesAgregar", policy => policy.RequireClaim("permission", "profesionales.agregar"));
    options.AddPolicy("ProfesionalesActualizar", policy => policy.RequireClaim("permission", "profesionales.actualizar"));
    options.AddPolicy("ProfesionalesEliminar", policy => policy.RequireClaim("permission", "profesionales.eliminar"));

    // Políticas para Pacientes
    options.AddPolicy("PacientesLeer", policy => policy.RequireClaim("permission", "pacientes.leer"));
    options.AddPolicy("PacientesAgregar", policy => policy.RequireClaim("permission", "pacientes.agregar"));
    options.AddPolicy("PacientesActualizar", policy => policy.RequireClaim("permission", "pacientes.actualizar"));
    options.AddPolicy("PacientesEliminar", policy => policy.RequireClaim("permission", "pacientes.eliminar"));

    // Políticas para Turnos
    options.AddPolicy("TurnosLeer", policy => policy.RequireClaim("permission", "turnos.leer"));
    options.AddPolicy("TurnosAgregar", policy => policy.RequireClaim("permission", "turnos.agregar"));
    options.AddPolicy("TurnosActualizar", policy => policy.RequireClaim("permission", "turnos.actualizar"));
    options.AddPolicy("TurnosEliminar", policy => policy.RequireClaim("permission", "turnos.eliminar"));
    options.AddPolicy("TurnosAtender", policy => policy.RequireClaim("permission", "turnos.atender"));

    // Políticas para Historias Clínicas
    options.AddPolicy("HistoriasClinicasLeer", policy => policy.RequireClaim("permission", "historiasclinicas.leer"));
    options.AddPolicy("HistoriasClinicasAgregar", policy => policy.RequireClaim("permission", "historiasclinicas.agregar"));
    options.AddPolicy("HistoriasClinicasActualizar", policy => policy.RequireClaim("permission", "historiasclinicas.actualizar"));

    // Políticas para Facturas
    options.AddPolicy("FacturasLeer", policy => policy.RequireClaim("permission", "facturas.leer"));
    options.AddPolicy("FacturasAgregar", policy => policy.RequireClaim("permission", "facturas.agregar"));
    options.AddPolicy("FacturasActualizar", policy => policy.RequireClaim("permission", "facturas.actualizar"));

    // Políticas para Usuarios
    options.AddPolicy("UsuariosLeer", policy => policy.RequireClaim("permission", "usuarios.leer"));
    options.AddPolicy("UsuariosAgregar", policy => policy.RequireClaim("permission", "usuarios.agregar"));
    options.AddPolicy("UsuariosActualizar", policy => policy.RequireClaim("permission", "usuarios.actualizar"));
    options.AddPolicy("UsuariosEliminar", policy => policy.RequireClaim("permission", "usuarios.eliminar"));

    // Fallback: Requerir autenticación para endpoints no especificados
    options.FallbackPolicy = options.DefaultPolicy;
});

// Add CORS for Blazor WebAssembly + React Native
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorWasm",
        policy =>
        {
            policy.AllowAnyOrigin() // TODO: En producción especificar orígenes exactos por seguridad
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseExceptionHandler();

// Add pending migrations and seed the database
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

// Use CORS
app.UseCors("AllowBlazorWasm");

// Autenticacion antes que autorizacion
// la segunda decide sobre el.
app.UseAuthentication();
app.UseAuthorization();

// Map endpoints
app.MapAuthEndpoints();
app.MapUsuarioEndpoints();
app.MapProfesionalEndpoints();
app.MapEspecialidadEndpoints();
app.MapTurnoEndpoints();
app.MapHistoriaClinicaEndpoints();
app.MapPacienteEndpoints();
app.MapFacturaEndpoints();

await app.RunAsync();
