using Microsoft.Extensions.DependencyInjection;
using NasLandingPage.Common.Config;
using NasLandingPage.Common.Helpers;

namespace NasLandingPage.Common.Providers;

public interface ILinkImageProvider
{
  string ResolveImagePath(string imageName);
}

public class LinkImageProvider : ILinkImageProvider
{
  private readonly IFileSystemHelper _fsHelper;
  private readonly NasLandingPageConfig _config;
  private readonly string _dataDir;

  public LinkImageProvider(IServiceProvider serviceProvider)
  {
    _fsHelper = serviceProvider.GetRequiredService<IFileSystemHelper>();
    _config = serviceProvider.GetRequiredService<INasLandingPageConfigProvider>().Provide();
    _dataDir = GenerateDataDirPath();
  }


  public string ResolveImagePath(string imageName)
  {
    // TODO: [LinkImageProvider.ResolveImagePath] (TESTS) Add tests
    var imagePath = $"{_dataDir}{imageName}";

    return _fsHelper.FileExists(imagePath)
      ? imagePath
      : $"{_dataDir}{_config.PlaceHolderImage}";
  }


  private string GenerateDataDirPath()
  {
    // TODO: [LinkImageProvider.GenerateDataDirPath] (TESTS) Add tests
    var sep = _config.IsLinux ? "/" : "\\";
    var rootDir = _fsHelper.CurrentDirectory;

    if (!rootDir.EndsWith(sep))
      rootDir += sep;

    var processed = _config.DataDir
      .Replace("./", rootDir);

    if (!processed.EndsWith(sep))
      processed += sep;

    processed += $"link-img{sep}";
    _fsHelper.EnsureFolderExists(processed);

    return processed;
  }
}
