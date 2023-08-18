using Dapper;
using NasLandingPage.Models.Entities;

namespace NasLandingPage.Repos;

public interface INetworkRepo
{
  Task<List<NetworkDeviceInfoEntity>> GetEnabledDevicesAsync();
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
}
