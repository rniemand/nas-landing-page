using NasLandingPage.Common.Builders;
using NasLandingPage.Common.Clients;
using NasLandingPage.Common.Extensions;
using NasLandingPage.Common.Models.Responses;
using Octokit;

namespace NasLandingPage.Common.Sync;

public interface IRootRepositoryContentInfoSync
{
  Task SyncAsync(RunCommandResponseBuilder responseBuilder, ProjectInfo projectInfo);
}

public class RootRepositoryContentInfoSync : IRootRepositoryContentInfoSync
{
  private readonly INlpGitHubClient _gitHubClient;

  public RootRepositoryContentInfoSync(INlpGitHubClient gitHubClient)
  {
    _gitHubClient = gitHubClient;
  }

  public async Task SyncAsync(RunCommandResponseBuilder responseBuilder, ProjectInfo projectInfo)
  {
    // TODO: [RootRepositoryContentInfoSync.SyncAsync] (TESTS) Add tests
    var contents = await _gitHubClient.GetAllContentsAsync(projectInfo.Repo.RepoId);
    var messages = new List<string>();

    SyncEditorConfig(messages, projectInfo, contents);
    SyncGitAttributes(messages, projectInfo, contents);
    SyncReadme(messages, projectInfo, contents);

    SyncHasDirSrc(messages, projectInfo, contents);
    SyncHasDirTests(messages, projectInfo, contents);
    SyncHasDirDocs(messages, projectInfo, contents);
    SyncHasDirBuild(messages, projectInfo, contents);
    SyncHasDirDotGithub(messages, projectInfo, contents);

    responseBuilder.WithMessages(messages);
  }

  // Top level files
  private static void SyncEditorConfig(ICollection<string> messages, ProjectInfo projectInfo, IReadOnlyList<RepositoryContent> contents)
  {
    // TODO: [RootRepositoryContentInfoSync.SyncEditorConfig] (TESTS) Add tests
    var filePath = contents.GetHtmlFilePath(".editorconfig");
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
    // TODO: [RootRepositoryContentInfoSync.SyncGitAttributes] (TESTS) Add tests
    var filePath = contents.GetHtmlFilePath(".gitattributes");
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
    // TODO: [RootRepositoryContentInfoSync.SyncReadme] (TESTS) Add tests
    var filePath = contents.GetHtmlFilePath("README.md");
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
    // TODO: [RootRepositoryContentInfoSync.SyncHasDirSrc] (TESTS) Add tests
    var hasDirectory = contents.ContainsDirectory("src");

    if (projectInfo.Folders.Src == hasDirectory)
      return;

    messages.Add("Setting Src to: " + (hasDirectory ? "true" : "false"));
    projectInfo.Folders.Src = hasDirectory;
  }

  private static void SyncHasDirTests(ICollection<string> messages, ProjectInfo projectInfo, IReadOnlyList<RepositoryContent> contents)
  {
    // TODO: [RootRepositoryContentInfoSync.SyncHasDirTests] (TESTS) Add tests
    var hasDirectory = contents.ContainsDirectory("tests");

    if (projectInfo.Folders.Tests == hasDirectory)
      return;

    messages.Add("Setting Tests to: " + (hasDirectory ? "true" : "false"));
    projectInfo.Folders.Tests = hasDirectory;
  }

  private static void SyncHasDirDocs(ICollection<string> messages, ProjectInfo projectInfo, IReadOnlyList<RepositoryContent> contents)
  {
    // TODO: [RootRepositoryContentInfoSync.SyncHasDirDocs] (TESTS) Add tests
    var hasDirectory = contents.ContainsDirectory("docs");

    if (projectInfo.Folders.Docs == hasDirectory)
      return;

    messages.Add("Setting Docs to: " + (hasDirectory ? "true" : "false"));
    projectInfo.Folders.Docs = hasDirectory;
  }

  private static void SyncHasDirBuild(ICollection<string> messages, ProjectInfo projectInfo, IReadOnlyList<RepositoryContent> contents)
  {
    // TODO: [RootRepositoryContentInfoSync.SyncHasDirBuild] (TESTS) Add tests
    var hasDirectory = contents.ContainsDirectory("build");

    if (projectInfo.Folders.Build == hasDirectory)
      return;

    messages.Add("Setting Build to: " + (hasDirectory ? "true" : "false"));
    projectInfo.Folders.Build = hasDirectory;
  }

  private static void SyncHasDirDotGithub(ICollection<string> messages, ProjectInfo projectInfo, IReadOnlyList<RepositoryContent> contents)
  {
    // TODO: [RootRepositoryContentInfoSync.SyncHasDirDotGithub] (TESTS) Add tests
    var hasDirectory = contents.ContainsDirectory(".github");

    if (projectInfo.Folders.DotGithub == hasDirectory)
      return;

    messages.Add("Setting DotGithub to: " + (hasDirectory ? "true" : "false"));
    projectInfo.Folders.DotGithub = hasDirectory;
  }
}
