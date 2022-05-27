using Microsoft.Extensions.DependencyInjection;
using NasLandingPage.Common.Models.Responses;
using NasLandingPage.Common.Providers;

namespace NasLandingPage.Common.Services;

public interface IUserLinkService
{
  Task<List<UserLink>> GetAll();
}

public class UserLinkService : IUserLinkService
{
  private readonly ILinkProvider _linkProvider;

  public UserLinkService(IServiceProvider serviceProvider) =>
    _linkProvider = serviceProvider.GetRequiredService<ILinkProvider>();

  public async Task<List<UserLink>> GetAll() =>
    await _linkProvider.GetAll();
}
