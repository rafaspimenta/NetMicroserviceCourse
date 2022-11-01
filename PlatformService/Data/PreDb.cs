using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlatformService.Data
{
    public static class PreDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
        }

        private static void SeedData(AppDbContext context)
        {
            if (context.Platforms.Any())
            {
                Console.WriteLine("--> We already have data");
                return;
            }

            Console.WriteLine("--> Seeding data...");
            var platforms = new List<Platform>
            {
                new Platform() { Name = "Dot Net", Cost = "Free", Publisher = "Microsoft" },
                new Platform() { Name = "Sql Server Express", Cost = "Free", Publisher = "Microsoft" },
                new Platform() { Name = "Kubernetes", Cost = "Free", Publisher = "Cloud Native Computing Foundation" }
            };
            context.Platforms.AddRange(platforms);
            context.SaveChanges();
        }
    }
}