using ToDoListApp.Domain;

char menuChoice;

do
{
    Menus.ShowMainMenu();

    menuChoice = Console.ReadKey().KeyChar;

    ChoiceProcessor.ProcessListsChoice(menuChoice);

} while (menuChoice != '7' && menuChoice != '8');