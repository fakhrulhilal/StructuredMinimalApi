using Chirper.Common.Contracts;
using SUT = Chirper.Authentication.Endpoints.Login;

namespace Chirper.UnitTests.Authentication.Endpoints;

using static Testing;

[TestFixture]
public class LoginHandlerTests
{
    [Test]
    public async Task ReturnUnauthorizedWhenNoMatchingUserByUsernameAndPassword()
    {
        // arrange
        var request = CreateValidRequest();
        await using var db = MockDb();
        var tokenService = Mock<ITokenService>();

        // act
        var result = await SUT.Handle(request, db, tokenService, CancellationToken.None);

        // assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Result, Is.Not.Null.And.TypeOf<UnauthorizedHttpResult>());
    }

    [Test]
    public async Task ReturnTokenWhenMatchingUserByUsernameAndPassword()
    {
        // arrange
        var token = Generator.Random.Hash();
        var request = CreateValidRequest();
        var tokenService = Mock<ITokenService>().AlwaysValidToken(token);
        await using var db = MockDb();
        db.HasUser(request.Username, request.Password);

        // act
        var result = await SUT.Handle(request, db, tokenService, CancellationToken.None);

        // assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Result, Is.Not.Null.And.TypeOf<Ok<SUT.Response>>());
        var okResult = (Ok<SUT.Response>)result.Result;
        Assert.That(okResult.Value, Is.Not.Null);
        Assert.That(okResult.Value.Token, Is.Not.Null.And.EqualTo(token));
    }

    private static SUT.Request CreateValidRequest() => new(Generator.Person.UserName, Generator.Random.Hash());
}