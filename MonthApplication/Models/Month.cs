namespace MonthApplication.Models
{
    public class Month
    {
        public Month(int number, string englishName, string frenchName, Month? nextMonth)
        {
            Number = number;
            EnglishName = englishName;
            FrenchName = frenchName;
            NextMonth = nextMonth;
        }

        public int Number { get; set; }

        public string EnglishName { get; set; }

        public string FrenchName { get; set; }

        public Month? NextMonth { get; set; }

        public Month? PreviousMonth { get; set; }

        public string GetName(LanguageEnum language) => language switch
        {            
            LanguageEnum.French => FrenchName,
            _ => EnglishName,
        };
    }
}

