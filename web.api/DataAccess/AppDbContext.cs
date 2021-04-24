using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using web.api.App.Events;
using web.api.App.Ingredients.ReferenceIngredients;
using web.api.App.Recipes;
using web.api.App.Teams;
using web.api.App.Users;
using web.api.DataAccess.Configurations;

namespace web.api.DataAccess
{
    public sealed class AppDbContext : DbContext
    {
        private static readonly object Locker = new();

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            lock (Locker)
            {
                if (Database.GetPendingMigrations().Any())
                {
                    Database.Migrate();
                }
            }
        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<ReferenceIngredient> ReferenceIngredients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
            modelBuilder.UseIdentityAlwaysColumns();
            modelBuilder.ApplyConfiguration(new UserDbConfiguration());
            modelBuilder.ApplyConfiguration(new TeamDbConfiguration());

            modelBuilder.Entity<Event>()
                .HasOne<Team>()
                .WithMany()
                .HasForeignKey(z => z.TeamId);

            modelBuilder.Entity<ReferenceIngredient>()
                .HasOne<Team>()
                .WithMany()
                .HasForeignKey(z => z.TeamId);
        }

        private bool HasUnappliedMigrations()
        {
            var migrationsAssembly = this.GetService<IMigrationsAssembly>();
            var differ = this.GetService<IMigrationsModelDiffer>();

            return differ.HasDifferences(
                migrationsAssembly.ModelSnapshot.Model.GetRelationalModel(),
                Model.GetRelationalModel());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        public static void Register(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(opt =>
                    opt.UseNpgsql("Server=localhost;Database=MeallerDevelop;User Id=postgres;Password=P@ssword1"),
                ServiceLifetime.Transient);

            services.BuildServiceProvider()
                .GetService<AppDbContext>();
        }
    }
}