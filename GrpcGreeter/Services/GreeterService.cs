using Grpc.Core;
using System.Diagnostics;
using GrpcServiceApp;

namespace GrpcServiceApp.Services
{
    public class FilerService : Filer.FilerBase
    {
        public override async Task<FileUploadResponse> UploadFile(IAsyncStreamReader<FileUploadRequest> requestStream, ServerCallContext context)
        {
            Stopwatch stopwatch = new Stopwatch();
            Console.WriteLine("File upload request received");
            stopwatch.Start();
            //пока не закрыли поток
            if (await requestStream.MoveNext())
            {
                FileUploadRequest request = requestStream.Current;
                FileModel File = new FileModel
                { 
                    FileBytes = request.File.FileBytes
                };
            }
            stopwatch.Stop();

            Console.WriteLine("Done: " + stopwatch.ElapsedMilliseconds);
            return new FileUploadResponse
            {
                Code = 200,
                Message = "File uploaded successfully",
                TimeLoad = (int)stopwatch.ElapsedMilliseconds
            };
        }
    }
}