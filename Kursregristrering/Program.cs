using AutoMapper;
using Kursregristrering.DBContext;
using Kursregristrering.Interface;
using Kursregristrering.Mapping;
using Kursregristrering.Seeder; // Lägg till detta så DataSeeder hittas
using Kursregristrering.Service;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks; // För Task
using Microsoft.AspNetCore.Builder;

namespace MyCourseRegistrationApp
{
    public class Program
    {
        public static async Task Main(string[] args)  // Markera Main som async Task
        {
            // Skapar och konfigurerar webbapplikationen
            var builder = WebApplication.CreateBuilder(args);

            // Registrera tjänster i DI-containern
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationDBContext>(options =>
                options.UseSqlServer(connectionString));

            // Exempel på hur du kan lägga till andra tjänster:
            // builder.Services.AddFluentValidationAutoValidation();
            // builder.Services.AddScoped<IUserService, UserService>();
            // builder.Services.AddScoped<ICourseService, CourseService>();
            // builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();

            var app = builder.Build();

            // Skapa en scope och kör seedningen
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
                // Om du vill köra migrationer automatiskt, avkommentera nästa rad:
                // context.Database.Migrate();
                await UserSeeder.SeedUserAsync(context);
            }

            // Konfigurera HTTP-förfrågningspipen
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
