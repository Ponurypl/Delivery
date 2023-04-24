using MultiProject.Delivery.Domain.Common.ValueTypes;

namespace MultiProject.Delivery.Domain.Tests.Unit.Helpers;

public partial class DomainFixture
{
    protected readonly Faker Faker = new();

    public UserFixtures Users => new(this);
    public ScanFixtures Scans => new(this);
    public AttachmentFixtures Attachments => new(this);
    public TransportUnitsFixtures TransportUnits => new(this);
    public TransportFixtures Transports => new(this);
    public UnitOfMeasuresFixtures UnitOfMeasures => new(this);
    public RecipientFixtures Recipients => new(this);
    public LocationFixtures Locations => new(this);
    

    public DomainFixture()
    {
        Randomizer.Seed = new Random(78956987);
    }
}