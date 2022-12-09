using MultiProject.Delivery.Application;
using MultiProject.Delivery.Infrastructure;
using WebApi;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddApplicationLayerServices();
services.AddInfrastructureLayerServices(builder.Configuration);
services.AddWebApiLayerServices();
services.AddFastEndpoints();

var app = builder.Build();
app.UseAuthorization();
app.UseFastEndpoints();

app.Run();
