using Authentication2.DataAccessLayer;
using Authentication2.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Authentication2
{
    public class Startup
    {

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            _hostingEnvironment = env;

        }

        public IConfiguration Configuration { get; }
        private readonly IHostingEnvironment _hostingEnvironment;

        public void ConfigureServices(IServiceCollection services)
        {
            string connection = "";
            if (_hostingEnvironment.IsDevelopment())
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    connection = Configuration.GetConnectionString("DefaultWinConnection");
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    connection = Configuration.GetConnectionString("DefaultMacConnection");
                }
                services.AddDbContext<MyProductionDbContext>(options =>
                  options.UseSqlite(connection));

                var optionsBuilder = new DbContextOptionsBuilder<MyProductionDbContext>();
                optionsBuilder.UseSqlite(connection);
                var db = new MyProductionDbContext(optionsBuilder.Options);

                services.AddSingleton<IDbContext>(db);

            }
            else
            {
                connection = Configuration.GetConnectionString("DefaultConnection");
                var optionsBuilder = new DbContextOptionsBuilder<MyProductionDbContext>();
                optionsBuilder.UseSqlServer(connection);
                var db = new MyProductionDbContext(optionsBuilder.Options);
                services.AddSingleton<IDbContext>(db);
                services.AddDbContext<MyProductionDbContext>(
                    options => options.UseSqlServer(connection)
                    );
            }
            Debug.WriteLine("You are using connection string: " + connection);


            services.AddIdentity<MyIdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            }).AddEntityFrameworkStores<MyProductionDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = new PathString("/Accounts/BecomeDriver");
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.ConfigureApplicationCookie(options => options.LoginPath = "/Accounts/Login");

        }

        public void Configure(IApplicationBuilder app,
            UserManager<MyIdentityUser> userManager, 
            RoleManager<IdentityRole> roleManager,
            MyProductionDbContext context)
        {
            if (_hostingEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {

                routes.MapRoute(
                    name: "Areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            MyIdentityInitializer.SeedData(userManager, roleManager, context);


        }
    }
}
