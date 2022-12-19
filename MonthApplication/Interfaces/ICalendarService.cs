using MonthApplication.Models;

namespace MonthApplication.Interfaces
{
    public interface ICalendarService
    {     
        int Count();
        Month? GetCurrentMonth();
        Month? GetMonth(int order);
        string? GetMonthName();
        string GetMonthName(int order);
        Month? GetNextMonth();
        Month? GetPreviousMonth();
        void SetCurrentMonth(Month? month);
        void SetMonth(int index, Month value);
        void SetLanguage(LanguageEnum language);

        Month? GetMonthByName(string monthName);
    }
}