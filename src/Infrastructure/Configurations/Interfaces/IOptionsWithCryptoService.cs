using MultiProject.Delivery.Application.Common.Cryptography;

namespace MultiProject.Delivery.Infrastructure.Configurations;

internal interface IOptionsWithCryptoService
{
    void ConfigureCryptoService(ICryptoService cryptoService);
}