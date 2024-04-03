using Microsoft.EntityFrameworkCore;
using WebMusic.Models;
using WebMusic.BLL.Interfaces;
using WebMusic.BLL.Services;
using WebMusic.BLL.Infrastructure;


var builder = WebApplication.CreateBuilder(args);



string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddWebMusicContext(connection);
builder.Services.AddUnitOfWorkService();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IMediaService, MediaService>();

builder.Services.AddTransient<IGenreService, GenreService>();
builder.Services.AddTransient<IExecutorService, ExecutorService>();

builder.Services.AddControllersWithViews();
var app = builder.Build();
//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


//app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
