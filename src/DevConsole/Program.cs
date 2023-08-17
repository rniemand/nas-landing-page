using System.Net.Http.Headers;
using System.Text;
using System.Web;
using Microsoft.Extensions.DependencyInjection;
using NasLandingPage.Auth;
using NasLandingPage.Repos;
using Newtonsoft.Json;

namespace DevConsole;

internal class Program
{
  static async Task Main(string[] args)
  {
    //var ghService = DIContainer.Services.GetRequiredService<IGitHubService>();
    //var repoRepo = DIContainer.Services.GetRequiredService<IGitHubRepoRepo>();
    //var selectedRepo = await repoRepo.GetByIdAsync(388318548);

    //await ghService.SyncRepoFilesAsync(selectedRepo);

    //await ghService.SyncCoreRepoInformationAsync();

    //await ResetPkceCodesAsync();
    await RefreshAccessTokenAsync();

    Console.WriteLine();
  }

  private static async Task ResetPkceCodesAsync()
  {
    var pkce = new Pkce();
    var oAuthRepo = DIContainer.Services.GetRequiredService<IOAuthRepo>();
    await oAuthRepo.SetPkceCodesAsync(
      "FitBit",
      pkce.CodeChallenge,
      pkce.CodeVerifier
    );

    await GenerateAuthUrl();
  }

  private static async Task GenerateAuthUrl()
  {
    var oAuthRepo = DIContainer.Services.GetRequiredService<IOAuthRepo>();
    var entry = await oAuthRepo.GetOAuthEntryAsync("FitBit");
    if (entry is null) throw new Exception("Missing FitBit OAuth entry");

    var sb = new StringBuilder($"{entry.AuthUrl}?")
      .Append("response_type=code")
      .Append($"&client_id={entry.ClientID}")
      .Append("&scope=activity+cardio_fitness+electrocardiogram+heartrate+location+nutrition+oxygen_saturation+profile+respiratory_rate+settings+sleep+social+temperature+weight")
      .Append($"&code_challenge={entry.PkceCodeChallenge}")
      .Append("&code_challenge_method=S256")
      .Append($"&state={entry.State}")
      .Append($"&redirect_uri={HttpUtility.UrlEncode(entry.RedirectUri)}");

    Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>");
    Console.WriteLine("> Follow this link");
    Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>");
    Console.WriteLine();
    Console.WriteLine(sb.ToString());
    Console.WriteLine();
    Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>");
    Console.WriteLine();
    await ProcessResponseCode();
  }

  private static async Task ProcessResponseCode()
  {
    Console.WriteLine();
    Console.WriteLine("Paste in the redirection URL here: ");
    Console.Write(":> ");
    var code = Console.ReadLine();
    var parts1 = code.Split("code=");
    var boooob = parts1[1].Split("&");

    var extractedCode = boooob[0];
    var oAuthRepo = DIContainer.Services.GetRequiredService<IOAuthRepo>();
    await oAuthRepo.SetAuthorizationCodeAsync("FitBit", extractedCode);

    Console.WriteLine();
    Console.WriteLine("CODE UPDATED!");
    Console.WriteLine();

    await GetTokensAsync();
  }

  private static async Task GetTokensAsync()
  {
    var oAuthRepo = DIContainer.Services.GetRequiredService<IOAuthRepo>();
    var entry = await oAuthRepo.GetOAuthEntryAsync("FitBit");
    if (entry is null) throw new Exception("Missing FitBit OAuth entry");

    var request = new HttpRequestMessage(HttpMethod.Post, entry.TokenUrl);
    request.Content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
    {
      new("client_id", entry.ClientID),
      new("grant_type", "authorization_code"),
      new("redirect_uri", entry.RedirectUri),
      new("code", entry.AuthorizationCode),
      new("code_verifier", entry.PkceCodeVerifier),
    });

    var userName = entry.ClientID;
    var userPassword = entry.ClientSecret;
    var authenticationString = $"{userName}:{userPassword}";
    var base64String = Convert.ToBase64String(Encoding.ASCII.GetBytes(authenticationString));
    request.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64String);

    var httpClient = new HttpClient();
    var response = await httpClient.SendAsync(request);
    response.EnsureSuccessStatusCode();
    var jsonResponse = await response.Content.ReadAsStringAsync();
    var fitbitResponse = JsonConvert.DeserializeObject<GetTokensResponse>(jsonResponse);
    if (fitbitResponse is null) throw new Exception("Unable to parse fitbit response");

    await oAuthRepo.SetAccessTokensAsync("FitBit",
      fitbitResponse.AccessToken,
      fitbitResponse.RefreshToken,
      fitbitResponse.ExpiresIn
    );

    Console.WriteLine("TOKEN INFORMATION UPDATED");
  }

  private static async Task RefreshAccessTokenAsync()
  {
    var oAuthRepo = DIContainer.Services.GetRequiredService<IOAuthRepo>();
    var entry = await oAuthRepo.GetOAuthEntryAsync("FitBit");
    if (entry is null) throw new Exception("Missing FitBit OAuth entry");

    var request = new HttpRequestMessage(HttpMethod.Post, entry.TokenUrl);
    request.Content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
    {
      new("grant_type", "refresh_token"),
      new("client_id", entry.ClientID),
      new("refresh_token", entry.RefreshToken),
    });

    var userName = entry.ClientID;
    var userPassword = entry.ClientSecret;
    var authenticationString = $"{userName}:{userPassword}";
    var base64String = Convert.ToBase64String(Encoding.ASCII.GetBytes(authenticationString));
    request.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64String);

    var httpClient = new HttpClient();
    var response = await httpClient.SendAsync(request);
    response.EnsureSuccessStatusCode();
    var jsonResponse = await response.Content.ReadAsStringAsync();
    var fitbitResponse = JsonConvert.DeserializeObject<GetTokensResponse>(jsonResponse);
    if (fitbitResponse is null) throw new Exception("Unable to parse fitbit response");

    await oAuthRepo.SetAccessTokensAsync("FitBit",
      fitbitResponse.AccessToken,
      fitbitResponse.RefreshToken,
      fitbitResponse.ExpiresIn
    );
  }
}

public class GetTokensResponse
{
  [JsonProperty("access_token")]
  public string AccessToken { get; set; } = string.Empty;

  [JsonProperty("expires_in")]
  public int ExpiresIn { get; set; }

  [JsonProperty("refresh_token")]
  public string RefreshToken { get; set; } = string.Empty;

  [JsonProperty("scope")]
  public string Scope { get; set; } = string.Empty;

  [JsonProperty("token_type")]
  public string TokenType { get; set; } = string.Empty;

  [JsonProperty("user_id")]
  public string UserId { get; set; } = string.Empty;
}
