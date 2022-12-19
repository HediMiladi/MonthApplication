using Microsoft.Extensions.Caching.Memory;
using MonthApplication.Interfaces;
using MonthApplication.Models;

namespace MonthApplication.Services
{
    public class CalendarService : ICalendarService
    {
        private const string MonthCacheKey = "MonthKey";

        private readonly IMemoryCache _memoryCache;

        public CalendarService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            InitializeMonths();
        }

        private Month? CurrentMonth { get; set; }

        private LanguageEnum Language { get; set; } = LanguageEnum.English;

        private void InitializeMonths()
        {
            if (!_memoryCache.TryGetValue(MonthCacheKey, out Month cacheValue))
            {
                Month december = new(12, "december", "décembre", null);
                Month november = new(11, "november", "novembre", december);
                Month october = new(10, "october", "octobre", november);
                Month september = new(9, "september", "septembre", october);
                Month august = new(8, "august", "aout", september);
                Month july = new(7, "july", "juillet", august);
                Month june = new(6, "june", "juin", july);
                Month may = new(5, "may", "mai", june);
                Month april = new(4, "april", "avril", may);
                Month march = new(3, "march", "mars", april);
                Month february = new(2, "february", "fevrier", march);
                Month january = new(1, "january", "janvier", february);

                december.PreviousMonth = november;
                november.PreviousMonth = october;
                october.PreviousMonth = september;
                september.PreviousMonth = august;
                august.PreviousMonth = july;
                july.PreviousMonth = june;
                june.PreviousMonth = may;
                may.PreviousMonth = april;
                april.PreviousMonth = march;
                march.PreviousMonth = february;
                february.PreviousMonth = january;

                cacheValue = january;
                _memoryCache.Set(MonthCacheKey, cacheValue);
            }

            CurrentMonth = cacheValue;
        }

        public void SetLanguage(LanguageEnum language)
        {
            Language = language;
        }

        public void SetCurrentMonth(Month? month)
        {
            CurrentMonth = month;
        }

        public string? GetMonthName()
        {
            return CurrentMonth?.GetName(Language);
        }

        public Month? GetCurrentMonth()
        {
            return CurrentMonth;
        }

        public Month? GetNextMonth()
        {
            return CurrentMonth?.NextMonth;
        }

        public Month? GetPreviousMonth()
        {
            return CurrentMonth?.PreviousMonth;
        }

        public int Count()
        {
            bool isLeftSide = true;

            int count = 0;

            while (true)
            {
                if (isLeftSide && CurrentMonth?.PreviousMonth == null)
                {
                    isLeftSide = false;
                }

                if (!isLeftSide)
                {
                    count++;
                }

                if (!isLeftSide && CurrentMonth?.NextMonth == null)
                {
                    break;
                }

                CurrentMonth = isLeftSide ? CurrentMonth?.PreviousMonth : CurrentMonth?.NextMonth;
            }

            return count;
        }

        private void UpdateMonthInCache()
        {
            _memoryCache.Set(MonthCacheKey, CurrentMonth);
        }

        public void SetMonth(int index, Month value)
        {
            var monthToUpdate = GetMonth(index + 1);
            monthToUpdate.EnglishName = value.EnglishName;
            monthToUpdate.FrenchName = value.FrenchName;
            UpdateMonthInCache();
        }

        public Month? GetMonth(int order)
        {
            bool isLeftSide = true;

            Month? month = null;

            while (true)
            {
                if (CurrentMonth?.Number == order)
                {
                    month = CurrentMonth;
                    break;
                }

                if (isLeftSide && CurrentMonth?.PreviousMonth == null)
                {
                    isLeftSide = false;
                }

                if (!isLeftSide && CurrentMonth?.NextMonth == null)
                {
                    break;
                }

                CurrentMonth = isLeftSide ? CurrentMonth?.PreviousMonth : CurrentMonth?.NextMonth;
            }

            return month;
        }

        public Month? GetMonthByName(string monthName)
        {
            bool isLeftSide = true;

            Month? month = null;

            while (true)
            {
                if (monthName.Equals(CurrentMonth?.FrenchName, StringComparison.OrdinalIgnoreCase) 
                    || monthName.Equals(CurrentMonth?.EnglishName, StringComparison.OrdinalIgnoreCase))
                {
                    month = CurrentMonth;
                    break;
                }

                if (isLeftSide && CurrentMonth?.PreviousMonth == null)
                {
                    isLeftSide = false;
                }

                if (!isLeftSide && CurrentMonth?.NextMonth == null)
                {
                    break;
                }

                CurrentMonth = isLeftSide ? CurrentMonth?.PreviousMonth : CurrentMonth?.NextMonth;
            }

            return month;
        }

        public string GetMonthName(int order)
        {
            return GetMonth(order)?.GetName(Language) ?? Language switch
            {
                LanguageEnum.English => "Not found",
                _ => "pas trouvé"
            };
        }
    }
}