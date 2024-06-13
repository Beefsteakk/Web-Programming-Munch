using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using EffectiveWebProg.Data;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {

        
        services.AddSession();
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(Configuration.GetConnectionString("DefaultConnection"),
                new MySqlServerVersion(new Version(8, 0, 30)))); 
        services.AddControllersWithViews();



        //Session implementation

    services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(1); // Set session timeout
        options.Cookie.HttpOnly = true; // Make the session cookie HTTP-only
        options.Cookie.IsEssential = true; // Make the session cookie essential
    });

    }




    }
