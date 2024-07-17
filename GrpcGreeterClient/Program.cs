using System.Diagnostics;
using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcClientApp;


//установка значений конфигурации клиента (на максимальное передаваемое значение)
using var channel = GrpcChannel.ForAddress("https://localhost:5002", new GrpcChannelOptions
{
    MaxSendMessageSize = int.MaxValue
});

var filer = new Filer.FilerClient(channel);

//Установка пути до передаваемого файла
byte[] file = File.ReadAllBytes("E:\\File1Gb.txt");

FileUploadRequest request = new FileUploadRequest();

//запись файла в protobuf
request.File = new FileModel{
    FileBytes = Google.Protobuf.ByteString.CopyFrom(file),
};

Stopwatch stopwatch = new Stopwatch();
stopwatch.Start();

//начало передачи файла
var call = filer.UploadFile();
//запись байт файла в текущий поток
await call.RequestStream.WriteAsync(request);
//закрытие потока
await call.RequestStream.CompleteAsync();
//ожидание конца загрузки файла и получение ответа после
FileUploadResponse response = await call;

stopwatch.Stop();
Console.WriteLine(response.Message + ' ' + stopwatch.ElapsedMilliseconds);
Console.WriteLine();
Console.WriteLine("Press any key to continue");
Console.ReadKey();
