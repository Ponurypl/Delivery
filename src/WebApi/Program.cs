using FastEndpoints.Swagger;
using MultiProject.Delivery.Application;
using MultiProject.Delivery.Infrastructure;
using System.Text.Json;
using System.Text.Json.Serialization;
using MultiProject.Delivery.WebApi;
using FastEndpoints.Security;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddApplicationLayerServices();
services.AddInfrastructureLayerServices(builder.Configuration);
services.AddWebApiLayerServices();

services.AddFastEndpoints();
services.AddAuthenticationJWTBearer("21pZv^5jW1nQ&6hK3Qt2");
services.AddSwaggerDoc(settings =>
                       {
                           settings.Title = "Delivery.WebApi";
                           settings.Version = "v1";
                       },
                       js =>
                       {
                           js.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                           js.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                       },
                       shortSchemaNames: true, maxEndpointVersion: 1);

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.UseFastEndpoints(c =>
                     {
                         c.Endpoints.ShortNames = true;
                         c.Endpoints.RoutePrefix = "api";

                         c.Versioning.DefaultVersion = 1;
                         c.Versioning.Prefix = "v";
                         c.Versioning.PrependToRoute = true;
                     }); 
app.UseSwaggerGen();

app.Run();
