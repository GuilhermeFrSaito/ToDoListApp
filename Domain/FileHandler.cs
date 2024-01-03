using System.Text.Json;
using ToDoListApp.Models;
using System.Text.Json.Serialization;

namespace ToDoListApp.Domain
{
    internal class FileHandler
    {
        private readonly string _filePath;

        public FileHandler(string filePath)
        {
            _filePath = filePath;
        }

        public void SaveToDoLists(List<ToDoList> toDoLists)
        {
            JsonSerializerOptions options = new()
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Converters = { new DateOnlyConverter() }
            };

            string jsonString = JsonSerializer.Serialize(toDoLists, options);
            File.WriteAllText(_filePath, jsonString);
        }

        public List<ToDoList> LoadToDoLists()
        {
            if (File.Exists(_filePath))
            {
                string jsonString = File.ReadAllText(_filePath);

                JsonSerializerOptions options = new()
                {
                    Converters = { new DateOnlyConverter() }
                };

                return JsonSerializer.Deserialize<List<ToDoList>>(jsonString, options)!;
            }
            else
            {
                Console.WriteLine("\nFile not found!");
                Console.Write("Press any key to continue");
                Console.ReadKey();
                return new List<ToDoList>();
            }
        }
    }
}
