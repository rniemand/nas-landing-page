using Microsoft.AspNetCore.Mvc;
using NasLandingPage.Models.Dto;
using NasLandingPage.Services;

namespace NasLandingPage.Controllers;

[ApiController]
[Route("[controller]")]
public class GameReceiptController : ControllerBase
{
  private readonly IGameReceiptService _receiptService;

  public GameReceiptController(IGameReceiptService receiptService)
  {
    _receiptService = receiptService;
  }

  [HttpGet("order-info/{receiptId:int}")]
  public async Task<GameReceiptDto?> GetOrderInformation([FromRoute] int receiptId) =>
    await _receiptService.GetByIDAsync(receiptId);

  [HttpPost("update")]
  public async Task<GameReceiptDto?> UpdateReceipt([FromBody] GameReceiptDto gameReceipt) =>
    await _receiptService.UpdateAsync(gameReceipt);

  [HttpGet("create/game-id/{gameId:long}")]
  public async Task<GameReceiptDto?> AddReceipt([FromRoute] long gameId) =>
    await _receiptService.AddReceiptAsync(gameId);

  [HttpGet("search/term/{term}")]
  public async Task<List<GameReceiptDto>> Search([FromRoute] string term) =>
    await _receiptService.SearchAsync(term);

  [HttpPatch("associate/game-id/{gameId:long}/receipt-id/{receiptId:int}")]
  public async Task<GameReceiptDto?> AssociateReceiptToGame(
    [FromRoute] long gameId,
    [FromRoute] int receiptId) =>
    await _receiptService.AssociateGameReceiptAsync(gameId, receiptId);
}
