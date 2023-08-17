using System.Net.Http.Headers;
using NasLandingPage.Models.FitBit;
using NasLandingPage.Repos;
using Newtonsoft.Json;

namespace NasLandingPage.Services;

public interface IFitBitService
{
  Task<FitbitActivitySummaryResponse.SummaryNode> GetFitbitActivitySummaryAsync(DateOnly date);
}

public class FitBitService : IFitBitService
{
  private readonly IOAuthRepo _oAuthRepo;
  private const string BaseUrl = "https://api.fitbit.com";
  private OAuthEntity? _oAuthEntity;
  private readonly HttpClient _httpClient = new();

  public FitBitService(IOAuthRepo oAuthRepo)
  {
    _oAuthRepo = oAuthRepo;
  }

  public async Task<FitbitActivitySummaryResponse.SummaryNode> GetFitbitActivitySummaryAsync(DateOnly date)
  {
    await InitializeClientAsync();
    
    var url = "{BaseUrl}/1/user/-/activities/date/{date}.json"
      .Replace("{BaseUrl}", BaseUrl)
      .Replace("{date}", date.ToString("O"));

    var request = new HttpRequestMessage(HttpMethod.Get, url);
    var response = await _httpClient.SendAsync(request);
    response.EnsureSuccessStatusCode();
    var responseJson = await response.Content.ReadAsStringAsync();
    var parsed = JsonConvert.DeserializeObject<FitbitActivitySummaryResponse>(responseJson);
    if (parsed is null) throw new Exception("Failed to parse response");
    return parsed.Summary;
  }

  private async Task InitializeClientAsync()
  {
    if (_oAuthEntity is not null) return;
    _oAuthEntity = await GetClientInfoAsync();
    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
      "Bearer",
      _oAuthEntity.AccessToken
    );
  }

  private async Task<OAuthEntity> GetClientInfoAsync() =>
    await _oAuthRepo.GetOAuthEntryAsync("FitBit");
}
