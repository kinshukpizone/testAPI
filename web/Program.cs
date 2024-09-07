using Domain.General.OptionsModel;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using web.AppStartup;
using web.AppStartup.ServicesConfiguration._Interface;
using web.Middlewares;
using web.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

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


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var DatabaseOption = scope.ServiceProvider.GetService<IOptions<DatabaseOptions>>()!.Value;
    var databaseContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    databaseContext.Database.SetCommandTimeout(DatabaseOption.CommandTimeoutForAutoMigrate);
    databaseContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseCors("corsapp");

/// ====================================================================
/// ========================= Middleware =====================
/// 
app.UseMiddleware<JwtUnauthorizedMiddleware>();
app.UseMiddleware<ForbiddenMiddleware>();
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=SignIn}/{id?}");

app.Run();
