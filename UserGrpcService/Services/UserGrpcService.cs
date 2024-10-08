﻿using Google.Protobuf.WellKnownTypes;
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
        public override async Task GetAll(Empty request, IServerStreamWriter<UserResponse> responseStream, ServerCallContext context)
        {
            var users = _userRepo.All();
            foreach (var user in users)
            {
                await responseStream.WriteAsync(user);
            }
        }

        public override async Task<UserResponse> GetUserById(UserIdRequest request, ServerCallContext context)
        {
            var user = _userRepo.GetUserById(request.Id);
            if (user != null)
                return user;
            throw new RpcException(new Status(StatusCode.NotFound, $"User with {request.Id} not found"));
        }
    }
}
