namespace NasLandingPage.Common.Database.Entities;

public class ScmEntity
{
  public int ScmId { get; set; }
  public int ProjectId { get; set; }
  public bool Deleted { get; set; }
  public bool SrcDir { get; set; }
  public bool TestDir { get; set; }
  public bool DocsDir { get; set; }
  public bool BuildDir { get; set; }
  public bool Readme { get; set; }
  public bool PrTemplate { get; set; }
  public bool EditorConfig { get; set; }
  public bool CiInfo { get; set; }
  public bool BuildScript { get; set; }
  public bool TestScript { get; set; }
  public bool GitAttributes { get; set; }
  public string CiVersion { get; set; } = string.Empty;
  public string License { get; set; } = string.Empty;
  public string LicenseUrl { get; set; } = string.Empty;
  public string CiFlows { get; set; } = "[]";
  public string JsonDirectories { get; set; } = "{}";
  public string JsonFiles { get; set; } = "{}";
}
