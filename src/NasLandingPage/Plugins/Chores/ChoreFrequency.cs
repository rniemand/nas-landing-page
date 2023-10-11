using NasLandingPage.Exceptions;

namespace NasLandingPage.Plugins.Chores;

internal class ChoreFrequency
{
  public ChoreFrequencyInterval IntervalModifier { get; }
  public string Interval { get; set; }
  public DayOfWeek[] DaysOfWeek { get; }
  public int[] DaysOfMonth { get; }

  public ChoreFrequency(string modifier, string interval)
  {
    IntervalModifier = MapChoreFrequencyInterval(modifier);
    Interval = GetAdjustInterval(modifier, interval);
    DaysOfWeek = GetDaysOfWeek(IntervalModifier, interval);
    DaysOfMonth = GetDaysOfMonth(IntervalModifier, interval);
  }

  public DateOnly GetNextOccurrence(DateOnly date)
  {
    switch (IntervalModifier)
    {
      case ChoreFrequencyInterval.Days:
        return date.AddDays(int.Parse(Interval));
      case ChoreFrequencyInterval.Weeks:
        return date.AddDays(int.Parse(Interval) * 7);
      case ChoreFrequencyInterval.Months:
        return date.AddMonths(int.Parse(Interval));
      case ChoreFrequencyInterval.DaysOfWeek:
        return date.AddDays(GetDaysDifference(date.DayOfWeek, GetNextDayOfWeek(DaysOfWeek, date.DayOfWeek)));
      case ChoreFrequencyInterval.DaysOfMonth:
      {
        var nextDay = GetNextDayOfMonth(DaysOfMonth, date.Day);
        var nextMonth = date.AddMonths(1);
        if (date.Day == nextDay) return nextMonth;
        return date.Day > nextDay ? new DateOnly(nextMonth.Year, nextMonth.Month, nextDay) : new DateOnly(date.Year, date.Month, nextDay);
      }
      default:
        throw new NlpException($"Unsupported modifier: {IntervalModifier:G}");
    }
  }

  private static ChoreFrequencyInterval MapChoreFrequencyInterval(string modifier) =>
    modifier.ToLower().Trim() switch
    {
      "weeks" => ChoreFrequencyInterval.Weeks,
      "weekly" => ChoreFrequencyInterval.Weeks,
      "days" => ChoreFrequencyInterval.Days,
      "daily" => ChoreFrequencyInterval.Days,
      "months" => ChoreFrequencyInterval.Months,
      "monthly" => ChoreFrequencyInterval.Months,
      "daysofmonth" => ChoreFrequencyInterval.DaysOfMonth,
      "dom" => ChoreFrequencyInterval.DaysOfMonth,
      "daysofweek" => ChoreFrequencyInterval.DaysOfWeek,
      "dow" => ChoreFrequencyInterval.DaysOfWeek,
      _ => throw new NlpException($"Unsupported chore modifier: {modifier}")
    };

  private static string GetAdjustInterval(string modifier, string interval) =>
    modifier.ToLower().Trim() switch
    {
      "weekly" => "1",
      "monthly" => "1",
      "daily" => "1",
      _ => interval
    };

  private static DayOfWeek MapDayOfWeek(string value) =>
    value switch
    {
      "mon" => DayOfWeek.Monday,
      "monday" => DayOfWeek.Monday,
      "tue" => DayOfWeek.Tuesday,
      "tuesday" => DayOfWeek.Tuesday,
      "wed" => DayOfWeek.Wednesday,
      "wednesday" => DayOfWeek.Wednesday,
      "thu" => DayOfWeek.Thursday,
      "thursday" => DayOfWeek.Thursday,
      "fri" => DayOfWeek.Friday,
      "friday" => DayOfWeek.Friday,
      "sat" => DayOfWeek.Saturday,
      "saturday" => DayOfWeek.Saturday,
      "sun" => DayOfWeek.Sunday,
      "sunday" => DayOfWeek.Sunday,
      _ => throw new NlpException($"Unsupported day of week: {value}")
    };

  private static DayOfWeek[] GetDaysOfWeek(ChoreFrequencyInterval modifier, string value) =>
    modifier != ChoreFrequencyInterval.DaysOfWeek
      ? Array.Empty<DayOfWeek>()
      : value.ToLower().Trim().Split(",", StringSplitOptions.RemoveEmptyEntries).Select(MapDayOfWeek).ToArray();

  private static int[] GetDaysOfMonth(ChoreFrequencyInterval modifier, string value) =>
    modifier != ChoreFrequencyInterval.DaysOfMonth
      ? Array.Empty<int>()
      : value.ToLower().Trim().Split(",", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

  private static DayOfWeek GetNextDayOfWeek(IReadOnlyList<DayOfWeek> daysOfWeek, DayOfWeek currentDayOfWeek)
  {
    if (daysOfWeek.Count == 1) return daysOfWeek[0];
    var pastCurrentDow = daysOfWeek.Where(x => x > currentDayOfWeek).Order().ToList();
    return pastCurrentDow.Any() ? pastCurrentDow.First() : daysOfWeek.Where(x => x < currentDayOfWeek).Order().First();
  }

  private static int GetNextDayOfMonth(IReadOnlyList<int> daysOfMonth, int currentDayOfMonth)
  {
    if (daysOfMonth.Count == 1) return daysOfMonth[0];
    var futureDay = daysOfMonth.Where(x => x > currentDayOfMonth).Order().ToList();
    return futureDay.Any() ? futureDay.First() : daysOfMonth.Where(x => x < currentDayOfMonth).Order().First();
  }

  private static int GetDaysDifference(DayOfWeek a, DayOfWeek b)
  {
    if (a == b) return 7;
    if (b > a) return b - a;
    return 7 - (a - b);
  }
}
