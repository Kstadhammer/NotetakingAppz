namespace Notetakingapp.Models
{
    public class Note
    {
        private static int _counter = 100;
        public string Id {get; set;}
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<string> Tags { get; set; }


        private string GenerateId()
        {

            int Id = Interlocked.Increment(ref _counter) % 10000;
            return Id.ToString("D3");

        }

        public Note(string title, string content)
        {
            Id = GenerateId();
            Title = title;
            Content = content;
            CreatedAt = DateTime.Now;
            Tags = new List<string>();
        }
    }
}
