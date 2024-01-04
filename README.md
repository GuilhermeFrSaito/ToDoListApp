# ToDoList App

This is a simple ToDo List App.
This will be part of a series of apps made with the intent of learning some programming skills. The application presents menus for creating and manipulating Lists of ToDo Tasks, with Task Name, Due Date and Status of Task.
Implements a FileHandler class to store and persist data contained in lists inside text files.

## Details

Implemented as a Console Application, intended as a training project to learn and improve a few basic concepts:
- Implementation of CRUD operations over lists;
- Implements a FileHandler class to manipulate and create some persistence of data;
- Use a DateOnly type Converter to identify lists by date

## Usage

Running the main program should lead to a initial menu, presenting the today date which will be used as date template for usage by the user. From this menu of options the user may:
- Create a new list for the today date (if none);
- Load lists from file (if any);
- Visualize tasks from lists by choosing date;
- Delete a list by date;
- Select a list to work with;
- Save the changesto file;
- Save and exit;
- Exit without saving.

After selecting a list to work with, the user may:
- Printi list tasks;
- Add tasks;
- Update tasks;
- Delete tasks;
- Change status of tasks;
- Save and close list;
- Close without saving.

## TODO

A few topics worth of improving in the future:
- Implementation of a file selector, where it is possible to create specific files, not just the one already hardcoded;
- Remove hardcoded filename and put it elsewhere, like an appsettings.json;
- Improving the existing interface, checking each menu behavior and data presentation;
- Implement behavior for null and empty data when inserting and updating ToDo Lists;
- Implement persistence of data in non-txt files, to improve security over stored data;
- Implement a way to select folder for Lists File storage, since the app saves it inside build folder;

## Additional Info

The App was tested but a lot of issues may arise from usage. Any suggestions and/or pointers as to how to improve or correct it are very welcome.
