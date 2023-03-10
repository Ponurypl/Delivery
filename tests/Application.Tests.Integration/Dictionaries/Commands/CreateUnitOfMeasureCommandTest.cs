using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using MultiProject.Delivery.Application.Common.Persistence;
using MultiProject.Delivery.Application.Common.Persistence.Repositories;
using MultiProject.Delivery.Application.Dictionaries.Commands.CreateUnitOfMeasure;
using MultiProject.Delivery.Application.Tests.Integration.Helpers;
using MultiProject.Delivery.Domain.Dictionaries.Entities;
using MultiProject.Delivery.Domain.Dictionaries.ValueTypes;

namespace MultiProject.Delivery.Application.Tests.Integration.Dictionaries.Commands;

public class CreateUnitOfMeasureCommandTest
{
    private readonly ContainerSetup _services;
    private readonly Mock<IUnitOfMeasureRepository> _repoMock;

    public CreateUnitOfMeasureCommandTest()
    {
        _repoMock = new Mock<IUnitOfMeasureRepository>();
        _services = ContainerSetup.CreateNew()
                                  .AddDefaultValidators()
                                  .AddMediatR()
                                  .AddLogging()
                                  .AddScoped(Mock.Of<IUnitOfWork>())
                                  .AddScoped(_repoMock.Object);
    }

    [Fact]
    public async void CreateUnitOfMeasureCommand_WhenValidDataProvided_ThenReturnsId()
    {
        //Arrange
        const int unitId = 1;
        _repoMock.Setup(s => s.Add(It.IsAny<UnitOfMeasure>()))
                .Callback<UnitOfMeasure>(unit => unit.SetId(new UnitOfMeasureId(unitId)));

        var provider = _services.Build();
        var sender = provider.GetRequiredService<ISender>();

        //Act
        var result = await sender.Send(new CreateUnitOfMeasureCommand() { Name = "abc", Symbol = "abc" });

        //Assert
        result.IsError.Should().BeFalse();
        result.Value.Id.Should().Be(unitId);

    }
}