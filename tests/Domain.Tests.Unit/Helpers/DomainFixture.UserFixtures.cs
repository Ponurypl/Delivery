using MultiProject.Delivery.Domain.Users.Entities;
using MultiProject.Delivery.Domain.Users.Enums;
using MultiProject.Delivery.Domain.Users.ValueTypes;

namespace MultiProject.Delivery.Domain.Tests.Unit.Helpers;

public partial class DomainFixture
{
    public class UserFixtures : BaseFixture
    {
        private readonly Faker<User> _userFaker;

        public UserFixtures(DomainFixture fixture) : base(fixture)
        {
            _userFaker = new Faker<User>()
                         .UseSeed(147852369)
                         .CustomInstantiator(_ => DomainObjectBuilder.Create<User, UserId>(GetId))
                         .RuleFor(x => x.Username, f => f.Internet.UserName())
                         .RuleFor(x => x.Password, f => f.Internet.Password())
                         .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber())
                         .RuleFor(x => x.IsActive, true);
        }

        public string Username => Faker.Internet.UserName();
        public string Password => Faker.Internet.Password();
        public string PhoneNumber => Faker.Phone.PhoneNumber();

        public UserId GetId() => new(Faker.Random.Guid());

        public User GetUser(UserRole role = UserRole.Deliverer) => _userFaker.RuleFor(x => x.Role, role).Generate();
    }
}