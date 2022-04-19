global using Serilog;

using Tailwind;

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

    builder.Services
    .AddRazorPages();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.RunTailwind("tailwind", "./");
    }

    app.UseStatusCodePagesWithReExecute("/Error", "?code={0}");
    app.UseStaticFiles();

    app.MapRazorPages();

    var life = app.Services.GetRequiredService<IHostApplicationLifetime>();
    life.ApplicationStopped.Register(() =>
    {
        Log.Information("Stopping services");
    });

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
