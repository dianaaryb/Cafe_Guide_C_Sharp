namespace App.DTO.v1_0.Identity;

public class JWTResponse
{
    public string Jwt { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string UserId { get; set; } = null!;
}