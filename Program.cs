using static HighLevelFuncs.Functions;

static class Program
{
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
}