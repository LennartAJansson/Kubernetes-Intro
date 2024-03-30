namespace BuildVersionsApi.Features.Domain.Model
{
  public class BaseLoggedEntity
  {
    public int Id { get; set; }
    public string? Username { get; set; }
    public DateTime? Created { get; set; }
    public DateTime? Changed { get; set; }
  }
}