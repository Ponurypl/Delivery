using FastEndpoints.Swagger;
using MultiProject.Delivery.Application;
using MultiProject.Delivery.Infrastructure;
using System.Text.Json;
using System.Text.Json.Serialization;
using MultiProject.Delivery.WebApi;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MultiProject.Delivery.WebApi.Common.Auth;
using NSwag;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddApplicationLayerServices();
services.AddInfrastructureLayerServices(builder.Configuration);
services.AddWebApiLayerServices();

services.AddFastEndpoints();
services.AddAuthentication(o =>
                           {
                               o.DefaultAuthenticateScheme = AuthConsts.AccessSchema;
                               o.DefaultChallengeScheme = AuthConsts.AccessSchema;
                           })
        .AddJwtBearer(AuthConsts.AccessSchema, o =>
                      {
                          o.TokenValidationParameters = new TokenValidationParameters
                                                         {
                                                             ValidateIssuerSigningKey = true,
                                                             ValidateAudience = true,
                                                             ValidAudience = AuthConsts.AccessSchema,
                                                             ValidateIssuer = false,
                                                             ValidateLifetime = true,
                                                             ClockSkew = TimeSpan.FromSeconds(60),
                                                             IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AuthConsts.JwtSigningKey))
                                                         };
                      })
        .AddJwtBearer(AuthConsts.RefreshSchema, o =>
                                               {
                                                   o.TokenValidationParameters = new TokenValidationParameters
                                                       {
                                                           ValidateIssuerSigningKey = true,
                                                           ValidateAudience = true,
                                                           ValidAudience = AuthConsts.RefreshSchema,
                                                           ValidateIssuer = false,
                                                           ValidateLifetime = true,
                                                           ClockSkew = TimeSpan.FromSeconds(60),
                                                           IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AuthConsts.JwtSigningKey))
                                                       };
                                               });

services.AddSwaggerDoc(settings =>
                       {
                           settings.Title = "Delivery.WebApi";
                           settings.Version = "v1";
                           settings.AddAuth(AuthConsts.AccessSchema,
                                            new()
                                            {
                                                Type = OpenApiSecuritySchemeType.Http,
                                                Scheme = JwtBearerDefaults.AuthenticationScheme,
                                                BearerFormat = "JWT",
                                            });
                           settings.AddAuth(AuthConsts.RefreshSchema,
                                            new()
                                            {
                                                Type = OpenApiSecuritySchemeType.Http,
                                                Scheme = JwtBearerDefaults.AuthenticationScheme,
                                                BearerFormat = "JWT",
                                            });
                           settings.OperationProcessors.Add(new AddUnauthorizedResponseOperationProcessor());
                       },
                       js =>
                       {
                           js.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                           js.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                       },
                       shortSchemaNames: true, maxEndpointVersion: 1, addJWTBearerAuth: false);

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