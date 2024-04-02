namespace BuildVersionsApi.Domain.Model
{
  public class BaseLoggedEntity
  {
    //VERIFY Implement History table
    //VERIFY Verify Username works as supposed
    //VERIFY Created becomes null after update, create works as expected
    public int Id { get; set; }
    public string? Username { get; set; }
    public DateTime? Created { get; set; }
    public DateTime? Changed { get; set; }
    public bool IsDeleted { get; set; } = false;
  }
}