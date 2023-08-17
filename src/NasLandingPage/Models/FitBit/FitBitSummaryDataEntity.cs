namespace NasLandingPage.Models.FitBit;

public class FitBitSummaryDataEntity
{
  public int UserId { get; set; }
  public DateTime Date { get; set; }
  public int CaloriesOut { get; set; }
  public float Distance { get; set; }
  public float Elevation { get; set; }
  public int Floors { get; set; }
  public int LightlyActiveMinutes { get; set; }
  public int MarginalCalories { get; set; }
  public int RestingHeartRate { get; set; }
  public int SedentaryMinutes { get; set; }
  public int Steps { get; set; }
  public int VeryActiveMinutes { get; set; }
}
