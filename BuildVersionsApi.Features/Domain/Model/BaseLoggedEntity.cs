namespace BuildVersionsApi.Features.Domain.Model
{
  public class BaseLoggedEntity
  {
    //TODO: Endpoint returns 204 when not found, verify this is correct
    //TODO: Implement History table
    public int Id { get; set; }
    //TODO: Verify Username works as supposed
    public string? Username { get; set; }
    //TODO: Created becomes null after update, create works as expected
    //TODO: Username, Created, Changed should not be included in responses
    public DateTime? Created { get; set; }
    public DateTime? Changed { get; set; }
    public bool IsDeleted { get; set; }=false;
  }
}