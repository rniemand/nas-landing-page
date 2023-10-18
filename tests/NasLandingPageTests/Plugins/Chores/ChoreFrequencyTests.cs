using NasLandingPage.Enums;
using NasLandingPage.Exceptions;
using NasLandingPage.Models.Misc;

namespace NasLandingPage.Tests.Plugins.Chores;

[TestFixture]
internal class ChoreFrequencyTests
{
  [TestCase("weeks", "mon", ChoreFrequencyInterval.Weeks)]
  [TestCase("weekly", "mon", ChoreFrequencyInterval.Weeks)]
  [TestCase("days", "1", ChoreFrequencyInterval.Days)]
  [TestCase("daily", "1", ChoreFrequencyInterval.Days)]
  [TestCase("months", "1", ChoreFrequencyInterval.Months)]
  [TestCase("monthly", "1", ChoreFrequencyInterval.Months)]
  [TestCase("DaysOfMonth", "1", ChoreFrequencyInterval.DaysOfMonth)]
  [TestCase("dom", "1", ChoreFrequencyInterval.DaysOfMonth)]
  [TestCase("DaysOfWeek", "mon", ChoreFrequencyInterval.DaysOfWeek)]
  [TestCase("dow", "mon", ChoreFrequencyInterval.DaysOfWeek)]
  public void ChoreFrequency_GivenPattern_ShouldExtractDaysOfWeek(string modifier, string interval, ChoreFrequencyInterval expected) =>
    Assert.That(new ChoreFrequency(modifier, interval).IntervalModifier, Is.EqualTo(expected));

  [Test]
  public void ChoreFrequency_GivenUnknownModifier_ShouldThrow()
  {
    var ex = Assert.Throws<NlpException>(() => new ChoreFrequency("unknown", "1"));
    Assert.That(ex!.Message, Is.EqualTo("Unsupported chore modifier: unknown"));
  }

  [Test]
  public void ChoreFrequency_GivenWeekly_ShouldSetCorrectInterval() =>
    Assert.That(new ChoreFrequency("weekly", "10").Interval, Is.EqualTo("1"));

  [Test]
  public void ChoreFrequency_GivenMonthly_ShouldSetCorrectInterval() =>
    Assert.That(new ChoreFrequency("monthly", "10").Interval, Is.EqualTo("1"));

  [Test]
  public void ChoreFrequency_GivenDaily_ShouldSetCorrectInterval() =>
    Assert.That(new ChoreFrequency("daily", "10").Interval, Is.EqualTo("1"));

  [TestCase("daily", "1", "2020-01-01", "2020-01-02")]
  [TestCase("days", "5", "2020-01-01", "2020-01-06")]
  [TestCase("weekly", "1", "2020-01-01", "2020-01-08")]
  [TestCase("weeks", "2", "2020-01-01", "2020-01-15")]
  [TestCase("monthly", "1", "2020-01-01", "2020-02-01")]
  [TestCase("months", "3", "2020-01-01", "2020-04-01")]
  [TestCase("dow", "mon", "2020-01-02", "2020-01-06")]
  [TestCase("dow", "mon,sun", "2020-01-02", "2020-01-05")]
  [TestCase("dow", "thu", "2020-01-02", "2020-01-09")]
  [TestCase("dom", "1,15", "2020-01-08", "2020-01-15")]
  [TestCase("dom", "15", "2020-01-15", "2020-02-15")]
  [TestCase("dom", "15", "2020-01-14", "2020-01-15")]
  [TestCase("dom", "1,15", "2020-01-16", "2020-02-01")]
  public void GetNextOccurrence_GivenValidIntervalAndModifier_ShouldGenerateExpectedDate(string modifier, string interval, string inputDate, string expected)
  {
    // arrange
    var choreFrequency = new ChoreFrequency(modifier, interval);

    // act
    var nextOccurrence = choreFrequency.GetNextOccurrence(DateOnly.Parse(inputDate));

    // assert
    Assert.That(nextOccurrence, Is.EqualTo(DateOnly.Parse(expected)));
  }
}
