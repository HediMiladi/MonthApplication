using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MonthApplication.Interfaces;
using MonthApplication.Models;

namespace MonthApplication.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMonthList<Month> _monthList;

        public string CurrentMonth { get; set; }
        public int MonthsCount { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IMonthList<Month> monthList)
        {
            _logger = logger;
            _monthList = monthList;
        }

        public void OnGet()
        {
            MonthsCount = _monthList.Count;
        }

        public JsonResult OnGetMonths(string value)
        {
            _ = int.TryParse(value, out int language);

            var languageToGet = (LanguageEnum)language;

            var months = _monthList
                            .Select(x => new Select2Item { Id = x.Number, Text = x.GetName(languageToGet) })
                            .ToList();


            return new JsonResult(new Select2Results
            {
                Results = months
            });
        }

        public JsonResult OnGetSearchMonthByIndex(string value, string languageValue)
        {
            _ = int.TryParse(languageValue, out int language);

            var languageToGet = (LanguageEnum)language;

            bool isValidInt =  int.TryParse(value, out int monthOrder);

            if (isValidInt)
            {
                return new JsonResult(_monthList[monthOrder - 1]?.GetName(languageToGet));
            }

            return new JsonResult("");
        }

        public JsonResult OnGetSearchMonthByName(string value)
        {
                return new JsonResult(_monthList.GetMonthByName(value)?.Number);
        }
    }
}