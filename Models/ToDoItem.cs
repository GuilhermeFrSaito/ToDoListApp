namespace ToDoListApp.Models
{
    internal class ToDoItem
    {
        public string TaskName { get; set; } = string.Empty;

        public DateTime DueDate { get; set; }

        public bool IsCompleted { get; set; } = false;
    }
}
