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

        public void RemoveUser(IEnumerable<int> listId)
        {
            // если список будет содержать больше 100 элементов 
            // то стоит рассмотреть вараинт с использованием HashSet<T> 
            // где время поиска равно O(1) а в списке оно O(n)
            //HashSet<int> hashSetIds = new HashSet<int>(listId);
            //_userRepo.RemoveAll(x => hashSetIds.Contains(x.Id));

            var ids = _userRepo.Select(x => x.Id);
            _userRepo.RemoveAll(x=> ids.Contains(x.Id));
        }

        public UserResponse? GetUserById(int id) =>
            _userRepo.FirstOrDefault(x => x.Id == id);
    }
}
