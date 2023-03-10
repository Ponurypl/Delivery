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

    [Fact]
    public async void CreateUnitOfMeasureCommand_WhenValidDataProvided_ThenReturnsId()
    {
        //Arrange
        const int unitId = 1;
        Mock<IUnitOfMeasureRepository> repoMock = new();
        repoMock.Setup(s => s.Add(It.IsAny<UnitOfMeasure>()))
                .Callback((UnitOfMeasure u) => u.SetId(new UnitOfMeasureId(unitId)));

        var provider = ContainerSetup.CreateNew()
                                     .AddDefaultValidators()
                                     .AddMediatR()
                                     .AddLogging()
                                     .AddScoped(Mock.Of<IUnitOfWork>())
                                     .AddScoped(repoMock.Object)
                                     .Build();

        var sender = provider.GetRequiredService<ISender>();

        //Act
        var result = await sender.Send(new CreateUnitOfMeasureCommand() { Name = "abc", Symbol = "abc" });

        //Assert
        result.IsError.Should().BeFalse();
        result.Value.Id.Should().Be(unitId);

    }
}