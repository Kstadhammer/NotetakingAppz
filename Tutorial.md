# Note Taking App Tutorial

This tutorial explains how to use the Note Taking App, a simple console-based application for managing your notes.

## Application Structure

The application is built using C# and follows a clean architecture:

- `Models/Note.cs`: Defines the structure of a note
- `Services/NoteService.cs`: Handles note operations (create, read, edit)
- `Program.cs`: Main application interface and user interaction

## Features

### 1. Main Menu

When you start the application, you'll see the main menu with these options:
```
=== Note Taking App ===
1. Create a new note
2. View all notes
3. Edit a note
4. Exit
```

### 2. Creating a Note

To create a new note:
1. Select option `1` from the main menu
2. Enter a title when prompted
3. Enter your note content
4. The note will be saved automatically with:
   - Timestamp
   - Title
   - Content
   - Default tag (#note)

### 3. Viewing Notes

To view all your notes:
1. Select option `2` from the main menu
2. The app will display all notes with:
   - Title
   - Creation timestamp
   - Content
3. Press any key to return to the main menu

### 4. Editing Notes (Coming Soon)

The edit functionality is currently under development. When completed, you'll be able to:
1. Select option `3` from the main menu
2. Choose a note to edit
3. Modify its content

### 5. Exiting the App

To exit the application:
1. Select option `4` from the main menu
2. The app will display a closing message in red text
3. The application will terminate

## Technical Details

### Note Storage

Notes are stored in a text file (`Notes.txt`) with the following format:
```
=== Note Entry ===
Created: [timestamp]
Title: [your title]
Content:
[your note content]
Tags: #note
==============================
```

### Code Structure

1. **Note Model** (`Models/Note.cs`)
   - Properties: Title, Content, CreatedAt, Tags
   - Represents the structure of each note

2. **Note Service** (`Services/NoteService.cs`)
   - Handles file operations
   - Manages note creation and retrieval
   - Formats note storage

3. **Program** (`Program.cs`)
   - Manages the user interface
   - Handles user input
   - Coordinates between user actions and the NoteService

## Code Documentation

### 1. Note Model (Models/Note.cs)
```csharp
// Define the Note class in the Notetakingapp.Models namespace
namespace Notetakingapp.Models
{
    public class Note
    {
        // Basic properties of a note
        public string Title { get; set; }        // Stores the note's title
        public string Content { get; set; }      // Stores the note's content
        public DateTime CreatedAt { get; set; }  // Stores when the note was created
        public List<string> Tags { get; set; }   // Stores tags associated with the note

        // Constructor to create a new note
        public Note(string title, string content)
        {
            Title = title;
            Content = content;
            CreatedAt = DateTime.Now;           // Automatically set creation time
            Tags = new List<string>();          // Initialize empty tags list
        }
    }
}
```

### 2. Note Service (Services/NoteService.cs)
```csharp
// Service class that handles all note operations
public class NoteService
{
    private readonly string _filePath;  // Stores the path to the notes file

    // Constructor takes the file path where notes will be stored
    public NoteService(string filePath)
    {
        _filePath = filePath;
    }

    // Creates a new note and saves it to the file
    public void CreateNote(Note note)
    {
        // Open file in append mode (true parameter)
        using var writer = new StreamWriter(_filePath, true);
        
        // Write note with formatted structure
        writer.WriteLine($"=== Note Entry ===");
        writer.WriteLine($"Created: {note.CreatedAt:yyyy-MM-dd HH:mm:ss}");
        writer.WriteLine($"Title: {note.Title.Trim()}");
        writer.WriteLine($"Content:");
        writer.WriteLine(note.Content.Trim());
        writer.WriteLine($"Tags: {string.Join(", ", note.Tags)}");
        writer.WriteLine(new string('=', 30));  // Separator line
    }

    // Retrieves all notes from the file
    public List<Note> GetAllNotes()
    {
        var notes = new List<Note>();
        if (!File.Exists(_filePath)) return notes;  // Return empty list if file doesn't exist

        var lines = File.ReadAllLines(_filePath);
        Note currentNote = null;
        var content = new List<string>();

        // Parse file line by line
        foreach (var line in lines)
        {
            if (line.StartsWith("=== Note Entry ==="))  // Start of new note
            {
                if (currentNote != null)  // Save previous note if exists
                {
                    currentNote.Content = string.Join(Environment.NewLine, content);
                    notes.Add(currentNote);
                    content.Clear();
                }
                currentNote = new Note("", "");  // Create new note
            }
            else if (line.StartsWith("Title: ") && currentNote != null)
            {
                currentNote.Title = line.Substring(7);  // Extract title
            }
            else if (line.StartsWith("Content:"))
            {
                continue;  // Skip the Content: header
            }
            else if (!line.StartsWith("Created: ") && !line.StartsWith("Tags: ") && !line.StartsWith("======"))
            {
                content.Add(line);  // Add line to note content
            }
        }

        // Add the last note if exists
        if (currentNote != null)
        {
            currentNote.Content = string.Join(Environment.NewLine, content);
            notes.Add(currentNote);
        }

        return notes;
    }
}
```

### 3. Program (Program.cs)
```csharp
// Main program loop
while (appIsRunning)
{
    // Display menu
    Console.Clear();
    Console.WriteLine(@"=== Note Taking App ===
1. Create a new note
2. View all notes
3. Edit a note
4. Exit
=====================");

    string userChoice = Console.ReadLine();

    // Handle user choice
    switch(userChoice)
    {
        case "1":
            CreateNewNote();    // Create new note
            break;
        case "2":
            ViewAllNotes();     // View all notes
            break;
        case "3":
            NoteEdit();         // Edit note (coming soon)
            break;
        case "4":
            ExitApp();          // Exit application
            break;
        default:
            Console.WriteLine("Invalid Choice. Press any key to continue");
            Console.ReadKey();
            break;
    }
}

// Function to create a new note
void CreateNewNote()
{
    // Get note details from user
    Console.WriteLine("Enter a title: ");
    string title = Console.ReadLine() ?? "";

    Console.WriteLine("Enter your note: ");
    string content = Console.ReadLine() ?? "";

    // Create and save the note
    var note = new Note(title, content);
    noteService.CreateNote(note);

    // Success message
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("\nNote saved successfully!");
    Console.ResetColor();
    
    Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey();
}

// Function to view all notes
void ViewAllNotes()
{
    var notes = noteService.GetAllNotes();
    
    if (notes.Count == 0)
    {
        Console.WriteLine("No notes found. Press any key to continue...");
        Console.ReadKey();
        return;
    }

    // Display each note
    foreach (var note in notes)
    {
        Console.WriteLine($"\nTitle: {note.Title}");
        Console.WriteLine($"Created: {note.CreatedAt:yyyy-MM-dd HH:mm:ss}");
        Console.WriteLine("Content:");
        Console.WriteLine(note.Content);
        Console.WriteLine(new string('-', 30));
    }

    Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey();
}

// Function to exit the application
void ExitApp()
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("\nClosing app...");
    Console.ResetColor();
    appIsRunning = false;
}

// Function for editing notes (to be implemented)
void NoteEdit()
{
    // Coming soon
}
```

## Error Handling

The application includes basic error handling:
- Null checks for user input
- File existence verification
- Input validation in the main menu

## Future Enhancements

Planned features include:
1. Note editing functionality
2. Note deletion
3. Search capabilities
4. Custom tags
5. Note categorization
