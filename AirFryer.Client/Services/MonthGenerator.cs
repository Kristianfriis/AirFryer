using System.Globalization;

namespace AirFryer.Client.Services;

public class Month
{
    public int Year { get; set; }
    public int MonthNumber { get; set; }
    public string MonthName { get; set; }
    public List<Week> Weeks { get; set; }
}

public class Week
{
    public int WeekNumber { get; set; }
    public List<int> Dates { get; set; }
}

public static class MonthGenerator
{
    public static List<Month> GenerateMonths(int year)
    {
        List<Month> months = new List<Month>();

        for (int month = 1; month <= 12; month++)
        {
            DateTime firstDayOfMonth = new DateTime(year, month, 1);
            int daysInMonth = DateTime.DaysInMonth(year, month);

            List<Week> weeks = new List<Week>();
            int currentWeekNumber = 1;

            for (int day = 1; day <= daysInMonth; day++)
            {
                DateTime currentDate = new DateTime(year, month, day);
                int weekNumber = GetIso8601WeekNumber(currentDate);

                if (weekNumber > currentWeekNumber)
                {
                    weeks.Add(new Week
                    {
                        WeekNumber = currentWeekNumber,
                        Dates = new List<int>()
                    });

                    currentWeekNumber = weekNumber;
                }

                weeks[^1].Dates.Add(day);
            }

            months.Add(new Month
            {
                Year = year,
                MonthNumber = month,
                MonthName = firstDayOfMonth.ToString("MMMM"),
                Weeks = weeks
            });
        }

        return months;
    }

    // Function to get ISO-8601 week number
    static int GetIso8601WeekNumber(DateTime date)
    {
        DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(date);
        if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
        {
            date = date.AddDays(3);
        }

        return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
    }
}
