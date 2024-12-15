namespace Chirper.Common.Contracts;

public interface ITokenService
{
    string GenerateToken(User user);
}