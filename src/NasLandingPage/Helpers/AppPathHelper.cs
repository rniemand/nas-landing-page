using NasLandingPage.Models;
using RnCore.Abstractions;

namespace NasLandingPage.Helpers;

public interface IAppPathHelper
{
  string ResolveImagePath(string path);
}

public class AppPathHelper : IAppPathHelper
{
  private readonly IPathAbstraction _path;
  private readonly string _imagesRoot;

  public AppPathHelper(IPathAbstraction path, AppConfig config)
  {
    _path = path;
    _imagesRoot = _path.Join(config.GameIndexRootDir, "images");
  }

  public string ResolveImagePath(string path) => _path.Join(_imagesRoot, path);
}
