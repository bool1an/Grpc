using System.Threading.Tasks;
using Grpc.Net.Client;


using var channel = GrpcChannel.ForAddress("http://localhost:5242");
var fileService = new FileService.FileServiceClient(channel);

byte[] file = File.ReadAllBytes("E:\\malicious_phish.csv");

FileUploadRequest request = new FileUploadRequest();
request.File = new FileModel();
request.File.Day = "Day 1";
request.File.Session = "Session 1";
request.File.FileBytes = Google.Protobuf.ByteString.CopyFrom(file);
request.File.FileExtention = "jpg";
request.File.StartTime = "08:30";
request.File.CombinationId = 1;
request.File.Decryptionkey = "Eex";
request.File.FileTimeMinutes = 90;

request.Request = new GeneralRequest();
request.Request.Ip = "192.168.88.5";
request.Request.Mac = " asdf";
request.Request.HddSerial = "Asdfadf";
request.Request.Timestamp = "ASdf";

var call = fileService.UploadFile();
await call.RequestStream.WriteAsync(request);
await call.RequestStream.CompleteAsync();
FileUploadResponse response = await call;
Console.WriteLine(response.Response.Message);


Console.WriteLine("Press anykey to continue");
Console.ReadKey();
