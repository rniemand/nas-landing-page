namespace NasLandingPage.Models.Dto;

public class HomeRoomDto
{
  public int RoomId { get; set; }
  public int FloorId { get; set; }
  public DateTimeOffset DateAdded { get; set; } = DateTimeOffset.MinValue;
  public DateTimeOffset? DateDeleted { get; set; }
  public string RoomName { get; set; } = null!;
}
