﻿using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcGreeterClient;

// создаем канал для обмена сообщениями с сервером
// параметр - адрес сервера gRPC
using var channel = GrpcChannel.ForAddress("https://localhost:7255");
// создаем клиент
var client = new Greeter.GreeterClient(channel);
Console.Write("Введите имя: ");
var name = Console.ReadLine();
// обмениваемся сообщениями с сервером
var reply = await client.SayHelloAsync(new HelloRequest { Name = name });
Console.WriteLine($"Ответ сервера: {reply.Message}");
Console.ReadKey();