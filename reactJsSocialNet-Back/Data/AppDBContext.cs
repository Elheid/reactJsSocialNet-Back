using Microsoft.EntityFrameworkCore;
using reactJsSocialNet_Back.Data;

public class AppDBContext : DbContext
{
    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<UserProfile> Profiles => Set<UserProfile>();
    public DbSet<Friendship> Friendships => Set<Friendship>();
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<Media> Media => Set<Media>();
    public DbSet<Dialog> Dialogs => Set<Dialog>();
    public DbSet<UserDialog> UserDialogs => Set<UserDialog>();
    public DbSet<Message> Messages => Set<Message>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");

        // User - Profile
        modelBuilder.Entity<User>()
            .HasOne(u => u.Profile)
            .WithOne()
            .HasForeignKey<UserProfile>(p => p.Id)
            .OnDelete(DeleteBehavior.Cascade);

        // Friends
        modelBuilder.Entity<Friendship>()
            .HasOne(f => f.User)
            .WithMany(u => u.Friendships)
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Friendship>()
            .HasOne(f => f.Friend)
            .WithMany()
            .HasForeignKey(f => f.FriendId)
            .OnDelete(DeleteBehavior.Restrict);


        // Dialogs and users    
        modelBuilder.Entity<UserDialog>()
            .HasKey(ud => new { ud.UserId, ud.DialogId });

        // Messages in dialogs
        modelBuilder.Entity<Message>()
            .HasOne(m => m.Dialog)
            .WithMany(d => d.Messages)
            .HasForeignKey(m => m.DialogId)
            .OnDelete(DeleteBehavior.Cascade);

        // Comments
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Post)
            .WithMany(p => p.Comments)
            .HasForeignKey(c => c.PostId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Message)
            .WithMany(m => m.Comments)
            .HasForeignKey(c => c.MessageId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.ParentComment)
            .WithMany(c => c.Replies)
            .HasForeignKey(c => c.ParentCommentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.ToTable(t => t.HasCheckConstraint(
                "CK_Comment_Parents",
                "(post_id IS NOT NULL AND message_id IS NULL) OR " +
                "(post_id IS NULL AND message_id IS NOT NULL)"));

            entity.HasIndex(c => new { c.PostId, c.Created })
                .HasDatabaseName("IX_Comment_Post_Created");

            entity.HasIndex(c => new { c.MessageId, c.Created })
                .HasDatabaseName("IX_Comment_Message_Created");
        });

        // Media content
        modelBuilder.Entity<Media>()
            .HasOne(m => m.Post)
            .WithMany(p => p.Media)
            .HasForeignKey(m => m.PostId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Media>()
            .HasOne(m => m.Comment)
            .WithMany(c => c.Media)
            .HasForeignKey(m => m.CommentId)
            .OnDelete(DeleteBehavior.Cascade);


     //indexes 
        modelBuilder.Entity<Message>()
            .HasIndex(m => new { m.DialogId, m.Created })
            .HasDatabaseName("IX_Message_Dialog_Created");

        
        modelBuilder.Entity<User>()
            .HasIndex(u => u.LastSeen)
            .HasDatabaseName("IX_User_LastSeen");


    }
}