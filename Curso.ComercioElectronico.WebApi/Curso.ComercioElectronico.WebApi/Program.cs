using System.Text;
using Curso.ComercioElectronico.Aplicacion;
using Curso.ComercioElectronico.Aplicacion.Insercion;
using Curso.ComercioElectronico.Aplicacion.Services;
using Curso.ComercioElectronico.Aplicacion.ServicesImpl;
using Curso.ComercioElectronico.Dominio;
using Curso.ComercioElectronico.Dominio.Insercion;
using Curso.ComercioElectronico.Dominio.Repositories;
using Curso.ComercioElectronico.Infraestructura;
using Curso.ComercioElectronico.Infraestructura.Insercion;
using Curso.ComercioElectronico.Infraestructura.Repositories;
using Curso.ComercioElectronico.WebApi.Controllers;
using Curso.ComercioElectronico.WebApi.Filter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    //Aplicar filter globalmente a todos los controller
    options.Filters.Add<ApiExceptionFilterAttribute>();
});

//agregar conexion a base de datos
builder.Services.AddDbContext<ComercioElectronicoDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ComercioElectronico"));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Inyectar configurar las dependencias

//ServiceCollection
//REPOSITORIOS
//builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
//builder.Services.AddTransient<ICatalogoRepositorio, CatalogoRepositorio>();
//builder.Services.AddTransient<IProductRepository, ProductRepository>();
//builder.Services.AddTransient<IProductTypeRepository, ProductTypeRepository>();
//builder.Services.AddTransient<IBrandRepository, BrandRepository>();

//SERVICIOS APLICACION
//builder.Services.AddTransient(typeof(ICatalogoAplicacion), typeof(CatalogoAplicacion));
//builder.Services.AddTransient<IProductAppService, ProductAppService>();
//builder.Services.AddTransient<IProductTypeAppService, ProductTypeAppService>();
//builder.Services.AddTransient<IBrandAppService, BrandAppService>();

//1. Configurar el esquema de Autentificacion JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{

    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
    };
});

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("EsEcuatorianoyTieneLicencia", policy => policy.RequireClaim("Ecuatoriano",true.ToString())
//                                                        .RequireClaim("Licencia",true.ToString())
//                                                        .RequireRole("Admin"));
//    options.AddPolicy("Gestion", policy => policy.RequireRole("Admin"));
//});


//insercion de dependencias

builder.Services.AddInfraestructura(builder.Configuration);
builder.Services.AddAplicacion(builder.Configuration);
builder.Services.AddDominio(builder.Configuration);
builder.Services.AddCors();

//Configurations
builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection("JWT"));

var app = builder.Build(); 

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // Permitir cualquier origen
    .AllowCredentials());



//2. Registrar middleware
// el middleware debe estar antes de cualquier componente que requiere autentificacion
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
