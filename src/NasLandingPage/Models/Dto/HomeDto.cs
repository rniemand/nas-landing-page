namespace NasLandingPage.Models.Dto;

public class HomeDto
{
  public int HomeId { get; set; }
  public bool DefaultHome { get; set; }
  public double Longitude { get; set; }
  public double Latitude { get; set; }
  public DateTimeOffset DateAddedUtc { get; set; } = DateTimeOffset.MinValue;
  public DateTimeOffset? DateDeletedUtc { get; set; }
  public string? Country { get; set; }
  public string? PostalCode { get; set; }
  public string? City { get; set; }
  public string? Province { get; set; }
  public string HomeName { get; set; } = null!;
  public string? AddressLine1 { get; set; }
  public string? AddressLine { get; set; }
}