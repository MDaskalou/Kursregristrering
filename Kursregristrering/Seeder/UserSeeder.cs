using Bogus;
using Kursregristrering.DBContext;
using Kursregristrering.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Kursregristrering.Seeder
{
    public static class UserSeeder
    {
        public static async Task SeedUserAsync(ApplicationDBContext context)
        {
            // Kolla om det redan finns data i Users-tabellen
            if (await context.Users.AnyAsync())
            {
                return; // Avbryt seedning om data redan finns
            }

            // Skapa en Faker för User med korrekta lambda-satser (två parametrar: f och u)
            var userFaker = new Faker<User>()
                .RuleFor(u => u.UserId, (f, u) => Guid.NewGuid())
                .RuleFor(u => u.Name, (f, u) => f.Person.FullName)
                .RuleFor(u => u.Email, (f, u) => f.Person.Email);

            // Generera 10 användare med Bogus
            var users = userFaker.Generate(10);

            // Lägg till de genererade användarna i databasen
            await context.Users.AddRangeAsync(users);
            await context.SaveChangesAsync();
        }
    }
}
