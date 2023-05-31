namespace NasLandingPage.Models;

public class AppConfig
{
  [ConfigurationKeyName("LinkImageRootDir")]
  public string LinkImageRootDir { get; set; } = string.Empty;
}
