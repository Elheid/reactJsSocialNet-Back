namespace reactJsSocialNet_Back.Data
{
    public class Dialog
    {
        public int Id { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime? LastMessageTime { get; set; }

        
        public List<UserDialog> Participants { get; set; } = new();
        public List<Message> Messages { get; set; } = new();
    }

    public class UserDialog
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int DialogId { get; set; }
        public Dialog Dialog { get; set; }
    }

}
