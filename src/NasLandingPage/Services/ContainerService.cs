using NasLandingPage.Repos;

namespace NasLandingPage.Services;

public interface IContainerService
{
}

public class ContainerService : IContainerService
{
  private readonly IContainerRepo _containerRepo;

  public ContainerService(IContainerRepo containerRepo)
  {
    _containerRepo = containerRepo;
  }
}
