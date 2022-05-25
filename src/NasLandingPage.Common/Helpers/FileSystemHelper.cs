using Microsoft.Extensions.DependencyInjection;
using Rn.NetCore.Common.Abstractions;
using Rn.NetCore.Common.Helpers;

namespace NasLandingPage.Common.Helpers;

public interface IFileSystemHelper
{
  string CurrentDirectory { get; }

  // Directories
  void EnsureFolderExists(string path);
  string[] DirectoryGetFiles(string path, string searchPattern, SearchOption searchOption);

  // Files
  bool BackupFileToFolder(string source, string destination);
  List<string> ListDirectoryFiles(string folder, bool stripExtension = false);
  bool FileExists(string path);
  void WriteTextFile(string path, string contents);
  string GetFileName(string path);
  string GetFileNameWithoutExtension(string path);
  void SaveJsonFile<T>(string path, T data, bool formatJson);

  // Helpers
  TOut? LoadJsonFile<TOut>(string filePath) where TOut : class, new();
}

public class FileSystemHelper : IFileSystemHelper
{
  private readonly IEnvironmentAbstraction _environment;
  private readonly IDirectoryAbstraction _directory;
  private readonly IFileAbstraction _file;
  private readonly IPathAbstraction _path;
  private readonly IJsonHelper _jsonHelper;

  public FileSystemHelper(IServiceProvider serviceProvider)
  {
    _directory = serviceProvider.GetRequiredService<IDirectoryAbstraction>();
    _file = serviceProvider.GetRequiredService<IFileAbstraction>();
    _path = serviceProvider.GetRequiredService<IPathAbstraction>();
    _jsonHelper = serviceProvider.GetRequiredService<IJsonHelper>();
    _environment = serviceProvider.GetRequiredService<IEnvironmentAbstraction>();
  }


  // Directories
  public string CurrentDirectory => _environment.CurrentDirectory;

  public void EnsureFolderExists(string path)
  {
    if (_directory.Exists(path))
      return;

    _directory.CreateDirectory(path);
  }

  public string[] DirectoryGetFiles(string path, string searchPattern, SearchOption searchOption) =>
    _directory.GetFiles(path, searchPattern, searchOption);


  // Files
  public bool BackupFileToFolder(string source, string destination)
  {
    if (!_file.Exists(source))
    {
      return false;
    }

    if (_file.Exists(destination))
    {
      _file.Delete(destination);
    }

    _file.Move(source, destination);
    return true;
  }

  public List<string> ListDirectoryFiles(string folder, bool stripExtension = false)
  {
    var directoryFiles = _directory.GetFiles(folder, "*.json", SearchOption.TopDirectoryOnly);

    return directoryFiles.Select(file => stripExtension
        ? _path.GetFileNameWithoutExtension(file)
        : _path.GetFileName(file))
      .ToList();
  }

  public bool FileExists(string path) =>
    _file.Exists(path);

  public void WriteTextFile(string path, string contents) =>
    _file.WriteAllText(path, contents);

  public string GetFileName(string path) =>
    _path.GetFileName(path);

  public string GetFileNameWithoutExtension(string path) =>
    _path.GetFileNameWithoutExtension(path);

  public void SaveJsonFile<T>(string path, T data, bool formatJson)
  {
    var projectJson = _jsonHelper.SerializeObject(data, formatJson);
    WriteTextFile(path, projectJson);
  }


  // Helpers
  public TOut? LoadJsonFile<TOut>(string filePath) where TOut : class, new()
  {
    if (!_file.Exists(filePath))
      return null;

    var fileJson = _file.ReadAllText(filePath);
    return _jsonHelper.DeserializeObject<TOut>(fileJson);
  }
}
