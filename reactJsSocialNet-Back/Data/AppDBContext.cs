using Microsoft.EntityFrameworkCore;
using reactJsSocialNet_Back.Data;

public class AppDBContext : DbContext
{
    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<UserProfile> Profiles => Set<UserProfile>();
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<Friends> Friendships => Set<Friends>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");

        // Настройка отношений
        modelBuilder.Entity<User>()
            .HasOne(u => u.Profile)
            .WithOne()
            .HasForeignKey<UserProfile>(p => p.Id)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Friends>()
            .HasKey(f => new { f.UserId, f.FriendId })
            .HasName("PK_Friendships");

        modelBuilder.Entity<Post>()
               .HasMany<Comment>() 
               .WithOne()         
               .HasForeignKey(c => c.PostId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}