using Newtonsoft.Json;

namespace NasLandingPage.Models.FitBit;

public class FitbitActivitySummaryResponse
{
  [JsonProperty("summary")]
  public SummaryNode Summary { get; set; } = new();

  public class SummaryNode
  {
    [JsonProperty("activeScore")]
    public int ActiveScore { get; set; }

    [JsonProperty("activityCalories")]
    public int ActivityCalories { get; set; }

    [JsonProperty("caloriesBMR")]
    public int CaloriesBmr { get; set; }

    [JsonProperty("caloriesOut")]
    public int CaloriesOut { get; set; }

    [JsonProperty("distances")]
    public DistanceNode[] Distances { get; set; } = Array.Empty<DistanceNode>();

    [JsonProperty("elevation")]
    public float Elevation { get; set; }

    [JsonProperty("fairlyActiveMinutes")]
    public int FairlyActiveMinutes { get; set; }

    [JsonProperty("floors")]
    public int Floors { get; set; }

    [JsonProperty("lightlyActiveMinutes")]
    public int LightlyActiveMinutes { get; set; }

    [JsonProperty("marginalCalories")]
    public int MarginalCalories { get; set; }

    [JsonProperty("restingHeartRate")]
    public int RestingHeartRate { get; set; }

    [JsonProperty("sedentaryMinutes")]
    public int SedentaryMinutes { get; set; }

    [JsonProperty("steps")]
    public int Steps { get; set; }

    [JsonProperty("veryActiveMinutes")]
    public int VeryActiveMinutes { get; set; }
  }

  public class DistanceNode
  {
    [JsonProperty("activity")]
    public string Activity { get; set; } = string.Empty;

    [JsonProperty("distance")]
    public float Distance { get; set; }
  }
}
