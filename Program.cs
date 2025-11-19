class Program
{
    const string VERSION_NOTICE = "Note CLI - v1.1.1";

    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            PrintNotes();
            return;
        }
    
        for (int i = 0; i < args.Length; i++)
        {
            switch (args[i])
            {
                case "-v":
                case "--version":
                    PrintVersionNotice();
                    break;
                    
                case "-n":
                case "--new":
                case "--note":
                case "--add":
                    // Example of command:        note   -n    my-note     in      my-category
                    //        Index guide:               (i)   (i + 1)   (i + 2)     (i + 3)
                    string note = args[i + 1];
                    string? categoryName = null;
                    
                    if (args.Length >= i + 3 && args[i + 2].ToLower() == "in")
                    {
                        categoryName = args[i + 3];
                        i += 3;
                    }
                    else
                    {
                        i += 1;
                    }
                    
                    if (note.Contains('%') || categoryName is not null && categoryName.Contains('%'))
                    {
                        Console.WriteLine("ERROR: Your note or category name should not contain the character '%'.");
                        return;
                    }
                    
                    NewNote(note, categoryName);
                    break;
                    
                case "-rm":
                case "-del":
                case "--remove:":
                case "--delete:":
                    RemoveNote(args[++i]);
                    break;
                    
                case "-c":
                case "--clear":
                case "--clear-all":
                    ClearAllNotes();
                    break;
                
                default:
                    Console.WriteLine("Could not find flag: " + args[i]);
                    break;
            }
        }
    }

#region Low-level Functions
    
    static string GetNoteFilePath()
    {
        string path = Path.GetDirectoryName(Environment.ProcessPath) + "/Notes.save";
        
        if (!File.Exists(path))
        {
            File.Create(path).Close();
        }
    
        return path;
    }

    /// <returns>Raw note and category data. Raw data are strings that contain: note + % + category(optional)</returns>
    static List<string> LoadNotes()
    {
        using (StreamReader reader = new(GetNoteFilePath()))
        {
            return reader.ReadToEnd().Trim().Split('\n').ToList();
        }
    }

    /// <summary>
    /// Saves raw note data.
    /// </summary>
    static void SaveNotes(List<string> rawNoteData)
    {
        using (StreamWriter writer = new(GetNoteFilePath(), false))
        {
            writer.Write(
            string.Join('\n', rawNoteData)
            );
        }
    }

#endregion

#region High-level Functions

    static void NewNote(string note, string? category)
    {
        var notes = LoadNotes();
        
        if (category is not null) note += "%" + category;
        notes.Add(note);
        
        SaveNotes(notes);
        Console.WriteLine("Successfully added a new note.");
    }
    
    static void PrintNotes() // TODO: WIP
    {
        var rawNotes = LoadNotes();
        List<string> notes = new();
        List<string> categories = new();
        HashSet<string> uniqueCategories = new();
        
        // Parsing the raw data into notes and categories.
        foreach (string rawNote in rawNotes)
        {
            string[] notesAndCategories = rawNote.Split('%');
            notes.Add(notesAndCategories[0]);
            if (notesAndCategories.Length == 2)
            {
                categories          .Add(notesAndCategories[1]);
                uniqueCategories    .Add(notesAndCategories[1]);
            }
            else
            {
                categories          .Add("Uncatagorized");
                uniqueCategories    .Add("Uncatagorized");
            }
        }
        
        // Printing.
        
        Console.WriteLine("\n");
        
        var uniqueCategoriesArray = uniqueCategories.ToArray();
        
        for (int i = 0; i < uniqueCategoriesArray.Length; i++)
        {
            Console.WriteLine($"    {uniqueCategoriesArray[i]}:");
            
            for (int j = 0; j < notes.Count; j++)
            {
                if (categories[j] == uniqueCategoriesArray[i])
                {
                    Console.WriteLine($"{j + 1}. {notes[j]}");
                }
            }
            
            Console.WriteLine("\n");
        }
        
        Console.WriteLine("\n");
    }
    
    static void RemoveNote(string stringIndex)
    {
        var notes = LoadNotes();
        
        try
        {
            int index = Convert.ToInt16(stringIndex) - 1;
            
            if (index + 1 > notes.Count) throw new IndexOutOfRangeException();
            
            notes.RemoveAt(index);
        
            SaveNotes(notes);
            Console.WriteLine($"Successfully removed note #{index + 1}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR: Note could not be found:\n{ex.Message}");
        }
    }
    
    static void PrintVersionNotice()
    {
        Console.WriteLine(VERSION_NOTICE);
    }
    
    static void ClearAllNotes()
    {
        Console.WriteLine("\nAre you sure that you want to clear all notes? (Y/N)");

        if (Console.ReadKey().Key != ConsoleKey.Y)
        {
            Console.WriteLine("Operation canceled.");
            return;
        }

        SaveNotes(new List<string>());
        Console.WriteLine("All notes have been successfully cleared.");

    }

#endregion
}