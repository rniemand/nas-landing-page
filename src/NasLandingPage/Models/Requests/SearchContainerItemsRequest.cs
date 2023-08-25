namespace NasLandingPage.Models.Requests;

public class SearchContainerItemsRequest
{
  public string? Category { get; set; }
  public string? SubCategory { get; set; }
  public string Term { get; set; }
}
