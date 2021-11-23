using System;
using System.Linq;
using Chimera_v2.Models;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;
namespace Chimera_v2.Data
{
    public static class  InitializeDb
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.Migrate();

            if(context.Users.Any()) return;

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = "Jorge",
                Password = BC.HashPassword("123123"),
                Role = "Admin"
            };
            context.Add(user);
            context.SaveChanges();
        }
    }
}