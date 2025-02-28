using System.ComponentModel.DataAnnotations.Schema;

namespace reactJsSocialNet_Back.Data
{
    public class Media
    {
        public int Id { get; set; }
        public required string Url { get; set; }
        public int? PostId { get; set; }
        public int? CommentId { get; set; }

        [ForeignKey("PostId")]
        public Post? Post { get; set; }

        [ForeignKey("CommentId")]
        public Comment? Comment { get; set; }
    }
}
