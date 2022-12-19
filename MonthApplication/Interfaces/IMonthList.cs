using System.Collections;
using MonthApplication.Models;

namespace MonthApplication.Interfaces
{
    public interface IMonthList<T> : IList<T> 
    {
        void SetLanguage(LanguageEnum language);

        Month? GetMonthByName(string monthName);
    }
}
