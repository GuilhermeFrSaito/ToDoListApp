using ToDoListApp.Models;

namespace ToDoListApp.Domain
{
    internal class ToDoListManager
    {
        private Dictionary<DateOnly, ToDoList> _toDoLists;
        private readonly FileHandler _fileHandler;
        private ToDoList? _activeList;

        public ToDoListManager(string filePath)
        {
            _toDoLists = new Dictionary<DateOnly, ToDoList>();
            _fileHandler = new FileHandler(filePath);
            _activeList = null;
        }

        public void CreateToDoList(DateOnly date)
        {
            if (!_toDoLists.ContainsKey(date))
            {
                _toDoLists[date] = new ToDoList(date);
                Console.WriteLine($"\nList for {date} created successfully!");
                PressToContinue();
                return;
            }

            Console.WriteLine("\nInformed date already has a list! No action taken.");
            PressToContinue();
        }

        public void PrintList(DateOnly date)
        {
            if (!VerifyDate(date))
            {
                Console.WriteLine($"There is no ToDo List for {date}");
                PressToContinue();
                return;
            }

            Console.Clear();
            Console.WriteLine($"Showing ToDo Items for date {date}");
            Console.WriteLine("------------------------------------------------------------------------");
            Console.WriteLine("\t Task Name \t\t Due Date\t\t Status");
            Console.WriteLine("------------------------------------------------------------------------");

            int listPosition = 1;
            foreach (var item in _toDoLists[date].Items)
            {
                if (item.IsCompleted)
                    Console.WriteLine($"{listPosition++} - {item.TaskName}\t\t\t {item.DueDate}\t Done");
                else
                    Console.WriteLine($"{listPosition++} - {item.TaskName}\t\t\t {item.DueDate}\t Pending");
            }

            Console.WriteLine("------------------------------------------------------------------------");
            PressToContinue();
        }

        public void DeleteList(DateOnly date)
        {
            if (_toDoLists.ContainsKey(date))
            {
                char confirmation;
                Console.WriteLine();
                Console.WriteLine($"\nThis will delete the list for date: {date}!");
                Console.Write("Are you sure? (Y/N)");
                confirmation = Console.ReadKey().KeyChar;
                if (confirmation.ToString().ToUpper().Equals("Y"))
                {
                    _toDoLists.Remove(date);
                    Console.WriteLine($"\nList for date {date} removed!");
                    PressToContinue();
                    return;
                }
            }

            Console.WriteLine($"\nThere is no List for date {date}");
            PressToContinue();
        }

        public void SelectList(DateOnly date)
        {
            if (VerifyDate(date))
            {
                _activeList = _toDoLists[date];

                Console.WriteLine();
                Console.WriteLine($"List for date {date} successfuly selected.");
                PressToContinue();

                return;
            }

            Console.WriteLine($"There is no list for date {date}");
            PressToContinue();
        }

        public ToDoItem? GetToDoItem(int index)
        {
            if (_activeList != null && index > 0 && _activeList.Items.Count >= index)
                return _activeList.Items[index];
            else
                return null;
        }

        public void SaveToDoLists()
        {
            _fileHandler.SaveToDoLists(_toDoLists.Values.ToList());
            Console.WriteLine("\nToDo Lists saved successfully!");
            PressToContinue();
        }

        public void LoadToDoLists()
        {
            List<ToDoList> loadedLists = _fileHandler.LoadToDoLists();
            _toDoLists = loadedLists.ToDictionary(todoList => todoList.Date, todoList => todoList);

            Console.WriteLine("\nLists successfully loaded!");
            PressToContinue();
        }

        private bool VerifyDate(DateOnly date)
        {
            if (_toDoLists.ContainsKey(date))
                return true;
            else
                return false;
        }

        #region [ Active List ]
        public bool HasActiveList()
        {
            if (_activeList != null)
                return true;

            return false;
        }

