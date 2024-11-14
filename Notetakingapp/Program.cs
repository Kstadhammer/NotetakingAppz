using Notetakingapp.Models;
using Notetakingapp.Services;

string saveNoteFile = @"/Users/kimhammerstad/RiderProjects/Notetakingapp/Notes.txt";
var noteService = new NoteService(saveNoteFile);

bool appIsRunning = true;

while (appIsRunning)
{
    Console.Clear();
    Console.WriteLine(@"=== Note Taking App ===
1. Create a new note
2. View all notes
3. Edit a note
4. Exit
=====================
Enter your choice (1-3): ");

    string userChoice = Console.ReadLine();

    switch(userChoice)
    {
        case "1":
            CreateNewNote();
            break;
        case "2":
            ViewAllNotes();
            break;
        case "3":
            NoteEdit();
            break;
        case "4":
            ExitApp();
            break;
        default:
            Console.WriteLine("Invalid Choice. Press any key to continue");
            Console.ReadKey();
            break;
    }
}



void ExitApp()
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("\nClosing app...");
    Console.ResetColor();
    appIsRunning = false;




}
void CreateNewNote()
{
    Console.WriteLine("Enter a title: ");
    string title = Console.ReadLine() ?? "";


    Console.WriteLine("Enter your note: ");
    string content = Console.ReadLine() ?? "";

    var note = new Note(title, content);
    noteService.CreateNote(note);

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("\nNote saved successfully!");
    Console.ResetColor();
    
    Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey();
}

void ViewAllNotes()
{
    var notes = noteService.GetAllNotes();
    
    if (notes.Count == 0)
    {
        Console.WriteLine("No notes found. Press any key to continue...");
        Console.ReadKey();
        return;
    }

    foreach (var note in notes)
    {
        Console.WriteLine($"ID: {note.Id}");
        Console.WriteLine($"\nTitle: {note.Title}");
        Console.WriteLine($"Created: {note.CreatedAt:yyyy-MM-dd HH:mm:ss}");
        Console.WriteLine("Content:");
        Console.WriteLine(note.Content);
        Console.WriteLine(new string('-', 30));
    }

    Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey();
}


void NoteEdit()
{


}