using Domain.General.OptionsModel;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Reflection;
using webapi.AppStartup;
using webapi.AppStartup.ServicesConfiguration._Interface;
using webapi.Middlewares;
using webapi.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
/// ====================================================================
/// ========================= Options conigration ======================
/// 
builder.Services.ConfigureOptions<DatabaseOptionsSetup>();
builder.Services.ConfigureOptions<JwtConfigsOptionsSetup>();

/// ====================================================================
/// ========================= Dependency Injection =====================
/// 
builder.Services
    .InstallService(builder.Configuration,
    typeof(IServiceInstaller).Assembly);

builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "E-com",
        Description = "",
        //TermsOfService = "",
        Contact = new OpenApiContact
        {
            Name = "E-Com",
            Email = string.Empty,
            
        },
        License = new OpenApiLicense
        {
            Name = "E-Com",

        }
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,

                        },
                        new List<string>()
                      }
                    });
    

});

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var DatabaseOption = scope.ServiceProvider.GetService<IOptions<DatabaseOptions>>()!.Value;
    var databaseContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    databaseContext.Database.SetCommandTimeout(DatabaseOption.CommandTimeoutForAutoMigrate);
    databaseContext.Database.Migrate();
}
app.UseCors(builder =>
{
    builder
       .WithOrigins("http://localhost:4200")
       .SetIsOriginAllowedToAllowWildcardSubdomains()
       .AllowAnyHeader()
       .AllowCredentials()
       .WithMethods("GET", "PUT", "POST", "DELETE", "OPTIONS")
       .SetPreflightMaxAge(TimeSpan.FromSeconds(3600));
}
);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("corsapp");

/// ====================================================================
/// ========================= Middleware =====================
/// 
app.UseMiddleware<JwtUnauthorizedMiddleware>();
app.UseMiddleware<ForbiddenMiddleware>();
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
