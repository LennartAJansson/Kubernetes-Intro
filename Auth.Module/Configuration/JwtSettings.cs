namespace Auth.Module.Configuration;

public sealed class JwtSettings
{
  public string Issuer { get; set; } = string.Empty;
  public string Secret { get; set; } = string.Empty;
  public int ExpirationInDays { get; set; }
  public bool RequireDigit { get; set; } = false;
  public int RequiredLength { get; set; } = 6;
  public bool RequireNonAlphanumeric { get; set; } = false;
  public bool RequireUppercase { get; set; } = false;
  public bool RequireLowercase { get; set; } = false;
}