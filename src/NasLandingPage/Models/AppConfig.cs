namespace NasLandingPage.Models;

public class AppConfig
{
  [ConfigurationKeyName("LinkImageRootDir")]
  public string LinkImageRootDir { get; set; } = string.Empty;

  [ConfigurationKeyName("GithubTokenFilePath")]
  public string GithubTokenFilePath { get; set; } = string.Empty;

  [ConfigurationKeyName("GitHubClientName")]
  public string GitHubClientName { get; set; } = "nasLandingPage";

  [ConfigurationKeyName("gameIndexRootDir")]
  public string GameIndexRootDir { get; set; } = string.Empty;
}