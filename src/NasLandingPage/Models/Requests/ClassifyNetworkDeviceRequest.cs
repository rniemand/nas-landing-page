namespace NasLandingPage.Models.Requests;

public class ClassifyNetworkDeviceRequest
{
  public int DeviceID { get; set; }
  public string Category { get; set; } = string.Empty;
  public string? SubCategory { get; set; }
  public string? Manufacturer { get; set; }
  public string? Model { get; set; }
}
