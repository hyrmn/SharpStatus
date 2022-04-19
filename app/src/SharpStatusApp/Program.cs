using Tailwind;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddRazorPages();

var app = builder.Build();

app.UseStatusCodePagesWithReExecute("/Error", "?code={0}");
app.UseStaticFiles();

app.MapRazorPages();

var life = app.Services.GetRequiredService<IHostApplicationLifetime>();
life.ApplicationStopped.Register(() => {
    Console.WriteLine("Application is shut down");
});

if (app.Environment.IsDevelopment())
{
    app.RunTailwind("tailwind", "./");
}

app.Run();
