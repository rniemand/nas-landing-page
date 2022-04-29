using Octokit;
using Rn.NetCore.Common.Extensions;

namespace NasLandingPage.Common.Extensions;

public static class GitClientExtensions
{
  public static bool ContainsFile(this IReadOnlyList<RepositoryContent> contents, string fileName)
  {
    // TODO: [GitClientExtensions.ContainsFile] (TESTS) Add tests
    return contents.Any(x =>
      x.Type.Value == ContentType.File &&
      x.Name.IgnoreCaseEquals(fileName)
    );
  }

  public static string GetFilePath(this IReadOnlyList<RepositoryContent> contents, string fileName)
  {
    // TODO: [GitClientExtensions.GetFilePath] (TESTS) Add tests
    var file = contents.FirstOrDefault(x =>
      x.Type.Value == ContentType.File &&
      x.Name.IgnoreCaseEquals(fileName)
    );

    return file is null ? string.Empty : file.HtmlUrl;
  }
}
