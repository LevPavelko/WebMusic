using Microsoft.EntityFrameworkCore;
using WebMusic.Models;
using WebMusic.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var app = builder.Build();

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<WebMusicContext>(options => options.UseSqlServer(connection));

builder.Services.AddScoped<IRepository, WebMusicRepository>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
