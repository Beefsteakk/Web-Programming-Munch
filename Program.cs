using EffectiveWebProg.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); 

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession();

<<<<<<< HEAD
// builder.WebHost.UseKestrel(options =>
// {
//     options.ListenAnyIP(443, listenOptions => {
//         listenOptions.UseHttps();
//     });
// });
=======
builder.WebHost.UseKestrel(options =>
{
    options.ListenAnyIP(443, listenOptions => {
        listenOptions.UseHttps("mycert.pfx", "Password123!");
    });
});
>>>>>>> 8e1200cf6a009d3a8f508bfbf49c7b19b4003c42

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), 
                     new MySqlServerVersion(new Version(8, 0, 30))));

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

app.UseSession();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
