using Grpc.Core;

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
            File File = new File();
            File.Day = request.File.Day;
            File.Session = request.File.Session;
            File.FileStartTime = request.File.StartTime;
            File.FileTimeMinutes = request.File.FileTimeMinutes;
            File.DecryptionKey = request.File.Decryptionkey;
            File.FileExtention = request.File.FileExtention;
            File.FileBytes = request.File.FileBytes.ToByteArray();
            try
            {
                myDbContext.Add(File);
                myDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                response.Code = 500;
                response.Status = "Failed";
                response.Message = "Database Exception Occured";
                response.Details = e.Message;
                return new FileUploadResponse
                {
                    Response = response
                };
            }
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