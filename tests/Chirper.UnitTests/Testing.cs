using Bogus;
using Chirper.Data;

namespace Chirper.UnitTests;

internal static class Testing
{
    public static readonly Faker Generator = new();

    public static T Mock<T>() where T : class => Substitute.For<T>();
    public static AppDbContext MockDb() => AppDbContextMocks.Create();
}