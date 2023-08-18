namespace NasLandingPage.Models.Requests;

public class AddNetworkDeviceRequest
{
  public bool IsPhysical { get; set; }
  public string DeviceName { get; set; } = string.Empty;
  public string? Floor { get; set; }
  public string? Room { get; set; }
  public string? RoomLocation { get; set; }
}
