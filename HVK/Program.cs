using HVK.Models;

namespace HVK
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Index}/{id?}");

            //Load all different kinds of vaccinations based on the enum in Vaccination model
            if (!DataAccessPoint.GetAllVaccination().Any())
            {
                foreach (var vaccination in Enum.GetValues(typeof(Vaccination.VaccinationType)))
                {
                    DataAccessPoint.CreateVaccination((Vaccination.VaccinationType)vaccination);
                }
            }

            app.Run();

        }
    }
}
