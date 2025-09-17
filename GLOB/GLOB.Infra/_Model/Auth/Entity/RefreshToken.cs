using System.ComponentModel.DataAnnotations;

namespace GLOB.Domain.Model.Auth;

public class RefreshToken
{
    [Key]
    public int Id { get; set; }

    public string Token { get; set; }
    public string JwtId { get; set; }

    public bool IsRevoked { get; set; }
    public DateTimeOffset? RevokedAt { get; set; }

    public bool IsUsed { get; set; }

    public string? CreatedByIp { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset ExpiresAt { get; set; }

    public string InfraUserId { get; set; }
    public InfraUser InfraUser { get; set; }
}

public class RefreshTokenRequest
{
  public required string AccessToken { get; set; }
  public required string RefreshToken { get; set; }
}
public class RevokeTokenRequest
{
  public required string RefreshToken { get; set; }
}
