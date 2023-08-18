namespace NasLandingPage.Models.Entities;

public class NetworkDeviceInfoEntity
{
  public int DeviceID { get; set; }
  public bool IsPhysical { get; set; }
  public bool IsActive { get; set; }
  public string DeviceName { get; set; } = string.Empty;
  public string? Floor { get; set; }
  public string? Room { get; set; }
  public string? RoomLocation { get; set; }
  public string Category { get; set; } = string.Empty;
  public string? SubCategory { get; set; }
  public string? Manufacturer { get; set; }
  public string? Model { get; set; }
  public string? MacAddress { get; set; }
  public string? IPv4 { get; set; }
  public long IPv4Int { get; set; }
  public string Connection { get; set; } = string.Empty;
  public string? NetworkName { get; set; }
}
