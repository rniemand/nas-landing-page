using Microsoft.Extensions.DependencyInjection;
using NasLandingPage.Common.Config;
using NasLandingPage.Common.Helpers;
using NasLandingPage.Common.Models.Responses;
using Rn.NetCore.Common.Extensions;
using Rn.NetCore.Metrics;
using Rn.NetCore.Metrics.Builders;

namespace NasLandingPage.Common.Providers;

public interface ILinkProvider
{
  Task AddLink(UserLink link);
  Task<List<UserLink>> GetAll();
  Task<UserLink?> GetById(string linkId);
}

public class LinkProvider : ILinkProvider
{
  private readonly IFileSystemHelper _fsHelper;
  private readonly IMetricService _metrics;
  private readonly NlpConfig _config;
  private readonly string _dataDir;
  private readonly string _backupDir;

  public LinkProvider(IServiceProvider serviceProvider)
  {
    _fsHelper = serviceProvider.GetRequiredService<IFileSystemHelper>();
    _metrics = serviceProvider.GetRequiredService<IMetricService>();
    _config = serviceProvider.GetRequiredService<INasLandingPageConfigProvider>().Provide();

    _dataDir = GenerateDataDirPath();
    _backupDir = GenerateBackupDirPath();
  }


  public async Task AddLink(UserLink link)
  {
    var builder = GetMetricBuilder(nameof(AddLink));

    try
    {
      using (builder.WithTiming())
      {
        link.LinkId = Guid.NewGuid();
        var filePath = GenerateLinkFilePath(link.LinkId);
        await Task.CompletedTask;
        _fsHelper.SaveJsonFile(filePath, link, true);
      }
    }
    catch (Exception ex)
    {
      builder.WithException(ex);
      throw;
    }
    finally
    {
      await _metrics.SubmitBuilderAsync(builder);
    }
  }

  public async Task<List<UserLink>> GetAll()
  {
    var builder = GetMetricBuilder(nameof(GetAll));

    try
    {
      using (builder.WithTiming())
      {
        var linkFiles = _fsHelper.DirectoryGetFiles(_dataDir, "*.json", SearchOption.TopDirectoryOnly);
        await Task.CompletedTask;

        return GetEnabledLinks(linkFiles.ToList())
          .OrderBy(x => x.Order)
          .ToList();
      }
    }
    catch (Exception ex)
    {
      builder.WithException(ex);
      throw;
    }
    finally
    {
      await _metrics.SubmitBuilderAsync(builder);
    }
  }

  public async Task<UserLink?> GetById(string linkId)
  {
    var builder = GetMetricBuilder(nameof(GetById));

    try
    {
      using (builder.WithTiming())
      {
        var linkFilePath = GenerateLinkFilePath(linkId);
        return _fsHelper.LoadJsonFile<UserLink>(linkFilePath);
      }
    }
    catch (Exception ex)
    {
      builder.WithException(ex);
      throw;
    }
    finally
    {
      await _metrics.SubmitBuilderAsync(builder);
    }
  }


  private List<UserLink> GetEnabledLinks(List<string> filePaths)
  {
    var builder = GetMetricBuilder(nameof(GetEnabledLinks));

    try
    {
      using (builder.WithTiming())
      {
        var enabledLinks = new List<UserLink>();

        foreach (var filePath in filePaths)
        {
          var parsedLink = _fsHelper.LoadJsonFile<UserLink>(filePath);

          if (parsedLink is null)
            continue;

          enabledLinks.Add(parsedLink);
        }

        return enabledLinks;
      }
    }
    catch (Exception ex)
    {
      builder.WithException(ex);
      throw;
    }
    finally
    {
      _metrics.SubmitBuilder(builder);
    }
  }

  private ServiceMetricBuilder GetMetricBuilder(string method)
    => new(nameof(LinkProvider), method);

  private string GenerateLinkFilePath(Guid linkId) =>
    GenerateLinkFilePath(linkId.ToString("N"));

  private string GenerateLinkFilePath(string linkId)
  {
    var cleanLinkId = linkId.Replace("-", "").LowerTrim();
    return $"{_dataDir}{cleanLinkId}.json";
  }

  private string GenerateDataDirPath()
  {
    var sep = _config.IsLinux ? "/" : "\\";
    var rootDir = _fsHelper.CurrentDirectory;

    if (!rootDir.EndsWith(sep))
      rootDir += sep;

    var processed = _config.DataDir
      .Replace("./", rootDir);

    if (!processed.EndsWith(sep))
      processed += sep;

    processed += $"links{sep}";
    _fsHelper.EnsureFolderExists(processed);

    return processed;
  }

  private string GenerateBackupDirPath()
  {
    var sep = _config.IsLinux ? "/" : "\\";
    var rootDir = _fsHelper.CurrentDirectory;

    if (!rootDir.EndsWith(sep))
      rootDir += sep;

    var processed = _config.DataDir
      .Replace("./", rootDir);

    if (!processed.EndsWith(sep))
      processed += sep;

    processed += $"links-backup{sep}";
    _fsHelper.EnsureFolderExists(processed);

    return processed;
  }
}
