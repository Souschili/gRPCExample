using Grpc.Core;
using SharedModels;
using UserGrpcService.Protos;

namespace UserGrpcService.Services
{
    public class MockUserRepo
    {
        private static List<User> _userRepo = [
            new User{Id=1,Name="Sarhan",Email="test1@demo.az"},
            new User{Id=2,Name="Orhan",Email="test2@demo.az"},
            new User{Id=3,Name="Radj",Email="test3@demo.az"},
            new User{Id=4,Name="Renat",Email="test4@demo.az"},
            ];

        public IEnumerable<UserResponse> All()
        {
            var users = _userRepo.Select(x => new UserResponse
            {
                Id=x.Id,
                Name=x.Name,
                Email=x.Email,
            });
            return users;
        }

        //public void RemoveUser(IEnumerable<int> listId)
        //{
        //    // если список будет содержать больше 100 элементов 
        //    // то стоит рассмотреть вараинт с использованием HashSet<T> 
        //    // где время поиска равно O(1) а в списке оно O(n)
        //    //HashSet<int> hashSetIds = new HashSet<int>(listId);
        //    //_userRepo.RemoveAll(x => hashSetIds.Contains(x.Id));

        //    var ids = _userRepo.Select(x => x.Id);
        //    _userRepo.RemoveAll(x=> ids.Contains(x.Id));
        //}

        public User? GetUserById(int id) =>
            _userRepo.FirstOrDefault(x => x.Id == id);

        public void UpdateUser(UserUpdateRequest request)
        {
            var user = _userRepo.FirstOrDefault(x => x.Id == request.Id);
            if (user is null)
                throw new RpcException(new Status(StatusCode.NotFound, "User not found"));
            user.Name = request.Name;
            user.Email=request.Email;
        }


    }
}
