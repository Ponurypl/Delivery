using Microsoft.Extensions.Options;
using MultiProject.Delivery.Application.Common.Cryptography;

namespace MultiProject.Delivery.Infrastructure.Configurations;

internal class ConfigureOptionsWithCryptoService : IConfigureOptions<IOptionsWithCryptoService>
{
    private readonly ICryptoService _cryptoService;

    public ConfigureOptionsWithCryptoService(ICryptoService cryptoService)
    {
        _cryptoService = cryptoService;
    }

    public void Configure(IOptionsWithCryptoService options)
    {
        options.ConfigureCryptoService(_cryptoService);
    }
}