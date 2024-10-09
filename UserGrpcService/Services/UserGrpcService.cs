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

        //public override async Task<Empty> DeleteUser(IAsyncStreamReader<UserIdRequest> requestStream, ServerCallContext context)
        //{
        //    List<int> userIds = new List<int>();
        //    await foreach (var request in requestStream.ReadAllAsync())
        //    {
        //        userIds.Add(request.Id);
        //    }

        //    if (userIds.Count == 0)
        //    {
        //        return new Empty();
        //    }

        //    _userRepo.RemoveUser(userIds);
        //    return new Empty();
        //}

        //public override async Task GetAll(Empty request, IServerStreamWriter<UserResponse> responseStream, ServerCallContext context)
        //{
        //    var users = _userRepo.All();
        //    foreach (var user in users)
        //    {
        //        await responseStream.WriteAsync(user);
        //    }
        //}

        //public override Task<UserResponse> GetUserById(UserIdRequest request, ServerCallContext context)
        //{
        //    var user = _userRepo.GetUserById(request.Id);
        //    if (user != null)
        //        return Task.FromResult(user); //так как результат уже есть то async\await ненужен 
        //    // генерируем RcpException ошибку для клиента , ну и для сервера(если логировать будем)
        //    throw new RpcException(new Status(StatusCode.NotFound, $"User with {request.Id} not found"));
        //}

        //public override Task<Empty> UpdateUser(UserUpdateRequest request, ServerCallContext context)
        //{
        //        _userRepo.UpdateUser(request);
        //        return Task.FromResult(new Empty()); 
        //}

        //public override async Task UserCreate(IAsyncStreamReader<UserCreateRequest> requestStream, IServerStreamWriter<UserResponse> responseStream, ServerCallContext context)
        //{
        //    List<UserCreateRequest> createList=new List<UserCreateRequest>();
        //    await foreach(var request in requestStream.ReadAllAsync())
        //    {
        //        createList.Add(request);
        //    }
        //    throw new RpcException(new Status(StatusCode.Unimplemented,"Not implemented"));
        //}
    }
}
