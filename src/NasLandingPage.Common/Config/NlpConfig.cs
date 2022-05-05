namespace NasLandingPage.Common.Config;

public class NlpConfig
{
  public string DataDir { get; set; } = "./data/";
  public bool IsLinux { get; set; } = true;
  public string PlaceHolderImage { get; set; } = "placeholder.png";
}
