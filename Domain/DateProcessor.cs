using System.Globalization;

namespace ToDoListApp.Domain
{
    internal static class DateProcessor
    {
        internal static DateOnly GetSystemDate()
        {
            DateOnly newDate = DateOnly.Parse(DateTime.Today.ToShortDateString(), CultureInfo.CurrentCulture.DateTimeFormat);

            return newDate;
        }
    }
}
