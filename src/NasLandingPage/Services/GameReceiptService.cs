using NasLandingPage.Models.Dto;
using NasLandingPage.Repos;

namespace NasLandingPage.Services;

public interface IGameReceiptService
{
  Task<GameReceiptDto?> GetByIDAsync(int receiptId);
  Task<GameReceiptDto?> UpdateAsync(GameReceiptDto gameReceipt);
  Task<GameReceiptDto?> AddReceiptAsync(long gameId);
  Task<List<GameReceiptDto>> SearchAsync(string term);
  Task<GameReceiptDto?> AssociateGameReceiptAsync(long gameId, int receiptId);
}

public class GameReceiptService : IGameReceiptService
{
  private readonly IGameReceiptRepo _gameReceiptRepo;

  public GameReceiptService(IGameReceiptRepo gameReceiptRepo)
  {
    _gameReceiptRepo = gameReceiptRepo;
  }

  public async Task<GameReceiptDto?> GetByIDAsync(int receiptId) =>
    await GetSingleOrDefaultAsync(receiptId);

  public async Task<GameReceiptDto?> UpdateAsync(GameReceiptDto gameReceipt)
  {
    // TODO: (GameReceiptService.UpdateAsync) [HANDLE] handle when nothing is updated
    var numRows = await _gameReceiptRepo.UpdateAsync(gameReceipt.ToEntity());
    if (numRows < 0) throw new Exception("Failed to update receipt");
    return await GetSingleOrDefaultAsync(gameReceipt.ReceiptID);
  }

  public async Task<GameReceiptDto?> AddReceiptAsync(long gameId)
  {
    var dbReceipt = await _gameReceiptRepo.GetByGameIDAsync(gameId);
    if (dbReceipt is not null) return GameReceiptDto.FromEntity(dbReceipt);

    var numRows = await _gameReceiptRepo.CreateNewReceiptAsync();
    if (numRows < 1) throw new Exception("Unable to create a new receipt");

    numRows = await _gameReceiptRepo.AssociateNewReceiptWithGameAsync(gameId);
    if (numRows < 1) throw new Exception("Unable to associate receipt with game");

    dbReceipt = await _gameReceiptRepo.GetByGameIDAsync(gameId);
    return dbReceipt is not null ? GameReceiptDto.FromEntity(dbReceipt) : null;
  }

  public async Task<List<GameReceiptDto>> SearchAsync(string term)
  {
    var dbReceipts = await _gameReceiptRepo.SearchReceiptsAsync(term);
    return dbReceipts.Select(GameReceiptDto.FromEntity).ToList();
  }

  public async Task<GameReceiptDto?> AssociateGameReceiptAsync(long gameId, int receiptId)
  {
    // TODO: (GameReceiptService.AssociateGameReceiptAsync) [HANDLE] handle this better
    var numRows = await _gameReceiptRepo.AssociateGameReceiptAsync(gameId, receiptId);
    if (numRows < 1) throw new Exception("Unable to associate game with receipt");
    return await GetSingleOrDefaultAsync(receiptId);
  }


  // Internal methods
  private async Task<GameReceiptDto?> GetSingleOrDefaultAsync(int receiptId)
  {
    var gameOrderInfoEntity = await _gameReceiptRepo.GetByIDAsync(receiptId);
    return gameOrderInfoEntity is null ? null : GameReceiptDto.FromEntity(gameOrderInfoEntity);
  }
}
