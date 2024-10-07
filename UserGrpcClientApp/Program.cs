using Grpc.Core;
using Grpc.Net.Client;
using UserGrpcClientApp.Protos;

namespace UserGrpcClientApp
{
    // синглтон класс возращающий клиент для юзерсервис
    public static class GrpcServiceClientFactory
    {
        private static UserService.UserServiceClient _serviceClient;
        private static readonly object _lock = new object();

        public static UserService.UserServiceClient GetUserServiceClient()
        {
            //патерн двойной проверки
            if (_serviceClient == null) //1 проверка
            {
                lock (_lock) //блокировка
                {
                    if (_serviceClient == null) //2 проверка
                    {
                        var channel = GrpcChannel.ForAddress("https://localhost:7206");
                        _serviceClient = new UserService.UserServiceClient(channel);
                    }

                }

            }
            return _serviceClient;
        }
    }

    internal class Program
    {
        //private static readonly UserService.UserServiceClient _userClient;
        static async Task Main(string[] args)
        {
            try
            {

                while (true)
                {
                    ShowMenu();
                    if (!Int32.TryParse(Console.ReadLine(), out int menuKey))
                    {
                        Console.WriteLine("Wrong input ,press 0 to exit");
                        continue;
                    }
                    if (menuKey == 0) break; // выходим из цикла и завершаем работу

                }

                // создаем канал и клиент(с помощью него мы будем отправлять и получать ответы от сервера)
                //var channel = GrpcChannel.ForAddress("https://localhost:7206");
                //var client = new UserService.UserServiceClient(channel);

                // // вызываем метод для получения всех клиентов
                // using var call = client.GetAll(new Google.Protobuf.WellKnownTypes.Empty());

                // // получаем все данные через асинхронный поток IAsyncEnumerable
                // await foreach (var item in call.ResponseStream.ReadAllAsync())
                // {
                //     Console.WriteLine($"Responce from server: {item.Id} {item.Name} ");
                // }
                // // don't close app
                //await Task.Delay(Timeout.InfiniteTimeSpan);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void ShowMenu()
        {
            Console.WriteLine("--------------------------");
            Console.WriteLine("1. Get all users");
            Console.WriteLine("0. Exit");
            Console.WriteLine("--------------------------");
        }
    }
}
