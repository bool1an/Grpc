using Grpc.Core;
using System.Diagnostics;

namespace GrpcServiceApp.Services
{
    public class FilerService : Filer.FilerBase
    {
        public override async Task<FileUploadResponse> UploadFile(IAsyncStreamReader<FileUploadRequest> requestStream, ServerCallContext context)
        {
            GeneralResponse response = new GeneralResponse();
            Stopwatch stopwatch = new Stopwatch();
            Console.WriteLine("File upload request received");
            stopwatch.Start();
            if (await requestStream.MoveNext())
            {
                FileUploadRequest request = new FileUploadRequest();
                request = requestStream.Current;
                GeneralRequest generalRequest = request.Request;

                if (generalRequest != null)
                {
                    FileModel File = new FileModel
                    {
                        Day = request.File.Day,
                        Session = request.File.Session,
                        StartTime = request.File.StartTime,
                        FileTimeMinutes = request.File.FileTimeMinutes,
                        Decryptionkey = request.File.Decryptionkey,
                        FileExtention = request.File.FileExtention,
                        FileBytes = request.File.FileBytes
                    };
                }
            }
            stopwatch.Stop();

            response.Code = 200;
            response.Status = "Success";
            response.Message = "File uploaded successfully";
            Console.WriteLine("Done: " + stopwatch.ElapsedMilliseconds);
            return new FileUploadResponse
            {
                Response = response
            };
        }
    }
}