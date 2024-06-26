using Prueba.Helpers;
using Prueba.Providers;
using Microsoft.EntityFrameworkCore;
using Prueba.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<SectorsContext>(options =>
        options.UseMySql(
            builder.Configuration.GetConnectionString("MySqlConnection"),
            Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.20-mysql"))
    );

builder.Services.AddSingleton<PathProvider>();
builder.Services.AddSingleton<HelperUploadFiles>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Sectors}/{action=Index}/{id?}");

app.Run();
