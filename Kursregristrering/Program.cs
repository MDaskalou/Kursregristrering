using AutoMapper;
using Kursregristrering.DBContext;
using Kursregristrering.Interface;
using Kursregristrering.Mapping;
using Kursregristrering.Seeder; // L�gg till detta s� DataSeeder hittas
using Kursregristrering.Service;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks; // F�r Task
using Microsoft.AspNetCore.Builder;

namespace MyCourseRegistrationApp
{
    public class Program
    {
        public static async Task Main(string[] args)  // Markera Main som async Task
        {
            // Skapar och konfigurerar webbapplikationen
            var builder = WebApplication.CreateBuilder(args);

            // Registrera tj�nster i DI-containern
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationDBContext>(options =>
                options.UseSqlServer(connectionString));

            // Exempel p� hur du kan l�gga till andra tj�nster:
            // builder.Services.AddFluentValidationAutoValidation();
            // builder.Services.AddScoped<IUserService, UserService>();
            // builder.Services.AddScoped<ICourseService, CourseService>();
            // builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();

            var app = builder.Build();

            // Skapa en scope och k�r seedningen
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
                // Om du vill k�ra migrationer automatiskt, avkommentera n�sta rad:
                // context.Database.Migrate();
                await UserSeeder.SeedUserAsync(context);
            }

            // Konfigurera HTTP-f�rfr�gningspipen
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
