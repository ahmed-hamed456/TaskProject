using TaskProject.DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskProject.Entities.Repositories.Contract;
using TaskProject.DataAccess.ImplementationRepos;
using Microsoft.AspNetCore.Identity.UI.Services;
using myshop.Utilities;
using TaskProject.DataAccess.DbInitializer;
using TaskProject.DataAccess.Handlers;
using TaskProject.Entities.Models;

namespace TaskProject.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();


            builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

            
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // PostgreSQL configuration
            builder.Services.AddDbContext<PostgresDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));


            // Register DbContextFactory as a singleton
            builder.Services.AddSingleton<DbContextFactory>();

            builder.Services.AddIdentity<IdentityUser,IdentityRole>(
                options => options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(4)
                ).AddDefaultTokenProviders().AddDefaultUI()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddSingleton<IEmailSender, EmailSender>();
        
            
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IDbInitializer,DbInitializer>();

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


            SeedDb();

            app.UseAuthorization();

            app.MapRazorPages();

            app.MapControllerRoute(
                name: "default",
                pattern: "{area=Admin}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "Customer",
                pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
               name: "default",
               pattern: "{controller=Account}/{action=Login}/{id?}");

            

            app.Run();

            void SeedDb()
            {
                using (var scope = app.Services.CreateScope())
                {
                    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();

                    dbInitializer.Initialize();
                }
            }
        }
    }
}
