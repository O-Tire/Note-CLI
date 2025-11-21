A simple cross-platform note management app which runs on the terminal.

**Available commands:**

-v or --version:
    Display version notice.

-n or --new or --note or --add:
    Add a note.
    Requires one string argument. (your note)

-rm or -del or --remove or --delete:
    Removes a note.
    Requires one integer argument. (the number of the note you want to remove)

-c or -clear or -clear-all:
    Removes all notes

If you call 'note' with no arguments:
    Prints out all of the notes and their numbers.

**New Category System:**

You can now organize your notes with categories using this command structure:
note -n my-note in my-category

This will create a new note in a new or existing category.

**Availability:**

This app is cross-platform and open source.
Note that the released binaries are built for Windows and don't require .NET Runtime.



Your feedback is greatly appericiated!