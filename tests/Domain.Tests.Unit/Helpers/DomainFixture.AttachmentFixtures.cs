using MultiProject.Delivery.Domain.Attachments.Entities;
using MultiProject.Delivery.Domain.Attachments.ValueTypes;

namespace MultiProject.Delivery.Domain.Tests.Unit.Helpers;

public partial class DomainFixture
{
    public class AttachmentFixtures : BaseFixture
    {
        private readonly Faker<Attachment> _attachmentFaker;

        public AttachmentFixtures(DomainFixture fixture) : base(fixture)
        {
            _attachmentFaker = new Faker<Attachment>()
                               .UseSeed(87895424)
                               .CustomInstantiator(_ => DomainObjectBuilder.Create<Attachment, AttachmentId>(GetId))
                               .RuleFor(x => x.CreatorId, Fixture.Users.GetId)
                               .RuleFor(x => x.TransportId, Fixture.Transports.GetId)
                               .RuleFor(x => x.Payload, f => f.Random.Bytes(50))
                               .RuleFor(x => x.AdditionalInformation, f => f.Lorem.Sentence())
                               .RuleFor(x => x.LastUpdateDate, f => f.Date.Recent());
        }

        public byte[] Payload => Faker.Random.Bytes(50);
        public string AdditionalInformation => Faker.Lorem.Sentence();

        public AttachmentId GetId() => new(Faker.Random.Int(1, 100));

        public Attachment GetAttachment() => _attachmentFaker.Generate();
    }
}