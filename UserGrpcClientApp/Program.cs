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
        static async Task Main(string[] args)
        {
            try
            {

                while (true)
                {
                    ShowMenu();
                    if (!Int32.TryParse(Console.ReadLine(), out int menuKey))
                    {
                        Console.WriteLine("Wrong input try again or press 0 to exit");
                        continue;
                    }
                    if (menuKey == 0 || menuKey<0) break; // выходим из цикла и завершаем работу

                    // тут передаем номер меню 
                    await InvokeMenu(menuKey);
                    Console.WriteLine("press any key to continue");
                    Console.ReadKey();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static async Task InvokeMenu(int menuPoint)
        {
            // async variant
            await (menuPoint switch
            {
                1 => GetUsers(),
                _ => Task.Run(() => Console.WriteLine("Unknown method searched"))
            });
        }

        static async Task GetUsers()
        {
            var client = GrpcServiceClientFactory.GetUserServiceClient();

            // запрос серверу
            using var call = client.GetAll(new Google.Protobuf.WellKnownTypes.Empty());

            // получаем все данные через асинхронный поток IAsyncEnumerable
            await foreach (var user in call.ResponseStream.ReadAllAsync())
            {
                Console.WriteLine($"Responce from server: {user.Id} {user.Name} ");
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
