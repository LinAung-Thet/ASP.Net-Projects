using Grpc.Net.Client;
using GrpcShared;

var channel = GrpcChannel.ForAddress("https://localhost:5001");
var client = new Greeter.GreeterClient(channel);

var reply = await client.SayHelloAsync(new HelloRequest { Name = "Alice" });

Console.WriteLine($"Greeting: {reply.Message}");
