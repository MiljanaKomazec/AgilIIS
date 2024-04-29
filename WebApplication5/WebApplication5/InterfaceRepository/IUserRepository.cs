using WebApplication5.DTO;
using WebApplication5.Models;

namespace WebApplication5.InterfaceRepository
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        User GetById(Guid userId);

        User AddUser(User user);

        User UpdateUser(User user);

        List<UserRoleDto> GetUserRole();

        void DeleteUser(Guid userId);

        public bool UserWithCredentialsExists(string email,  string password);


        public bool uniqueEmail(string email);


    }
}
