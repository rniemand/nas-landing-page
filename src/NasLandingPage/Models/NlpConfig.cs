namespace NasLandingPage.Models;

public class NlpConfig
{
  [ConfigurationKeyName("LinkImageRootDir")]
  public string LinkImageRootDir { get; set; } = string.Empty;

  [ConfigurationKeyName("LinkImageFallback")]
  public string LinkImageFallback { get; set; } = string.Empty;
}
