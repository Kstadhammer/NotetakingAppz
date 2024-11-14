using Notetakingapp.Models;

namespace Notetakingapp.Services
{
    public class NoteService
    {
        private readonly string _filePath;

        public NoteService(string filePath)
        {
            _filePath = filePath;
        }

        public void CreateNote(Note note)
        {
            using var writer = new StreamWriter(_filePath, true);
            writer.WriteLine($"=== Note Entry ===");
            writer.WriteLine($"ID: {note.Id}");
            writer.WriteLine($"Created: {note.CreatedAt:yyyy-MM-dd HH:mm:ss}");
            writer.WriteLine($"Title: {note.Title.Trim()}");
            writer.WriteLine($"Content:");
            writer.WriteLine(note.Content.Trim());
            writer.WriteLine($"Tags: {string.Join(", ", note.Tags)}");
            writer.WriteLine(new string('=', 30));
        }

        public List<Note> GetAllNotes()
        {
            var notes = new List<Note>();
            if (!File.Exists(_filePath)) return notes;

            var lines = File.ReadAllLines(_filePath);
            Note currentNote = null;
            var content = new List<string>();

            foreach (var line in lines)
            {
                if (line.StartsWith("=== Note Entry ==="))
                {
                    if (currentNote != null)
                    {
                        currentNote.Content = string.Join(Environment.NewLine, content);
                        notes.Add(currentNote);
                        content.Clear();
                    }
                    currentNote = new Note("", "");
                }
                else if (line.StartsWith("Title: ") && currentNote != null)
                {
                    currentNote.Title = line.Substring(7);
                }
                else if (line.StartsWith("Content:"))
                {
                    // Skip the "Content:" line
                    continue;
                }
                else if (!line.StartsWith("Created: ") && !line.StartsWith("Tags: ") && !line.StartsWith("======"))
                {
                    content.Add(line);
                }
            }

            if (currentNote != null)
            {
                currentNote.Content = string.Join(Environment.NewLine, content);
                notes.Add(currentNote);
            }

            return notes;
        }
    }
}
