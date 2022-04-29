using Microsoft.Extensions.DependencyInjection;
using Rn.NetCore.Common.Abstractions;

namespace NasLandingPage.Common.Helpers;

public interface IFileSystemHelper
{
  void EnsureFolderExists(string path);
}

public class FileSystemHelper : IFileSystemHelper
{
  private readonly IDirectoryAbstraction _directory;

  public FileSystemHelper(IServiceProvider serviceProvider)
  {
    _directory = serviceProvider.GetRequiredService<IDirectoryAbstraction>();
  }


  public void EnsureFolderExists(string path)
  {
    // TODO: [FileSystemHelper.EnsureFolderExists] (TESTS) Add tests
    if (_directory.Exists(path))
      return;

    _directory.CreateDirectory(path);
  }
}
