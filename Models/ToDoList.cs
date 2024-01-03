using System.Text.Json.Serialization;

namespace ToDoListApp.Models
{
    internal class ToDoList
    {
        public List<ToDoItem> Items { get; set; }
        public DateOnly Date { get; set; }

        public ToDoList()
        {
            Items = new List<ToDoItem>();
            Date = DateOnly.FromDateTime(DateTime.Today);
        }

        public ToDoList(DateOnly date)
        {
            Items = new List<ToDoItem>();
            Date = date;
        }

        [JsonConstructor]
        public ToDoList(List<ToDoItem> items, DateOnly date)
        {
            Items = items;
            Date = date;
        }
    }
}
