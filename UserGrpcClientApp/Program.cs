using Grpc.Core;
using Grpc.Net.Client;
using UserGrpcClientApp.Protos;

namespace UserGrpcClientApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                // создаем канал и клиент(с помощью него мы будем отправлять и получать ответы от сервера)
                var channel = GrpcChannel.ForAddress("https://localhost:7206");
                var client = new UserService.UserServiceClient(channel);

                // вызываем метод для получения всех клиентов
                using var call = client.GetAll(new Google.Protobuf.WellKnownTypes.Empty());

                // получаем все данные через асинхронный поток IAsyncEnumerable
                await foreach (var item in call.ResponseStream.ReadAllAsync())
                {
                    Console.WriteLine($"Responce from server: {item.Id} {item.Name} ");
                }
                // don't close app
               await Task.Delay(Timeout.InfiniteTimeSpan);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }



        }
    }
}
