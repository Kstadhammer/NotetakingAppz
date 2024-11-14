# Note Taking App

A simple console-based note-taking application written in C#.

## Features

- Create new notes with titles and content
- View existing notes
- Edit notes (coming soon)
- Delete notes (coming soon)
- Automatic timestamp for each note
- Notes are saved to a text file for persistence

## Getting Started

### Prerequisites

- .NET SDK (6.0 or later)
- Any text editor or IDE (preferably JetBrains Rider or Visual Studio)

### Installation

1. Clone the repository
```bash
git clone [your-repository-url]
```

2. Navigate to the project directory
```bash
cd Notetakingapp
```

3. Build the project
```bash
dotnet build
```

4. Run the application
```bash
dotnet run
```

## Usage

The application presents a menu-driven interface with the following options:

1. Create a new note - Add a new note with a title and content
2. View a note - Display all saved notes
3. Edit a note - (Feature coming soon)
4. Delete a note - (Feature coming soon)
5. Exit - Close the application

## Project Structure

- `Program.cs` - Main application logic and menu system
- `CreateNote.cs` - Handles note creation functionality
- `Notes.txt` - Storage file for saved notes

## Contributing

Feel free to fork the project and submit pull requests for any improvements.

## License

This project is licensed under the MIT License - see the LICENSE file for details
