using NasLandingPage.Plugins.Chores;

namespace NasLandingPage.Tests.Plugins.Chores;

[TestFixture]
internal class ChoreFrequencyTests
{
  [TestCase("m:1 dow:mon", new[] { DayOfWeek.Monday })]
  [TestCase("dow:tue,wed", new[] { DayOfWeek.Tuesday, DayOfWeek.Wednesday })]
  [TestCase("w:1 dow:thu,fri,sun", new[] { DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Sunday })]
  [TestCase("w:1 dow:mon,sat", new[] { DayOfWeek.Monday, DayOfWeek.Saturday })]
  [TestCase("w:1", new DayOfWeek[0])]
  public void ChoreFrequency_GivenPattern_ShouldExtractDaysOfWeek(string pattern, DayOfWeek[] expected) =>
    Assert.That(new ChoreFrequency(pattern).DaysOfWeek, Is.EqualTo(expected));

  [TestCase("w:1 dom:1,15", new[] { 1, 15 })]
  [TestCase("w:2", new int[0])]
  public void ChoreFrequency_GivenPattern_ShouldExtractDaysOfMonth(string pattern, int[] expected) =>
    Assert.That(new ChoreFrequency(pattern).DaysOfMonth, Is.EqualTo(expected));

  [TestCase("d:5", 5)]
  [TestCase("w:5", 0)]
  public void ChoreFrequency_GivenPattern_ShouldExtractIntervalDays(string pattern, int expected) =>
    Assert.That(new ChoreFrequency(pattern).IntervalDays, Is.EqualTo(expected));

  [TestCase("w:5", 5)]
  [TestCase("d:5", 0)]
  public void ChoreFrequency_GivenPattern_ShouldExtractIntervalWeeks(string pattern, int expected) =>
    Assert.That(new ChoreFrequency(pattern).IntervalWeeks, Is.EqualTo(expected));

  [TestCase("d:5", 0)]
  [TestCase("m:5", 5)]
  public void ChoreFrequency_GivenPattern_ShouldExtractIntervalMonths(string pattern, int expected) =>
    Assert.That(new ChoreFrequency(pattern).IntervalMonths, Is.EqualTo(expected));

  [TestCase("m:5", true)]
  public void ChoreFrequency_GivenPattern_ShouldSetIsValid(string pattern, bool expected) =>
    Assert.That(new ChoreFrequency(pattern).IsValid, Is.EqualTo(expected));
}
