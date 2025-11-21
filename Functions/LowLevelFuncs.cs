namespace LowLevelFuncs;
public static class Functions
{
    public static string GetNoteFilePath()
    {
        string path = Path.GetDirectoryName(Environment.ProcessPath) + "/Notes.save";

        if (!File.Exists(path))
        {
            File.Create(path).Close();
        }

        return path;
    }

    /// <returns>Raw note and category data. Raw data are strings that contain: note + CHAR_THAT_SEPERATES_NOTE_FROM_CATEGORY(optional) + category(optional)</returns>
    public static List<string> LoadNotes()
    {
        using (StreamReader reader = new(GetNoteFilePath()))
        {
            return reader.ReadToEnd()
                            .Trim()
                            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
                            .ToList();
        }
    }

    /// <summary>
    /// Saves raw note data.
    /// </summary>
    public static void SaveNotes(List<string> rawNoteData)
    {
        using (StreamWriter writer = new(GetNoteFilePath(), false))
        {
            writer.Write(
            string.Join('\n', rawNoteData)
            );
        }
    }
}