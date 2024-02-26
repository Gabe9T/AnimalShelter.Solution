using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace AnimalShelter.Models
{
    public class AnimalShelterContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Animal> Animals { get; set; }

        public AnimalShelterContext(DbContextOptions<AnimalShelterContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityUserLogin<string>>(e =>
            {
                e.HasKey(e => new { e.LoginProvider, e.ProviderKey });
            });

            builder.Entity<Animal>()
                .HasData(
                   new Animal { AnimalId = 1, Name = "Gus", Species = "Hippo", Age = 7, Description = "Gus the hippo is 7 years old "},
                   new Animal { AnimalId = 2, Name = "Garry", Species = "Giraffe", Age = 52, Description = "Garry the Giraffe is 52 years old "},
                   new Animal { AnimalId = 3, Name = "Colby", Species = "Koala", Age = 6, Description = "Colby the koala is 6 years old "},
                   new Animal { AnimalId = 4, Name = "Bernie", Species = "Bison", Age = 28, Description = "Bernie the bison is 28  years old "},
                   new Animal { AnimalId = 5, Name = "Terry", Species = "Turtle", Age = 81, Description = "Terry the turtle is 81 years old "},
                   new Animal { AnimalId = 6, Name = "Fred", Species = "Frog", Age = 1, Description = "Fred the frog is 1 year old "}
            );
        }
    }
}