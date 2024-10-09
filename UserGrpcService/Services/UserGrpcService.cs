using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using UserGrpcService.Protos;
using static UserGrpcService.Protos.UserService;

namespace UserGrpcService.Services
{
    public class UserGrpcService : UserServiceBase
    {
        private readonly MockUserRepo _userRepo;
        public UserGrpcService(MockUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public override Task<Empty> DeleteUser(UserIdRequest request, ServerCallContext context)
        {
            _userRepo.RemoveUser(request.Id);
            return Task.FromResult(new Empty());
        }

        public override async Task GetAll(Empty request, IServerStreamWriter<UserResponse> responseStream, ServerCallContext context)
        {
            var users = _userRepo.All();
            foreach (var user in users)
            {
                await responseStream.WriteAsync(user);
            }
        }

        public override Task<UserResponse> GetUserById(UserIdRequest request, ServerCallContext context)
        {
            var user = _userRepo.GetUserById(request.Id);
            if (user != null)
                return Task.FromResult(user); //так как результат уже есть то async\await ненужен 
            // генерируем RcpException ошибку для клиента , ну и для сервера(если логировать будем)
            throw new RpcException(new Status(StatusCode.NotFound, $"User with {request.Id} not found"));
        }
    }
}
