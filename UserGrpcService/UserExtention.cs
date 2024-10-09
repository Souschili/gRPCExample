using SharedModels;
using UserGrpcService.Protos;

namespace UserGrpcService
{
    public static class UserExtention
    {
        public static User MapToUser(this UserResponse response) =>
            new User { Id = response.Id, Name = response.Name, Email = response.Email };
        public static UserResponse MapToUserResponse(this User user) =>
             new UserResponse { Id = user.Id, Name = user.Name, Email = user.Email };
    }
}

