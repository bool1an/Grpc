using Grpc.Core;
using GrpcGreeter;

<<<<<<< Updated upstream
namespace GrpcGreeter.Services
=======


public override async Task<FileUploadResponse> UploadFile(IAsyncStreamReader<FileUploadRequest> requestStream, ServerCallContext context)
>>>>>>> Stashed changes
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }
    }
}
