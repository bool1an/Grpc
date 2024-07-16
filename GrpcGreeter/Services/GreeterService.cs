using Grpc.Core;

namespace GrpcServiceApp.Services
{
    public class FilerService : Filer.FilerBase
    {
        public override async Task<FileUploadResponse> UploadFile(IAsyncStreamReader<FileUploadRequest> requestStream, ServerCallContext context)
        {
            GeneralResponse response = new GeneralResponse();
            Console.WriteLine("File upload request received");
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


            response.Code = 200;
            response.Status = "Success";
            response.Message = "File uploaded successfully";
            Console.WriteLine("Done");
            return new FileUploadResponse
            {
                Response = response
            };
        }
    }
}