namespace NasLandingPage.Plugins.Chores;

interface IChoreService
{
}

internal class ChoreService : IChoreService
{
  private readonly IChoreRepo _choreRepo;

  public ChoreService(IChoreRepo choreRepo)
  {
    _choreRepo = choreRepo;
  }
}
