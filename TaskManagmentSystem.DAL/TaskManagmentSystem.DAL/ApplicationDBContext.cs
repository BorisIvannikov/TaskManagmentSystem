using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using TaskManagmentSystem.Domain.AdditionalsFunction;
using TaskManagmentSystem.Domain.Entity;

namespace TaskManagmentSystem.DAL
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<Department> Department { get; set; }
        public DbSet<UserEntityProfile> UserEntityProfile { get; set; }
        public DbSet<UserEntity> UserEntity { get; set; }
        public DbSet<TaskEntity> TaskEntity { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>()
                .HasOne(u => u.UserEntityProfile)
                .WithOne(u => u.UserEntity);      

            modelBuilder.Entity<UserEntity>()
                .HasOne(x => x.Department)
                .WithMany(x => x.Users)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserEntity>()
                .HasMany(x => x.taskEntitiesInitiator)
                .WithOne(x => x.Initiator)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserEntity>()
                .HasMany(x => x.taskEntitiesRecipient)
                .WithOne(x => x.Recipient)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Department>()
                .HasOne(x => x.Director);

            modelBuilder.Entity<TaskEntity>()
                .HasOne(x => x.Initiator)
                .WithMany(x => x.taskEntitiesInitiator)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TaskEntity>()
                .HasOne(x => x.Recipient)
                .WithMany(x => x.taskEntitiesRecipient)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserEntityProfile>()
                .HasData( new UserEntityProfile()
                {
                    UserEntityProfileId = 1,
                    Name = "Admin",
                    Position = Domain.Enums.Position.Admin,
                    Login = "Admin",
                    Password = AdditionalFunction.HashPassword("Admin"),
                    UserEntityId = 1,
                });

            modelBuilder.Entity<UserEntity>()
                .HasData(new UserEntity()
                {
                    UserEntityId = 1,
                    Name = "Admin",
                    Position = Domain.Enums.Position.Admin,
                    UserEntityProfileId = 1
                });
        }
    }
}
