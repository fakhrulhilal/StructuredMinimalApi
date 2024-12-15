using Chirper.Common.Contracts;
using Chirper.Data.Types;

namespace Chirper.UnitTests.Mocks;

using Mocked = ITokenService;
using static Testing;

internal static class TokenServiceMocks
{
    public static Mocked AlwaysValidToken(this Mocked svc, string? token = null)
    {
        svc.GenerateToken(Arg.Any<User>()).ReturnsForAnyArgs(token ?? Generator.Random.Hash());
        return svc;
    }
}