using NasLandingPage.Models.Entities;

namespace NasLandingPage.Models.Dto;

public class NetworkDeviceDto
{
  public int DeviceID { get; set; }
  public bool IsPhysical { get; set; }
  public bool IsActive { get; set; }
  public string DeviceName { get; set; } = string.Empty;
  public string? Floor { get; set; }
  public string? Room { get; set; }
  public string? RoomLocation { get; set; }
  public NetworkDeviceClassificationDto Classification { get; set; } = new();
  public List<NetworkDeviceIPv4EntryDto> IPv4 { get; set; } = new();

  public static NetworkDeviceDto FromEntity(NetworkDeviceInfoEntity entity) => new()
  {
    DeviceID = entity.DeviceID,
    IsActive = entity.IsActive,
    DeviceName = entity.DeviceName,
    Floor = entity.Floor,
    Room = entity.Room,
    RoomLocation = entity.RoomLocation,
    IsPhysical = entity.IsPhysical,
    Classification = NetworkDeviceClassificationDto.FromEntity(entity),
  };
}

public class NetworkDeviceClassificationDto
{
  public string Category { get; set; } = string.Empty;
  public string? SubCategory { get; set; }
  public string? Manufacturer { get; set; }
  public string? Model { get; set; }

  public static NetworkDeviceClassificationDto FromEntity(NetworkDeviceInfoEntity entity) => new()
  {
    Category = entity.Category,
    Manufacturer = entity.Manufacturer,
    Model = entity.Model,
    SubCategory = entity.SubCategory,
  };
}

public class NetworkDeviceIPv4EntryDto
{
  public string? MacAddress { get; set; }
  public string? IPv4 { get; set; }
  public long IPv4Int { get; set; }
  public string Connection { get; set; } = string.Empty;
  public string? NetworkName { get; set; }

  public static NetworkDeviceIPv4EntryDto FromEntity(NetworkDeviceInfoEntity entity) => new()
  {
    Connection = entity.Connection,
    NetworkName = entity.NetworkName,
    MacAddress = entity.MacAddress,
    IPv4Int = entity.IPv4Int,
    IPv4 = entity.IPv4,
  };
}
