namespace BuildVersionsApi.Features.Model
{
  public class BaseLoggedEntity
  {
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public DateTime Changed { get; set; }
  }
}
