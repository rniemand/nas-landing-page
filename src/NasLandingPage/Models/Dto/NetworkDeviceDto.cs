using System.Text.Json.Serialization;
using NasLandingPage.Models.Entities;
using Newtonsoft.Json;

namespace NasLandingPage.Models.Dto;

public class NetworkDeviceDto
{
  [JsonProperty("deviceID"), JsonPropertyName("deviceID")]
  public int DeviceID { get; set; }

  [JsonProperty("isPhysical"), JsonPropertyName("isPhysical")]
  public bool IsPhysical { get; set; }

  [JsonProperty("isActive"), JsonPropertyName("isActive")]
  public bool IsActive { get; set; }

  [JsonProperty("deviceName"), JsonPropertyName("deviceName")]
  public string DeviceName { get; set; } = string.Empty;

  [JsonProperty("floor"), JsonPropertyName("floor")]
  public string? Floor { get; set; }

  [JsonProperty("room"), JsonPropertyName("room")]
  public string? Room { get; set; }

  [JsonProperty("roomLocation"), JsonPropertyName("roomLocation")]
  public string? RoomLocation { get; set; }

  [JsonProperty("classification"), JsonPropertyName("classification")]
  public NetworkDeviceClassificationDto Classification { get; set; } = new();

  [JsonProperty("ipv4"), JsonPropertyName("ipv4")]
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
  [JsonProperty("category"), JsonPropertyName("category")]
  public string Category { get; set; } = string.Empty;

  [JsonProperty("subCategory"), JsonPropertyName("subCategory")]
  public string? SubCategory { get; set; }

  [JsonProperty("manufacturer"), JsonPropertyName("manufacturer")]
  public string? Manufacturer { get; set; }

  [JsonProperty("model"), JsonPropertyName("model")]
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
  [JsonProperty("macAddress"), JsonPropertyName("macAddress")]
  public string? MacAddress { get; set; }

  [JsonProperty("ipv4"), JsonPropertyName("ipv4")]
  public string? IPv4 { get; set; }

  [JsonProperty("ipv4Int"), JsonPropertyName("ipv4Int")]
  public long IPv4Int { get; set; }

  [JsonProperty("connection"), JsonPropertyName("connection")]
  public string Connection { get; set; } = string.Empty;

  [JsonProperty("networkName"), JsonPropertyName("networkName")]
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
