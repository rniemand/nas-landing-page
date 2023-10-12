using NasLandingPage.Models;
using NasLandingPage.Models.Responses;

namespace NasLandingPage.Plugins.Chores;

public interface IChoreService
{
  Task<BoolResponse> AddChoreAsync(NlpUserContext userContext, HomeChoreDto chore);
  Task<BoolResponse> UpdateChoreAsync(NlpUserContext userContext, HomeChoreDto chore);
  Task<BoolResponse> RescheduleChoreAsync(NlpUserContext userContext, HomeChoreDto chore);
}

internal class ChoreService : IChoreService
{
  private readonly IChoreRepo _choreRepo;

  public ChoreService(IChoreRepo choreRepo)
  {
    _choreRepo = choreRepo;
  }

  public async Task<BoolResponse> AddChoreAsync(NlpUserContext userContext, HomeChoreDto chore)
  {
    var response = new BoolResponse();
    var rowCount = await _choreRepo.AddChoreAsync(chore);
    return rowCount == 0 ? response.AsError("Failed to add chore") : response;
  }

  public async Task<BoolResponse> UpdateChoreAsync(NlpUserContext userContext, HomeChoreDto chore)
  {
    var response = new BoolResponse();
    var rowCount = await _choreRepo.UpdateChoreAsync(chore);
    return rowCount == 0 ? response.AsError("Failed to update chore") : response;
  }

  public async Task<BoolResponse> RescheduleChoreAsync(NlpUserContext userContext, HomeChoreDto chore)
  {
    var response = new BoolResponse();
    var rowCount = await _choreRepo.RescheduleChoreAsync(chore);
    return rowCount == 0 ? response.AsError("Failed to reschedule chore") : response;
  }
}
