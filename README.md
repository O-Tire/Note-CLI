A simple cross-platform note management app which runs on the terminal.

**Available commands:**

-v or --version:
    Display version notice.

-n or --new or --note or --add:
    Add a note.
    Requires one string argument. (your note)
    Example: note -n "Interview at 5 PM" in "Work Category"

-rm or -del or --remove or --delete:
    Removes one or multiple notes.
    Requires integer arguments. (the numbers of the notes you want to remove)
    Example: note -rm 1 7 4

-c or -clear or -clear-all:
    Removes all notes

If you call 'note' with no arguments:
    Prints out all of the notes and their numbers.

**Category System:**

You can now organize your notes with categories using this command structure:
note -n my-note in my-category

This will create a new note in a new or existing category.

**Availability:**

This app is cross-platform and open source.
Note that the released binaries are built for Windows and don't require .NET Runtime.



Your feedback is greatly appreciated!