using static LowLevelFuncs.Functions;

namespace HighLevelFuncs;
public static class Functions
{
    const string VERSION_NOTICE = "Note CLI - v1.1.2";

    public static void NewNote(string note, string? category)
    {
        var notes = LoadNotes();

        if (category is not null) note += "%" + category;
        notes.Add(note);

        SaveNotes(notes);
        Console.WriteLine("Successfully added a new note.");
    }

    public static void PrintNotes()
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
        int noteNumber = 1;

        for (int i = 0; i < uniqueCategoriesArray.Length; i++)
        {
            Console.WriteLine($"    {uniqueCategoriesArray[i]}:");

            for (int j = 0; j < notes.Count; j++)
            {
                if (categories[j] == uniqueCategoriesArray[i])
                {
                    Console.WriteLine($"{noteNumber++}. {notes[j]}");
                }
            }

            Console.WriteLine("\n");
        }

        Console.WriteLine("\n");
    }

    public static void RemoveNote(string stringIndex)
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

    public static void PrintVersionNotice()
    {
        Console.WriteLine(VERSION_NOTICE);
    }

    public static void ClearAllNotes()
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
}