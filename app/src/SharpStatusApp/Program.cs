global using Serilog;

using Tailwind;
using Microsoft.EntityFrameworkCore;
using SharpStatusApp.Data;
using SharpStatusApp.Areas.Identity.Data;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Configuration.AddEnvironmentVariables();

    builder.Host.UseSerilog((hostContext, loggerConfiguration) =>
        loggerConfiguration.ReadFrom.Configuration(hostContext.Configuration)
    );

    var connectionString = builder.Configuration.GetConnectionString("UserContextConnection"); ;

    builder.Services
        .AddDbContext<UserContext>(options => options.UseSqlite(connectionString))
        .AddRazorPages();

    builder.Services
        .AddDefaultIdentity<SharpStatusAppUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddEntityFrameworkStores<UserContext>();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.RunTailwind("tailwind", "./");
    }

    app.UseStatusCodePagesWithReExecute("/Error", "?code={0}");
    app.UseStaticFiles();

    app.UseAuthentication();
    app.MapRazorPages();

    var life = app.Services.GetRequiredService<IHostApplicationLifetime>();
    life.ApplicationStopped.Register(() =>
    {
        Log.Information("Stopping services");
    });

    using (var scope = app.Services.CreateScope())
    {
        var dataContext = scope.ServiceProvider.GetRequiredService<UserContext>();
        dataContext.Database.Migrate();
    }

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}
