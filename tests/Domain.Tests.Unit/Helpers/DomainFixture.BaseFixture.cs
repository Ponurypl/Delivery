namespace MultiProject.Delivery.Domain.Tests.Unit.Helpers;

public partial class DomainFixture
{
    public abstract class BaseFixture
    {
        protected readonly Faker Faker;
        protected readonly DomainFixture Fixture;

        protected BaseFixture(DomainFixture fixture)
        {
            Faker = fixture.Faker;
            Fixture = fixture;
        }
    }
}