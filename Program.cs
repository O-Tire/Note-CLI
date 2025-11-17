class Program
{
    const string VERSION_NOTICE = "Note CLI - v1.0.0";

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
                    TakeNote(args[++i]);
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

    static List<string> LoadNotes()
    {
        using (StreamReader reader = new(GetNoteFilePath()))
        {
            return reader.ReadToEnd().Trim().Split('\n').ToList();
        }
    }

    static void SaveNotes(List<string> notes)
    {
        using (StreamWriter writer = new(GetNoteFilePath(), false))
        {
            writer.Write(
            string.Join('\n', notes)
            );
        }
    }

#endregion

#region High-level Functions

    static void TakeNote(string note)
    {
        var notes = LoadNotes();
        notes.Add(note);
        
        SaveNotes(notes);
        Console.WriteLine("Successfully added a new note.");
    }
    
    static void PrintNotes()
    {
        var notes = LoadNotes();
        
        Console.WriteLine("\n");
        for (int i = 0; i < notes.Count; i++)
        {
            Console.WriteLine($"{i+1}. {notes[i]}");
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
            Console.WriteLine($"ERROR while trying to remove note:\n{ex.Message}");
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