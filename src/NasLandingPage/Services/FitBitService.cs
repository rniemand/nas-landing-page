using System.Net.Http.Headers;
using NasLandingPage.Models.FitBit;
using NasLandingPage.Repos;
using Newtonsoft.Json;

namespace NasLandingPage.Services;

public interface IFitBitService
{
  Task SyncFitbitActivitySummaryAsync(int userId, DateOnly date);
}

public class FitBitService : IFitBitService
{
  private readonly IOAuthRepo _oAuthRepo;
  private readonly IFitBitSummaryDataRepo _fitBitSummaryDataRepo;
  private const string BaseUrl = "https://api.fitbit.com";
  private OAuthEntity? _oAuthEntity;
  private readonly HttpClient _httpClient = new();

  public FitBitService(IOAuthRepo oAuthRepo, IFitBitSummaryDataRepo fitBitSummaryDataRepo)
  {
    _oAuthRepo = oAuthRepo;
    _fitBitSummaryDataRepo = fitBitSummaryDataRepo;
  }

  public async Task SyncFitbitActivitySummaryAsync(int userId, DateOnly date)
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

    await _fitBitSummaryDataRepo.DeleteDatedEntryAsync(userId, date);
    await _fitBitSummaryDataRepo.AddEntryAsync(new FitBitSummaryDataEntity
    {
      CaloriesOut = parsed.Summary.CaloriesOut,
      Date = date.ToDateTime(TimeOnly.MinValue),
      Distance = parsed.Summary.Distances.FirstOrDefault(x=>x.Activity== "total")?.Distance ?? 0,
      Elevation = parsed.Summary.Elevation,
      Floors = parsed.Summary.Floors,
      LightlyActiveMinutes = parsed.Summary.LightlyActiveMinutes,
      MarginalCalories = parsed.Summary.MarginalCalories,
      RestingHeartRate = parsed.Summary.RestingHeartRate,
      SedentaryMinutes = parsed.Summary.SedentaryMinutes,
      Steps = parsed.Summary.Steps,
      UserId = userId,
      VeryActiveMinutes = parsed.Summary.VeryActiveMinutes,
    });
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
