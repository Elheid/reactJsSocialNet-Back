using System.ComponentModel.DataAnnotations.Schema;

namespace reactJsSocialNet_Back.Data
{
        public class User
        {
            public int Id { get; set; }
            public required string Username { get; set; }
            public required string PasswordHash { get; set; }
            public DateTime LastSeen { get; set; } = DateTime.UtcNow;
            public bool IsOnline { get; set; }

            // Навигационные свойства
            public UserProfile Profile { get; set; }
            public List<Friendship> Friendships { get; set; } = new();
            public List<Post> Posts { get; set; } = new();
            public List<UserDialog> Dialogs { get; set; } = new();
            public List<Comment> Comments { get; set; } = new();
            public List<Message> Messages { get; set; } = new();
        }

        public class UserProfile
        {
            public int Id { get; set; }
            public required string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime BirthDate { get; set; }
            public string City { get; set; }
            public string PhoneNumber { get; set; }
            public string Email { get; set; }
            public string Description { get; set; }
            public string AvatarUrl { get; set; }
        }

        public class Friendship
        {
            public int Id { get; set; }
            public int UserId { get; set; }
            public int FriendId { get; set; }
            public DateTime FriendsSince { get; set; } = DateTime.UtcNow;

            [ForeignKey("UserId")]
            public User User { get; set; }

            [ForeignKey("FriendId")]
            public User Friend { get; set; }
        }
}
