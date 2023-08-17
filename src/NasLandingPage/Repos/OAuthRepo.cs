using Dapper;

namespace NasLandingPage.Repos;

public class OAuthEntity
{
  public string OAuthType { get; set; } = string.Empty;
  public string ClientID { get; set; } = string.Empty;
  public string ClientSecret { get; set; } = string.Empty;
  public string PkceCodeVerifier { get; set; } = string.Empty;
  public string PkceCodeChallenge { get; set; } = string.Empty;
  public string State { get; set; } = string.Empty;
  public string RedirectUri { get; set; } = string.Empty;
  public string AuthorizationCode { get; set; } = string.Empty;
  public string TokenUrl { get; set; } = string.Empty;
  public string AuthUrl { get; set; } = string.Empty;
  public string AccessToken { get; set; } = string.Empty;
  public string RefreshToken { get; set; } = string.Empty;
}

public interface IOAuthRepo
{
  Task<OAuthEntity> GetOAuthEntryAsync(string type);
  Task<int> SetPkceCodesAsync(string type, string challenge, string verifier);
  Task<int> SetAuthorizationCodeAsync(string type, string code);
  Task<int> SetAccessTokensAsync(string type, string accessToken, string refreshToken, int expiresIn);
}

public class OAuthRepo : IOAuthRepo
{
  public const string TableName = "OAuth";
  private readonly IConnectionHelper _connectionHelper;

  public OAuthRepo(IConnectionHelper connectionHelper)
  {
    _connectionHelper = connectionHelper;
  }

  public async Task<OAuthEntity> GetOAuthEntryAsync(string type)
  {
    const string query = $"SELECT * FROM {TableName} WHERE `OAuthType` = @OAuthType";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.QuerySingleOrDefaultAsync<OAuthEntity>(query, new { OAuthType = type });
  }

  public async Task<int> SetPkceCodesAsync(string type, string challenge, string verifier)
  {
    var query = $@"UPDATE `OAuth`
    SET
      `PkceCodeVerifier` = '{verifier}',
      `PkceCodeChallenge` = '{challenge}'
    WHERE
      `OAuthType` = '{type}'";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.ExecuteAsync(query);
  }

  public async Task<int> SetAuthorizationCodeAsync(string type, string code)
  {
    var query = $@"UPDATE `OAuth`
    SET
      `AuthorizationCode` = '{code}'
    WHERE
      `OAuthType` = '{type}'";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.ExecuteAsync(query);
  }

  public async Task<int> SetAccessTokensAsync(string type, string accessToken, string refreshToken, int expiresIn)
  {
    var query = $@"UPDATE `OAuth`
    SET
      `AccessToken` = '{accessToken}',
      `RefreshToken` = '{refreshToken}',
      `TokenExpiryDate` = DATE_ADD(CURRENT_TIMESTAMP, INTERVAL {expiresIn} SECOND)
    WHERE
      `OAuthType` = '{type}'";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.ExecuteAsync(query);
  }
}
