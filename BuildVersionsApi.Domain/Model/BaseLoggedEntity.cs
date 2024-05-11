namespace BuildVersionsApi.Domain.Model
{
  public class BaseLoggedEntity : ISoftDeletable
  {
    public int Id { get; set; }

    public string? Username { get; set; }
    public DateTime? Created { get; set; }
    public DateTime? Changed { get; set; }
    public bool IsDeleted { get; set; } = false;
  }
}

public interface ISoftDeletable
{
  bool IsDeleted { get; set; }
}