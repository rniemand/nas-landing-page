using NasLandingPage.Models;
using NasLandingPage.Models.Responses;

namespace NasLandingPage.Plugins.Chores;

public interface IChoreService
{
  Task<BoolResponse> AddChoreAsync(NlpUserContext userContext, HomeChoreDto chore);
  Task<BoolResponse> UpdateChoreAsync(NlpUserContext userContext, HomeChoreDto chore);
  Task<BoolResponse> CompleteChoreAsync(NlpUserContext userContext, CompleteChoreRequest request);
  Task<HomeChoreDto[]> GetChoresAsync(NlpUserContext userContext);
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
    chore.DateScheduled = DateOnly.FromDateTime(DateTime.Now).AddDays(-1);
    var rowCount = await _choreRepo.AddChoreAsync(chore);
    return rowCount == 0 ? response.AsError("Failed to add chore") : response;
  }

  public async Task<BoolResponse> UpdateChoreAsync(NlpUserContext userContext, HomeChoreDto chore)
  {
    var response = new BoolResponse();
    var dbChore = await _choreRepo.GetChoreByIdAsync(chore.ChoreId);
    if (dbChore is null) return response.AsError("Failed to find chore in DB");
    var rowCount = await _choreRepo.UpdateChoreAsync(chore);
    return rowCount == 0 ? response.AsError("Failed to update chore") : response;
  }

  public async Task<BoolResponse> CompleteChoreAsync(NlpUserContext userContext, CompleteChoreRequest request)
  {
    var response = new BoolResponse();

    // Fetch the chore from the DB
    var dbChore = await _choreRepo.GetChoreByIdAsync(request.Chore.ChoreId);
    if (dbChore is null) return response.AsError("Invalid chore ID");

    // Reschedule the chore
    var doNow = DateOnly.FromDateTime(DateTime.Now);
    dbChore.DateLastCompleted = doNow;
    dbChore.DateScheduled = new ChoreFrequency(dbChore).GetNextOccurrence(doNow);
    var rowCount = await _choreRepo.RescheduleChoreAsync(dbChore);
    if (rowCount == 0) return response.AsError("Failed to reschedule chore");

    // Track the completed chore
    rowCount = await _choreRepo.AddChoreCompletedEntryAsync(new HomeChoreHistoryDto
    {
      ChoreId = dbChore.ChoreId,
      Points = dbChore.ChorePoints,
      UserId = request.CompletedBy,
    });

    return rowCount == 0 ? response.AsError("Chore rescheduled, failed to insert history record") : response;
  }

  public async Task<HomeChoreDto[]> GetChoresAsync(NlpUserContext userContext) => (await _choreRepo.GetChoresAsync()).ToArray();
}
