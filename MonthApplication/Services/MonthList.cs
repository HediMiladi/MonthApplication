using System.Collections;
using MonthApplication.Interfaces;
using MonthApplication.Models;

namespace MonthApplication.Services
{
    public class MonthList<T> : IMonthList<T> where T : Month
    {
        private readonly ICalendarService calendarService;

        public MonthList(ICalendarService calendarService)
        {
            this.calendarService = calendarService;
        }

        public T this[int index] { get => calendarService.GetMonth(index + 1) as T; set => calendarService.SetMonth(index, value); }

        public int Count => calendarService.Count();

        public bool IsReadOnly => false;
        public void SetLanguage(LanguageEnum language)
        {
            calendarService.SetLanguage(language);
        }

        public void Clear()
        {
            calendarService.SetCurrentMonth(null);
        }

        public bool Contains(T item)
        {
            return calendarService.GetMonth(item.Number) != null;
        }


        public IEnumerator<T> GetEnumerator()
        {
            while (true)
            {
                yield return (T) calendarService.GetCurrentMonth();
                if (calendarService.GetNextMonth() == null)
                {
                    break;
                }
                calendarService.SetCurrentMonth(calendarService.GetNextMonth());
            }
        }
        public int IndexOf(T item)
        {
            return item.Number - 1;
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }


        public Month? GetMonthByName(string monthName)
        {
            return calendarService.GetMonthByName(monthName);
        }


        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
        public void Add(T item)
        {
            throw new NotImplementedException();
        }
        public void Insert(int index, T item)
        {
            throw new NotImplementedException();
        }
    }
}
