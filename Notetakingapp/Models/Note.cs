namespace Notetakingapp.Models
{
    public class Note
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<string> Tags { get; set; }

        public Note(string title, string content)
        {
            Title = title;
            Content = content;
            CreatedAt = DateTime.Now;
            Tags = new List<string>();
        }
    }
}
