using Chirper.Data;
using Microsoft.EntityFrameworkCore;

namespace Chirper.UnitTests.Mocks;

using static Testing;
using Mocked = AppDbContext;

internal static class AppDbContextMocks
{
    private sealed class Factory : IDbContextFactory<Mocked>
    {
        public Mocked CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<Mocked>()
                .UseInMemoryDatabase($"db-tests-{DateTime.Now.ToFileTime()}")
                .Options;
            return new(options);
        }
    }

    public static Mocked Create() => new Factory().CreateDbContext();

    public static Mocked HasUser(this Mocked svc, string username, string password)
    {
        svc.Users.Add(new()
        {
            Username = username,
            Password = password,
            DisplayName = Generator.Person.FullName
        });
        svc.SaveChanges();
        return svc;
    }
}