using GrpcServiceApp.Services;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

//”становка максимального принимаемого значени€.
builder.Services.AddGrpc(options =>
{
    options.MaxReceiveMessageSize = int.MaxValue; 
});

var app = builder.Build();

app.MapGrpcService<FilerService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
