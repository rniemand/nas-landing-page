using Dapper;
using NasLandingPage.Models.Entities;
using NasLandingPage.Models.Requests;

namespace NasLandingPage.Repos;

public interface INetworkRepo
{
  Task<List<NetworkDeviceInfoEntity>> GetEnabledDevicesAsync();
  Task<int> AddDeviceAsync(AddNetworkDeviceRequest request);
  Task<int> AddDeviceClassificationAsync(ClassifyNetworkDeviceRequest request);
}

public class NetworkRepo : INetworkRepo
{
  private readonly IConnectionHelper _connectionHelper;

  public NetworkRepo(IConnectionHelper connectionHelper)
  {
    _connectionHelper = connectionHelper;
  }

  public async Task<List<NetworkDeviceInfoEntity>> GetEnabledDevicesAsync()
  {
    const string query = @"SELECT
	    nd.DeviceID,
	    nd.IsPhysical,
	    nd.IsActive,
	    nd.DeviceName,
	    nd.`Floor`,
	    nd.Room,
	    nd.RoomLocation,
	    nc.Category,
	    nc.SubCategory,
	    nc.Manufacturer,
	    nc.Model,
	    ip4.MacAddress,
	    ip4.IPv4,
	    ip4.IPv4Int,
	    ip4.`Connection`,
	    ip4.NetworkName
    FROM `NetworkDevices` nd
    LEFT JOIN `NetworkClassification` nc
	    ON nc.DeviceID = nd.DeviceID
    LEFT JOIN `NetworkIPv4` ip4
	    ON ip4.DeviceID = nd.DeviceID
    WHERE
	    nd.IsActive = true
    ORDER BY ip4.IPv4Int";
    await using var connection = _connectionHelper.GetCoreConnection();
    return (await connection.QueryAsync<NetworkDeviceInfoEntity>(query)).AsList();
  }

  public async Task<int> AddDeviceAsync(AddNetworkDeviceRequest request)
  {
    const string query = @"INSERT INTO `NetworkDevices`
      (`IsPhysical`, `IsActive`, `DeviceName`, `Floor`, `Room`, `RoomLocation`)
    VALUES
      (@IsPhysical, TRUE, @DeviceName, @Floor, @Room, @RoomLocation)";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.ExecuteAsync(query, request);
  }

  public async Task<int> AddDeviceClassificationAsync(ClassifyNetworkDeviceRequest request)
  {
    const string query = @"INSERT INTO `NetworkClassification`
      (`DeviceID`, `Category`, `SubCategory`, `Manufacturer`, `Model`)
    VALUES
      (@DeviceID, @Category, @SubCategory, @Manufacturer, @Model)";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.ExecuteAsync(query, request);
  }
}
