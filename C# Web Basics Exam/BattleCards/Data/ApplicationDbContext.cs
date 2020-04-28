namespace BattleCards.Data
{
    using BattleCards.Models;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(DatabaseConfiguration.ConnectionString);
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Card> Cards { get; set; }

        public DbSet<UserCard> UsersCards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(u => u.userCards)
                .WithOne(uc => uc.User)
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<Card>()
                .HasMany(u => u.userCards)
                .WithOne(uc => uc.Card)
                .HasForeignKey(u => u.CardId);

            modelBuilder.Entity<UserCard>()
                .HasKey(uc => new { uc.UserId, uc.CardId });
        }
    }
}
