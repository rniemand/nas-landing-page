namespace NasLandingPage.Repos;

public interface IContainerRepo
{
}

public class ContainerRepo : IContainerRepo
{
  private readonly IConnectionHelper _connectionHelper;

  public ContainerRepo(IConnectionHelper connectionHelper)
  {
    _connectionHelper = connectionHelper;
  }
}
