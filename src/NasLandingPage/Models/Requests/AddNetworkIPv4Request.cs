namespace NasLandingPage.Models.Requests;

public class AddNetworkIPv4Request
{
  public int DeviceID { get; set; }
  public string? MacAddress { get; set; }
  public string? IPv4 { get; set; }
  public string Connection { get; set; } = "LAN";
  public string? NetworkName { get; set; }
}
