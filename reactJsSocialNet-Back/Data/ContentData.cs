using System.ComponentModel.DataAnnotations.Schema;

namespace reactJsSocialNet_Back.Data
{
    public abstract class ContentEntity
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public User Author { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime? Updated { get; set; }
    }

    public class Post : ContentEntity
    {
        public required string Text { get; set; }
        public List<Media> Media { get; set; } = new();
        public List<Comment> Comments { get; set; } = new();
    }

    public class Comment : ContentEntity
    {
        public required string Text { get; set; }

        [Column("post_id")]  // Явное указание имени столбца
        public int? PostId { get; set; }

        [Column("message_id")]
        public int? MessageId { get; set; }

        [Column("parent_comment_id")]
        public int? ParentCommentId { get; set; }
            
        // Остальные свойства остаются без изменений
        public Post? Post { get; set; }
        public Message? Message { get; set; }
        public Comment? ParentComment { get; set; }
        public List<Comment> Replies { get; set; } = new();
        public List<Media> Media { get; set; } = new();
    }

    public class Message : ContentEntity
    {
        public required string Content { get; set; }
        public int DialogId { get; set; }
        public Dialog Dialog { get; set; }
        public List<Comment> Comments { get; set; } = new();
    }



}
