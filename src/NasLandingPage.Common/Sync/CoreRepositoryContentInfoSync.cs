using NasLandingPage.Common.Extensions;
using NasLandingPage.Common.Models.Responses;
using Octokit;

namespace NasLandingPage.Common.Sync;

public static class CoreRepositoryContentInfoSync
{
  public static List<string> Sync(ProjectInfo projectInfo, IReadOnlyList<RepositoryContent> contents)
  {
    // TODO: [CoreRepositoryContentInfoSync.Sync] (TESTS) Add tests
    var messages = new List<string>();

    SyncEditorConfig(messages, projectInfo, contents);
    SyncGitAttributes(messages, projectInfo, contents);
    SyncReadme(messages, projectInfo, contents);

    SyncHasDirSrc(messages, projectInfo, contents);
    SyncHasDirTests(messages, projectInfo, contents);
    SyncHasDirDocs(messages, projectInfo, contents);
    SyncHasDirBuild(messages, projectInfo, contents);

    return messages;
  }


  // Top level files
  private static void SyncEditorConfig(ICollection<string> messages, ProjectInfo projectInfo, IReadOnlyList<RepositoryContent> contents)
  {
    // TODO: [CoreRepositoryContentInfoSync.SyncEditorConfig] (TESTS) Add tests
    var filePath = contents.GetFilePath(".editorconfig");
    var fileExists = !string.IsNullOrWhiteSpace(filePath);

    if (projectInfo.Scm.HasEditorConfig != fileExists)
    {
      messages.Add("Updated HasEditorConfig to: " + (fileExists ? "true" : "false"));
      projectInfo.Scm.HasEditorConfig = fileExists;
    }

    if (projectInfo.Scm.EditorConfig != filePath)
    {
      messages.Add($"Updated .editorconfig file path: {filePath}");
      projectInfo.Scm.EditorConfig = filePath;
    }
  }

  private static void SyncGitAttributes(ICollection<string> messages, ProjectInfo projectInfo, IReadOnlyList<RepositoryContent> contents)
  {
    // TODO: [CoreRepositoryContentInfoSync.SyncGitAttributes] (TESTS) Add tests
    var filePath = contents.GetFilePath(".gitattributes");
    var fileExists = !string.IsNullOrWhiteSpace(filePath);

    if (projectInfo.Scm.HasGitAttributes != fileExists)
    {
      messages.Add("Updated HasGitAttributes to: " + (fileExists ? "true" : "false"));
      projectInfo.Scm.HasGitAttributes = fileExists;
    }

    if (projectInfo.Scm.GitAttributes != filePath)
    {
      messages.Add($"Updated .gitattributes file path: {filePath}");
      projectInfo.Scm.GitAttributes = filePath;
    }
  }

  private static void SyncReadme(ICollection<string> messages, ProjectInfo projectInfo, IReadOnlyList<RepositoryContent> contents)
  {
    // TODO: [CoreRepositoryContentInfoSync.SyncReadme] (TESTS) Add tests
    var filePath = contents.GetFilePath("README.md");
    var fileExists = !string.IsNullOrWhiteSpace(filePath);

    if (projectInfo.Scm.HasReadme != fileExists)
    {
      messages.Add("Updated HasReadme to: " + (fileExists ? "true" : "false"));
      projectInfo.Scm.HasReadme = fileExists;
    }

    if (projectInfo.Scm.Readme != filePath)
    {
      messages.Add($"Updated README.md file path: {filePath}");
      projectInfo.Scm.Readme = filePath;
    }
  }


  // Top level directories
  private static void SyncHasDirSrc(ICollection<string> messages, ProjectInfo projectInfo, IReadOnlyList<RepositoryContent> contents)
  {
    // TODO: [CoreRepositoryContentInfoSync.SyncHasDirSrc] (TESTS) Add tests
    var hasDirectory = contents.ContainsDirectory("src");

    if (projectInfo.Folders.Src == hasDirectory)
      return;

    messages.Add("Setting Src to: " + (hasDirectory ? "true" : "false"));
    projectInfo.Folders.Src = hasDirectory;
  }

  private static void SyncHasDirTests(ICollection<string> messages, ProjectInfo projectInfo, IReadOnlyList<RepositoryContent> contents)
  {
    // TODO: [CoreRepositoryContentInfoSync.SyncHasDirTests] (TESTS) Add tests
    var hasDirectory = contents.ContainsDirectory("tests");

    if (projectInfo.Folders.Tests == hasDirectory)
      return;

    messages.Add("Setting Tests to: " + (hasDirectory ? "true" : "false"));
    projectInfo.Folders.Tests = hasDirectory;
  }

  private static void SyncHasDirDocs(ICollection<string> messages, ProjectInfo projectInfo, IReadOnlyList<RepositoryContent> contents)
  {
    // TODO: [CoreRepositoryContentInfoSync.SyncHasDirDocs] (TESTS) Add tests
    var hasDirectory = contents.ContainsDirectory("docs");

    if (projectInfo.Folders.Docs == hasDirectory)
      return;

    messages.Add("Setting Docs to: " + (hasDirectory ? "true" : "false"));
    projectInfo.Folders.Docs = hasDirectory;
  }

  private static void SyncHasDirBuild(ICollection<string> messages, ProjectInfo projectInfo, IReadOnlyList<RepositoryContent> contents)
  {
    // TODO: [CoreRepositoryContentInfoSync.SyncHasDirBuild] (TESTS) Add tests
    var hasDirectory = contents.ContainsDirectory("build");

    if (projectInfo.Folders.Build == hasDirectory)
      return;

    messages.Add("Setting Build to: " + (hasDirectory ? "true" : "false"));
    projectInfo.Folders.Build = hasDirectory;
  }
}
