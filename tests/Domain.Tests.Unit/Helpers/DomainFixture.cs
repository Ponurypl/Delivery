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

    public DomainFixture()
    {
        Randomizer.Seed = new Random(78956987);
    }
    
 	public class Locations
    {
        //prawdopodobnie dane nie przejdą przez validator-y
        public static AdvancedGeolocation GetAdvancedGeolocation()
            => new Faker<AdvancedGeolocation>()
               .CustomInstantiator(f => new AdvancedGeolocation(f.Random.Double(), f.Random.Double(),
                                                                f.Random.Double(), f.Date.Recent(),
                                                                f.Random.Double(), f.Random.Double()))
               .Generate();
    }
}