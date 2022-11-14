using MultiProject.Delivery.Application.Delivieries.CreateScan;

namespace MultiProject.Delivery.Application.Deliveries.Commands.CreateScan;

public sealed record CreateScanCommand : ICommand<ScanCreatedDto>
{
    public int TransportUnitId { get; init; }
    public double? Quantity { get; init; }
    public Guid DelivererId { get; init; }

    //TODO: Obiekt Geolocation nie ma prawa istnieć w modelu przychodzącym z zewnątrz. Stwórz analogiczny tutaj i przepisz do prawdziwego Geolocation.
    // czy tu nie powinniśmy ogólnie zrobić "newScanToCreateDto", w którym byłyby te wszystkie pola podane w "płaski sposób"? coś na stylu "TransportUnitToCreate" w aplikacji i "NewTransportUnit" w domenie, a przepisywać w sumie nie mam za bardzo co chyba skoro w handlerze jest po odpalana metoda dodaj geolokację, a nie przekazywany model?
    // stworzyłęm kopię, ale nie wiem czy to dobrze, bo w domenie jest tam np. metoda "create" w kopii nie ma
    public GeolocationToAddDto? Location { get; init; }
}
