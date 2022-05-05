using NasLandingPage.Common.Builders;
using NasLandingPage.Common.Clients;
using NasLandingPage.Common.Extensions;
using NasLandingPage.Common.Models.Responses.Projects;
using Octokit;
using Rn.NetCore.Common.Extensions;

namespace NasLandingPage.Common.Sync;

public interface IRootRepositoryContentInfoSync
{
  Task SyncAsync(RunCommandResponseBuilder responseBuilder, ProjectInfo projectInfo);
}

public class RepoRootInfoSync : IRootRepositoryContentInfoSync
{
  private readonly INlpGitHubClient _gitHubClient;

  public RepoRootInfoSync(INlpGitHubClient gitHubClient)
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
    SyncLicense(messages, projectInfo, contents);

    SyncDirectorySrc(messages, projectInfo, contents);
    SyncDirectoryTest(messages, projectInfo, contents);
    SyncDirectoryDocs(messages, projectInfo, contents);
    SyncDirectoryBuild(messages, projectInfo, contents);

    responseBuilder.WithMessages(messages);
  }


  // Top level files
  private static void SyncEditorConfig(ICollection<string> messages, ProjectInfo projectInfo, IReadOnlyList<RepositoryContent> contents)
  {
    // TODO: [RootRepositoryContentInfoSync.SyncEditorConfig] (TESTS) Add tests
    var filePath = contents.GetHtmlFilePath(".editorconfig");

    if (projectInfo.Scm.EditorConfig.IgnoreCaseEquals(filePath))
      return;

    projectInfo.Scm.EditorConfig = filePath;
    messages.Add($"Updated 'scm.editorConfig' to: {filePath}");
  }

  private static void SyncGitAttributes(ICollection<string> messages, ProjectInfo projectInfo, IReadOnlyList<RepositoryContent> contents)
  {
    // TODO: [RootRepositoryContentInfoSync.SyncGitAttributes] (TESTS) Add tests
    var filePath = contents.GetHtmlFilePath(".gitattributes");

    if (projectInfo.Scm.GitAttributes.IgnoreCaseEquals(filePath))
      return;

    projectInfo.Scm.GitAttributes = filePath;
    messages.Add($"Updated 'scm.gitAttributes' to: {filePath}");
  }

  private static void SyncReadme(ICollection<string> messages, ProjectInfo projectInfo, IReadOnlyList<RepositoryContent> contents)
  {
    // TODO: [RootRepositoryContentInfoSync.SyncReadme] (TESTS) Add tests
    var filePath = contents.GetHtmlFilePath("README.md");

    if (projectInfo.Scm.Readme.IgnoreCaseEquals(filePath))
      return;

    projectInfo.Scm.Readme = filePath;
    messages.Add($"Updated 'scm.readme' to: {filePath}");
  }

  private void SyncLicense(ICollection<string> messages, ProjectInfo projectInfo, IReadOnlyList<RepositoryContent> contents)
  {
    // TODO: [RootRepositoryContentInfoSync.SyncLicense] (TESTS) Add tests
    var filePath = contents.GetHtmlFilePath("LICENSE");
    var fileExists = !string.IsNullOrWhiteSpace(filePath);
    if (!fileExists) return;

    var repoFiles = _gitHubClient.GetAllContentsAsync(
      projectInfo.Repo.RepoId,
      "LICENSE"
    ).GetAwaiter().GetResult();
    
    var repoFile = repoFiles.FirstOrDefault();
    if (repoFile is null) return;

    var fileContent = repoFile.Content;
    if (string.IsNullOrWhiteSpace(fileContent)) return;
    projectInfo.Scm.LicenseUrl = filePath;
    projectInfo.Scm.LicenseUrl = "Unknown";

    if (fileContent.IgnoreCaseContains("The MIT License (MIT)"))
      projectInfo.Scm.LicenseName = "MIT";
  }



  // Top level directories
  private static void SyncDirectorySrc(ICollection<string> messages, ProjectInfo projectInfo, IReadOnlyList<RepositoryContent> contents)
  {
    // TODO: [RootRepositoryContentInfoSync.SyncDirectorySrc] (TESTS) Add tests
    var repoDirectory = contents.GetDirectory("src");
    var dirPath = repoDirectory?.HtmlUrl ?? string.Empty;

    if(projectInfo.Scm.SrcDirectory.IgnoreCaseEquals(dirPath))
      return;
    
    projectInfo.Scm.SrcDirectory = dirPath;
    messages.Add($"Setting 'directories.src' to: {dirPath}");
  }

  private static void SyncDirectoryTest(ICollection<string> messages, ProjectInfo projectInfo, IReadOnlyList<RepositoryContent> contents)
  {
    // TODO: [RootRepositoryContentInfoSync.SyncDirectoryTest] (TESTS) Add tests
    var repoDirectory = contents.GetDirectory("test");
    var dirPath = repoDirectory?.HtmlUrl ?? string.Empty;

    if (projectInfo.Scm.TestDirectory.IgnoreCaseEquals(dirPath))
      return;

    projectInfo.Scm.TestDirectory = dirPath;
    messages.Add($"Setting 'directories.test' to: {dirPath}");
  }

  private static void SyncDirectoryDocs(ICollection<string> messages, ProjectInfo projectInfo, IReadOnlyList<RepositoryContent> contents)
  {
    // TODO: [RootRepositoryContentInfoSync.SyncDirectoryDocs] (TESTS) Add tests
    var repoDirectory = contents.GetDirectory("docs");
    var dirPath = repoDirectory?.HtmlUrl ?? string.Empty;

    if (projectInfo.Scm.DocsDirectory.IgnoreCaseEquals(dirPath))
      return;

    projectInfo.Scm.DocsDirectory = dirPath;
    messages.Add($"Setting 'directories.docs' to: {dirPath}");
  }

  private static void SyncDirectoryBuild(ICollection<string> messages, ProjectInfo projectInfo, IReadOnlyList<RepositoryContent> contents)
  {
    // TODO: [RootRepositoryContentInfoSync.SyncDirectoryDocs] (TESTS) Add tests
    var repoDirectory = contents.GetDirectory(".github");
    var dirPath = repoDirectory?.HtmlUrl ?? string.Empty;

    if (projectInfo.Scm.BuildDirectory.IgnoreCaseEquals(dirPath))
      return;

    projectInfo.Scm.BuildDirectory = dirPath;
    messages.Add($"Setting 'directories.build' to: {dirPath}");
  }
}
