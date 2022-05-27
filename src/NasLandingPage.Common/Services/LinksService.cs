using Microsoft.Extensions.DependencyInjection;
using NasLandingPage.Common.Models.Responses;
using NasLandingPage.Common.Providers;
using Rn.NetCore.Common.Logging;

namespace NasLandingPage.Common.Services;

public interface IUserLinkService
{
  Task<List<UserLink>> GetAll();
  Task RegisterFollow(string linkId);
  Task<List<string>> GetCategories();
}

public class UserLinkService : IUserLinkService
{
  private readonly ILoggerAdapter<UserLinkService> _logger;
  private readonly ILinkProvider _linkProvider;

  public UserLinkService(
    ILoggerAdapter<UserLinkService> logger,
    ILinkProvider linkProvider)
  {
    _logger = logger;
    _linkProvider = linkProvider;
  }

  public async Task<List<UserLink>> GetAll() =>
    await _linkProvider.GetAll();

  public async Task RegisterFollow(string linkId)
  {
    var link = await _linkProvider.GetById(linkId);
    if (link is null)
    {
      _logger.LogWarning("Unable to find a link with id: {id}", linkId);
      return;
    }

    link.FollowCount += 1;
    await _linkProvider.Update(link);
    _logger.LogDebug("Updated follow count for: {name}", link.Name);
  }

  public async Task<List<string>> GetCategories() =>
    await _linkProvider.GetLinkCategories();
}
