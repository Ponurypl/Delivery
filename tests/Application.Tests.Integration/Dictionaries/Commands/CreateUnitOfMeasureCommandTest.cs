using ErrorOr;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using MultiProject.Delivery.Application.Common.Behaviors;
using MultiProject.Delivery.Application.Common.Persistence;
using MultiProject.Delivery.Application.Common.Persistence.Repositories;
using MultiProject.Delivery.Application.Dictionaries.Commands.CreateUnitOfMeasure;
using MultiProject.Delivery.Domain.Common.Abstractions;
using MultiProject.Delivery.Domain.Dictionaries.Entities;
using MultiProject.Delivery.Domain.Dictionaries.ValueTypes;
using NSubstitute;
using System.Reflection;

namespace MultiProject.Delivery.Application.Tests.Integration.Dictionaries.Commands;

public class CreateUnitOfMeasureCommandTest
{

    [Fact]
    public async void Test()
    {
        var container = new ServiceCollection();
        container.AddMediatR(c =>
                             {
                                 c.RegisterServicesFromAssemblyContaining(typeof(ConfigureServices));
                                 c.AddOpenBehavior(typeof(ValidationBehavior<,>));
                             });



        container.AddScoped(s =>
                            {
                                var uow = Substitute.For<IUnitOfWork>();
                                //do konfiguracji
                                return uow;
                            });

        container.AddScoped(s =>
                            {
                                var repo = Substitute.For<IUnitOfMeasureRepository>();
                                repo.When(r => r.Add(Arg.Any<UnitOfMeasure>())).Do(info =>
                                {
                                    var unit = info.Arg<UnitOfMeasure>();
                                    typeof(Entity<UnitOfMeasureId>)
                                        .GetProperty(nameof(UnitOfMeasure.Id))
                                        .SetValue(unit, new UnitOfMeasureId(1));
                                });
                                return repo;
                            });

        container.AddScoped(s =>
                            {
                                //ILogger repo = (ILogger) Substitute.For(new[]{ typeof(ILogger<>)}, null);
                                var logger = Substitute
                                    .For<ILogger<ValidationBehavior<CreateUnitOfMeasureCommand,
                                        ErrorOr<UnitOfMeasureCreatedDto>>>>();
                                //do konfiguracji
                                return logger;
                            });

        var services = container.BuildServiceProvider();


        var sender = services.GetRequiredService<ISender>();

        var result = await sender.Send(new CreateUnitOfMeasureCommand() { Name = "abc", Symbol = "abc" });

        result.IsError.Should().BeFalse();
        result.Value.Id.Should().Be(1);

    }
}