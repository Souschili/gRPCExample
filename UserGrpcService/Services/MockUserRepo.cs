using Grpc.Core;
using UserGrpcService.Protos;

namespace UserGrpcService.Services
{
    public class MockUserRepo
    {
        private static List<UserResponse> _userRepo = [
            new UserResponse{Id=1,Name="Sarhan"},
            new UserResponse{Id=2,Name="Orhan"},
            new UserResponse{Id=3,Name="Radj"},
            new UserResponse{Id=4,Name="Renat"},
            ];

        public IEnumerable<UserResponse> All()
        {
            return _userRepo;
        }

        public void RemoveUser(int id)
        {
            var user = _userRepo.Find(x => x.Id == id);
            if (user != null)
            {
                _userRepo.Remove(user);
            }
            else
            {
                throw new RpcException(new Status(StatusCode.NotFound,"User not found"));
            }
        }
        public UserResponse? GetUserById(int id) =>
            _userRepo.FirstOrDefault(x => x.Id == id);
    }
}
