using System.ComponentModel.DataAnnotations.Schema;

namespace reactJsSocialNet_Back.Data
{
    public class User
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public UserProfile Profile { get; set; }
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

        public List<int> FriendIds { get; set; } = new();
        public List<int> PostIds { get; set; } = new();
    }

    public abstract class ContentEntity
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }

    public class Post : ContentEntity
    {
        public required string Text { get; set; }
        public List<string> MediaUrls { get; set; } = new();
    }

    public class Comment : ContentEntity
    {
        public required string Text { get; set; }
        public int PostId { get; set; }
        public List<string>? MediaUrls { get; set; }
        public int? ParentCommentId { get; set; }
    }

    public class Friends
    {
        public int UserId { get; set; }
        public int FriendId { get; set; }
        public DateTime FriendsSince { get; set; }
    }
}