        public void ShowActiveList()
        {
            if (_activeList != null)
            {
                Console.Clear();
                Console.WriteLine("Showing Tasks in Active List");
                Console.WriteLine("---------------------------------------------------------------------------------");
                Console.WriteLine("\t Task Name\t\t\t Due Date\t\t\tStatus");

                int listPosition = 1;

                foreach (var item in _activeList.Items)
                {
                    if (item.IsCompleted)
                        Console.WriteLine($"{listPosition++} - {item.TaskName}\t\t\t {item.DueDate}\t - Done");
                    else
                        Console.WriteLine($"{listPosition++} - {item.TaskName}\t\t\t {item.DueDate}\t - Pending");
                }


                Console.WriteLine("----------------------------\tEnd of Tasks\t---------------------------------\n");
                PressToContinue();

            }
            else
            {
                Console.WriteLine("There is no active list!");
                PressToContinue();
                return;
            }
        }

        public void AddToDoItem(ToDoItem item)
        {
            if (_activeList == null)
            {
                Console.WriteLine("No active list to insert ToDo task.");
                PressToContinue();
                return;
            }

            _activeList.Items.Add(item);
            Console.WriteLine("\nTask inserted successfully!");
            PressToContinue();
        }

        public void UpdateListItem(int index)
        {
            if (_activeList != null)
            {
                if (index > 0 && _activeList.Items.Count >= index)
                {
                    ToDoItem item = _activeList.Items[index - 1];

                    Console.WriteLine($"\nCurrent Task Name: {item.TaskName}");
                    Console.Write("Enter new Task Name: ");
                    var newTaskName = Console.ReadLine();
                    if (newTaskName != null && !string.IsNullOrWhiteSpace(newTaskName))
                    {
                        item.TaskName = newTaskName;
                    }

                    Console.WriteLine($"\nCurrent Task Due Date: {item.DueDate}");
                    Console.Write("Enter new due date (MM/DD/YYYY HH:MM:SS)");
                    var newReadDueDate = Console.ReadLine();
                    if (DateTime.TryParse(newReadDueDate, out DateTime newDueDate))
                    {
                        item.DueDate = newDueDate;
                    }

                    Console.WriteLine($"\nCurrent Status of Task: {item.IsCompleted}");
                    Console.Write("Is the task completed(Y/N): ");
                    char newStatus = Console.ReadKey().KeyChar;
                    if (newStatus.ToString().ToUpper().Equals('Y'))
                        item.IsCompleted = true;
                    else
                        item.IsCompleted = false;

                    _activeList.Items[index - 1] = item;

                    Console.WriteLine("\nTask Successfully updated!");


                    PressToContinue();
                }
                else
                {
                    Console.WriteLine("\nImpossible to update item from list!");
                    PressToContinue();
                }
            }
        }

        public void ChangeItemStatus(int index)
        {
            if (_activeList != null)
            {
                if (index > 0 && _activeList.Items.Count >= index)
                {
                    if (_activeList.Items[index - 1].IsCompleted)
                        _activeList.Items[index - 1].IsCompleted = false;
                    else
                        _activeList.Items[index - 1].IsCompleted = true;

                    Console.WriteLine("\nItem status successfully changed!");
                    PressToContinue();
                }
                else
                {
                    Console.WriteLine("\nImpossible to change item status!");
                    PressToContinue();
                }
            }
        }

        public void RemoveListItem(int index)
        {
            if (index > 0 && _activeList?.Items.Count >= index)
            {
                _activeList.Items.RemoveAt(index - 1);
                Console.WriteLine("\nItem successfully removed!");
                PressToContinue();
            }
            else
            {
                Console.WriteLine("\nImpossible to delete task from list!");
                PressToContinue();
            }
        }

        public void SaveAndCloseActiveList()
        {
            if (_activeList != null)
            {
                SaveToDoLists();
                _activeList = null;
            }
        }

        public void CloseActiveList()
        {
            _activeList = null;
        }

        #endregion

        public static void PressToContinue()
        {
            Console.Write("Press any key to continue");
            Console.ReadKey();
        }
    }
}
