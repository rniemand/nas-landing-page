using Octokit;
using Rn.NetCore.Common.Extensions;

namespace NasLandingPage.Common.Extensions;

public static class GitClientExtensions
{
  public static bool ContainsFile(this IReadOnlyList<RepositoryContent> contents, string fileName)
  {
    return contents.Any(x =>
      x.Type.Value == ContentType.File &&
      x.Name.IgnoreCaseEquals(fileName)
    );
  }

  public static string GetHtmlFilePath(this IReadOnlyList<RepositoryContent> contents, string fileName)
  {
    var file = contents.FirstOrDefault(x =>
      x.Type.Value == ContentType.File &&
      x.Name.IgnoreCaseEquals(fileName)
    );

    return file is null ? string.Empty : file.HtmlUrl;
  }

  public static string GetRepoFilePath(this IReadOnlyList<RepositoryContent> contents, string fileName)
  {
    var file = contents.FirstOrDefault(x =>
      x.Type.Value == ContentType.File &&
      x.Name.IgnoreCaseEquals(fileName)
    );

    return file is null ? string.Empty : file.Path;
  }

  public static bool ContainsDirectory(this IReadOnlyList<RepositoryContent> contents, string name)
  {
    return contents.Any(x =>
      x.Type.Value == ContentType.Dir &&
      x.Name.IgnoreCaseEquals(name)
    );
  }

  public static RepositoryContent? GetDirectory(this IReadOnlyList<RepositoryContent> contents, string name)
  {
    var directory = contents.FirstOrDefault(x =>
      x.Type.Value == ContentType.Dir &&
      x.Name.IgnoreCaseEquals(name));

    return directory ?? null;
  }
}
