using System.Diagnostics;
using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcClientApp;


using var channel = GrpcChannel.ForAddress("http://localhost:5001", new GrpcChannelOptions
{
    MaxReceiveMessageSize = null,
    MaxSendMessageSize = null,
    MaxRetryBufferSize = null,
    MaxRetryBufferPerCallSize = null
});
var filer = new Filer.FilerClient(channel);


byte[] file = File.ReadAllBytes("E:\\File100Mb.txt");

FileUploadRequest request = new FileUploadRequest();
request.File = new FileModel{
    Day = "Day 1",
    Session = "Session 1",
    FileBytes = Google.Protobuf.ByteString.CopyFrom(file),
    FileExtention = "txt",
    StartTime = "08:30",
    CombinationId = 1,
    Decryptionkey = "Eex",
    FileTimeMinutes = 90
};


request.Request = new GeneralRequest();
request.Request.Ip = "192.168.88.5";
request.Request.Mac = " asdf";
request.Request.HddSerial = "Asdfadf";
request.Request.Timestamp = "ASdf";

Stopwatch stopwatch = new Stopwatch();
stopwatch.Start();
var call = filer.UploadFile();
await call.RequestStream.WriteAsync(request);
await call.RequestStream.CompleteAsync();
FileUploadResponse response = await call;
stopwatch.Stop();
Console.WriteLine(response.Response.Message + ' ' + stopwatch.ElapsedMilliseconds);
Console.WriteLine();
Console.WriteLine("Press any key to continue");
Console.ReadKey();
