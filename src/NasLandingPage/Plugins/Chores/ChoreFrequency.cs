using NasLandingPage.Exceptions;

namespace NasLandingPage.Plugins.Chores;

public class ChoreFrequency
{
  public DayOfWeek[] DaysOfWeek { get; }
  public int[] DaysOfMonth { get; }
  public int IntervalDays { get; }
  public int IntervalWeeks { get; private set; }
  public int IntervalMonths { get; }
  public bool IsValid { get; }

  public ChoreFrequency(string expression)
  {
    var args = GenerateArgsDictionary(expression);
    DaysOfWeek = ExtractDaysOfWeek(args, "dow");
    DaysOfMonth = ExtractIntArrayKey(args, "dom");
    IntervalDays = ExtractIntKey(args, "d");
    IntervalWeeks = ExtractIntKey(args, "w");
    IntervalMonths = ExtractIntKey(args, "m");
    IsValid = ValidateConfiguration(expression);
  }

  public DateTimeOffset GetNextOccurrenceFrom(DateTimeOffset date)
  {

    return date.AddDays(IntervalDays);
  }

  private bool ValidateConfiguration(string expression)
  {
    // If we don't have any intervals but do have days of week defined we can assume this is weekly
    if (IntervalDays == 0 && IntervalWeeks == 0 && IntervalMonths == 0 && DaysOfWeek.Length > 0)
      IntervalWeeks = 1;

    // ReSharper disable once ConvertIfStatementToSwitchStatement
    if (IntervalDays == 0 && IntervalWeeks == 0 && IntervalMonths == 0)
      throw new NlpException($"Invalid expression: {expression}");

    if (IntervalDays > 0 && IntervalWeeks > 0)
      throw new NlpException("Cannot mix 'd:' and 'w:' expressions");

    if (IntervalDays > 0 && IntervalMonths > 0)
      throw new NlpException("Cannot mix 'd:' and 'm:' expressions");

    if (IntervalMonths > 0 && IntervalWeeks > 0)
      throw new NlpException("Cannot mix 'w:' and 'm:' expressions");

    return true;
  }

  private static int ExtractIntKey(IReadOnlyDictionary<string, string> args, string key) =>
    args.ContainsKey(key) ? int.Parse(args[key]) : 0;

  private static DayOfWeek[] ExtractDaysOfWeek(IReadOnlyDictionary<string, string> args, string key)
  {
    if (!args.ContainsKey(key)) return Array.Empty<DayOfWeek>();
    var dayOfWeeks = new List<DayOfWeek>();
    foreach (var dow in args[key].Split(",", StringSplitOptions.RemoveEmptyEntries))
    {
      switch (dow.ToLower().Trim())
      {
        case "mon": dayOfWeeks.Add(DayOfWeek.Monday); break;
        case "tue": dayOfWeeks.Add(DayOfWeek.Tuesday); break;
        case "wed": dayOfWeeks.Add(DayOfWeek.Wednesday); break;
        case "thu": dayOfWeeks.Add(DayOfWeek.Thursday); break;
        case "fri": dayOfWeeks.Add(DayOfWeek.Friday); break;
        case "sat": dayOfWeeks.Add(DayOfWeek.Saturday); break;
        case "sun": dayOfWeeks.Add(DayOfWeek.Sunday); break;
        default: throw new NlpException($"Invalid DoW value: {dow}");
      }
    }

    return dayOfWeeks.ToArray();
  }

  private static int[] ExtractIntArrayKey(IReadOnlyDictionary<string, string> args, string key) =>
    args.ContainsKey(key)
      ? args[key].Split(",", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray()
      : Array.Empty<int>();

  private static Dictionary<string, string> GenerateArgsDictionary(string expression)
  {
    var mapped = new Dictionary<string, string>();
    var parts = expression.ToLower().Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);
    foreach (var part in parts)
    {
      if (!part.Contains(':')) throw new NlpException($"Invalid frequency expression: {expression}");
      var subParts = part.Split(':');
      mapped[subParts[0]] = subParts[1];
    }
    return mapped;
  }
}
