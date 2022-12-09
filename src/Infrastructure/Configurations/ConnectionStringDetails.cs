using MultiProject.Delivery.Application.Common.Cryptography;

namespace MultiProject.Delivery.Infrastructure.Configurations;

internal sealed class ConnectionStringDetails : IOptionsWithCryptoService
{
    private string _password = default!;
    private ICryptoService? _cryptoService;

    public string ConnectionString { get; set; } = default!;
    public string Username { get; set; } = default!;
    public string Password
    {
        get => _password;
        set
        {
            try
            {
                _password = _cryptoService?.Decrypt(value) ?? value;
            }
            catch
            {
                _password = value;
            }
        }
    }

    public void ConfigureCryptoService(ICryptoService cryptoService)
    {
        _cryptoService = cryptoService;
        try
        {
            _password = cryptoService.Decrypt(_password);
        }
        catch
        {
            // ignored
        }
    }
}