namespace ToDoListApp.Domain
{
    internal static class ChoiceProcessor
    {
        private static readonly ToDoListManager _manager;

        static ChoiceProcessor()
        {
            // TODO: file selector
            _manager = new ToDoListManager("./ToDoLists.txt");
        }

        internal static void ProcessListsChoice(char choice)
        {
            switch (choice)
            {
                case '1':
                    _manager.CreateToDoList(DateProcessor.GetSystemDate());
                    break;
                case '2':
                    _manager.LoadToDoLists();
                    break;
                case '3':
                    Console.Clear();
                    Console.Write($"Type desired date to view list: ");
                    DateOnly dateView;
                    if (DateOnly.TryParse(Console.ReadLine(), out dateView))
                        _manager.PrintList(dateView);
                    else
                        InvalidDate();
                    break;
                case '4':
                    Console.Clear();
                    Console.Write("Please enter Date to delete List: ");
                    DateOnly dateDelete;
                    if (DateOnly.TryParse(Console.ReadLine(), out dateDelete))
                        _manager.DeleteList(dateDelete);
                    else
                        InvalidDate();
                    break;
                case '5':
                    Console.Clear();
                    Console.Write("Please enter date do select list: ");
                    DateOnly dateSelect;
                    if (DateOnly.TryParse(Console.ReadLine(), out dateSelect))
                    {
                        _manager.SelectList(dateSelect);
                        if (_manager.HasActiveList())
                        {
                            _manager.ShowActiveList();
                            char listChoice;
                            do
                            {
                                Menus.ShowListMenu(dateSelect);
                                listChoice = Console.ReadKey().KeyChar;

                                ProcessActiveListChoice(listChoice);

                            } while (listChoice != '6' && listChoice != '7');
                        }
                    }
                    else InvalidDate();
                    break;
                case '6':
                    _manager.SaveToDoLists();
                    break;
                case '7':
                    _manager.SaveToDoLists();
                    Console.WriteLine("\nLeaving ToDo Lists App");
                    break;
                case '8':
                    Console.WriteLine("\nLeaving ToDo Lists App");
                    break;
                default:
                    InvalidChoice();
                    break;
            }
        }

        internal static void ProcessActiveListChoice(char choice)
        {
            switch (choice)
            {
                case '1':
                    _manager.ShowActiveList();
                    break;
                case '2':
                    Console.Clear();
                    Console.Write("Enter Task Name or Description: ");
                    string taskName = Console.ReadLine()!;
                    if (taskName == null || string.IsNullOrEmpty(taskName) || string.IsNullOrWhiteSpace(taskName))
                    {
                        Console.WriteLine("\nTask cannot be empty.");
                        PressToContinue();
                        break;
                    }
                    Console.Write("Enter Due Date for Task (date time): ");
                    DateTime taskDueDate;
                    if (!DateTime.TryParse(Console.ReadLine(), out taskDueDate))
                    {
                        InvalidDateTime();
                        break;
                    }
                    Console.Write("Is the Task completed (Y/N)? ");
                    bool taskStatus;
                    char isCompleted = Console.ReadKey().KeyChar;
                    if (isCompleted.ToString().ToUpper().Equals('Y'))
                        taskStatus = true;
                    else
                        taskStatus = false;

                    _manager.AddToDoItem(new() { TaskName = taskName, DueDate = taskDueDate, IsCompleted = taskStatus });
                    _manager.ShowActiveList();
                    break;
                case '3':
                    Console.Write("\nEnter number of task to update: ");
                    int updateChoice;
                    try
                    {
                        updateChoice = int.Parse(Console.ReadLine()!);

                    }
                    catch (Exception)
                    {
                        Console.Error.WriteLine("Error accessing list entry! Entry number must contain only numbers.");
                        break;
                    }

                    _manager.UpdateListItem(updateChoice);
                    _manager.ShowActiveList();
                    break;
                case '4':
                    Console.WriteLine("Enter number of task to delete: ");
                    int deleteIndex;
                    try
                    {
                        deleteIndex = int.Parse(Console.ReadLine()!);
                    }
                    catch (Exception)
                    {
                        Console.Error.WriteLine("Invalid entry number! Entry number must contain only numbers.");
                        break;
                    }
                    _manager.RemoveListItem(deleteIndex);
                    break;
                case '5':
                    Console.Write("\nEnter Task Id for Status change: ");
                    int statusChangeIndex;
                    try
                    {
                        statusChangeIndex = int.Parse(Console.ReadLine()!);
                    }
                    catch (Exception)
                    {
                        Console.Error.WriteLine("Invalid task number! Must contain only numbers.");
                        break;
                    }
                    _manager.ChangeItemStatus(statusChangeIndex);
                    break;
                case '6':
                    _manager.SaveToDoLists();
                    break;
                case '7':
                    Console.WriteLine("\nLeaving ToDo Lists App");
                    break;
                default:
                    InvalidChoice();
                    break;
            }
        }

        private static void InvalidChoice()
        {
            Console.WriteLine();
            Console.WriteLine("Invalid selection. Please select a valid option");
            PressToContinue();
        }

        private static void InvalidDate()
        {
            Console.WriteLine();
            Console.WriteLine("Invalid date. Please type correct date format.");
            PressToContinue();
        }

        private static void InvalidDateTime()
        {
            Console.WriteLine();
            Console.WriteLine("Invalid date or time.");
            PressToContinue();
        }

        public static void PressToContinue()
        {
            Console.Write("Press any key to continue");
            Console.ReadKey();
        }
    }
}
