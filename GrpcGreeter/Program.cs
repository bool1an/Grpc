using GrpcServiceApp.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    //Установка HTTP/2 соединения без TLS.
    options.ListenLocalhost(5001, o => o.Protocols =
        HttpProtocols.Http2);
});

builder.Services.AddGrpc();

//Установка максимального принимаемого значения.
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxResponseBufferSize = 200 * 1024 * 1024;
    serverOptions.Limits. = 200 * 1024 * 1024;
});

var app = builder.Build();

app.MapGrpcService<FilerService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
