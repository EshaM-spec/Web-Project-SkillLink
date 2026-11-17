using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SkillLinkApp.Data;
using SkillLinkApp.Models.Interface;
using SkillLinkApp.Models.Repository;
using SkillLinkApp.Repositories;
using System.Threading.Tasks;
namespace SkillLinkApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<ApplicationUser>(
     options => options.SignIn.RequireConfirmedAccount = false)
     .AddRoles<IdentityRole>()
     .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();
            builder.Services.AddTransient<ICustomerRepoInterface, CustomerRepository>();
            builder.Services.AddTransient<IWorkerRepoInterface, WorkerRepository>();
            builder.Services.AddScoped<IServiceRepoInterface, ServiceRepository>();
            builder.Services.AddScoped<IRequestRepoInterface, RequestRepository>();
            builder.Services.AddScoped<IRequestRepoInterface, RequestRepository>();
            builder.Services.AddScoped<IRequestOfferRepoInterface, RequestOfferRepository>();
            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider
                    .GetRequiredService<RoleManager<IdentityRole>>();

                string[] roles = { "Customer", "ServiceProvider" };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        var result = await roleManager.CreateAsync(
                            new IdentityRole(role));

                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                Console.WriteLine(error.Description);
                            }
                        }
                    }
                }
            }
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();

            app.Run();
        }
    }
}
