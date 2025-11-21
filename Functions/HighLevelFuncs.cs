using static LowLevelFuncs.Functions;
using static Constants.Utilities;

namespace HighLevelFuncs;
public static class Functions
{
    public static void NewNote(string note, string? category)
    {
        var notes = LoadNotes();

        if (category is not null) note += NOTE_FROM_CATEGORY_SEPARATOR + category;
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
            string[] notesAndCategories = rawNote.Split(NOTE_FROM_CATEGORY_SEPARATOR);
            notes.Add(notesAndCategories[0]);
            if (notesAndCategories.Length == 2)
            {
                categories          .Add(notesAndCategories[1]);
                uniqueCategories    .Add(notesAndCategories[1]);
            }
            else
            {
                categories          .Add("Uncategorized");
                uniqueCategories    .Add("Uncategorized");
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

    public static void RemoveNote(List<int> numsToRemove)
    {
        var notes = LoadNotes();
        numsToRemove.Sort();
        numsToRemove.Reverse();

        foreach (var numToRemove in numsToRemove)
        {
            try
            {
                if (numToRemove <= 0 || numToRemove > notes.Count) throw new IndexOutOfRangeException();
                
                notes.RemoveAt(numToRemove - 1);

                SaveNotes(notes);
                Console.WriteLine($"Successfully removed note #{numToRemove}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: Note could not be found:\n{ex.Message}");
            }
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