namespace ToDoListApp.Domain
{
    internal static class Menus
    {
        internal static void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("-------------------------------------------\n");
            Console.WriteLine("Welcome to ToDo List! \n");
            Console.WriteLine("Today is " + DateTime.Today.ToShortDateString() + "\n");
            Console.WriteLine("Please select your desired action:\n");
            Console.WriteLine("1 - Create A List For Today");
            Console.WriteLine("2 - Load All Lists From File");
            Console.WriteLine("3 - View List From Date");
            Console.WriteLine("4 - Delete List From Date");
            Console.WriteLine("5 - Select List To Work");
            Console.WriteLine("6 - Save Changes");
            Console.WriteLine("7 - Save And Exit");
            Console.WriteLine("8 - Exit Without Saving\n");
            Console.WriteLine("-------------------------------------------");

            Console.Write("Your Selection: ");
        }

        internal static void ShowListMenu(DateOnly date)
        {
            Console.WriteLine("-------------------------------------------\n");
            Console.WriteLine($"ToDo list for date {date} \n");
            Console.WriteLine("1 - Print List Items");
            Console.WriteLine("2 - Add New Entry");
            Console.WriteLine("3 - Update List Entry");
            Console.WriteLine("4 - Delete List Entry");
            Console.WriteLine("5 - Change Item Status");
            Console.WriteLine("6 - Save And Close List");
            Console.WriteLine("7 - Close List Without Saving");
            Console.WriteLine("-------------------------------------------");

            Console.Write("Your Selection: ");
        }
    }
}
