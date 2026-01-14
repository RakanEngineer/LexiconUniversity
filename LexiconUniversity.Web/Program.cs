using LexiconUniversity.Persistance;
using LexiconUniversity.Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace LexiconUniversity.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<LexiconUniversityContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("LexiconUniversityContext") ?? 
            throw new InvalidOperationException("Connection string 'LexiconUniversityContext' not found.")));

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var context = serviceProvider.GetRequiredService<LexiconUniversityContext>();

                //await context.Database.EnsureDeletedAsync();
                //await context.Database.MigrateAsync();

                try
                {
                    await SeedData.InitAsync(context);
                }
                catch (Exception ex)
                {

                    throw;
                }
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
