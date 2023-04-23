using ApiTemplate;
using ApiTemplate.Services;
using ApiTemplate.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

#region Builder
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();//con esto se vuelven visibles las anotaciones 
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "ApiTemplate"
    });
    //Configuracion de Swagger para que resiva el JWT 
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        In = ParameterLocation.Header
    });
    //Se agrega a seguridad requerida
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
                         Array.Empty<string>()
                    }
                });
});

//Inyeccion de la cadena de conexion
builder.Services.AddDbContext<Db_TemplateContext>(options => {
    var conection = builder.Configuration.GetConnectionString(@"database");
    options.UseSqlServer(conection);
});

//Configracion para traer la key del JWT
builder.Configuration.AddJsonFile("appsettings.json");
var key = builder.Configuration.GetSection("settings").GetSection("key").ToString();
var encoding = Encoding.UTF8.GetBytes(key);

//Configuracoion de el JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(encoding),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

//Inyeccion de dependencias 
builder.Services.AddScoped<IAccountVerifyService, AccountVerifyService>();
builder.Services.AddScoped<IRenglonesService, RenglonesService>();
builder.Services.AddScoped<IArticulosService, ArticulosService>();

//Configuracion Cors
builder.Services.AddCors();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Configuracion de cors para entrada de todos los origenes 
app.UseCors(options =>
options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthentication();
//agregar para el jwt
app.UseAuthorization();

app.MapControllers();

app.Run();
