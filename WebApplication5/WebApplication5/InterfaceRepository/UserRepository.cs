using System.Security.Cryptography;
using WebApplication5.Controllers;
using WebApplication5.DTO;
using WebApplication5.Entities;
using WebApplication5.Models;

namespace WebApplication5.InterfaceRepository
{

  
    public class UserRepository : IUserRepository
    {
        private readonly UserContext dbContext;
        private readonly static int iterations = 1000;


        public UserRepository(UserContext dbContext)
        {
            this.dbContext = dbContext;
        }
       public List<User> GetAllUsers()
        {
            return dbContext.Users.ToList();
        }

        public User GetById(Guid id) 
        {
            return dbContext.Users.Find(id);
        }

        public User AddUser(User user) 
        {
            dbContext.Users.Add(user);   
            dbContext.SaveChanges();
            return user;
        }

        public User UpdateUser(User user) 
        {

            //dbContext.Users.Update(GetById(user.IDUser));
            User updateUser = GetById(user.IDUser);
            updateUser.NameUser = user.NameUser;
            updateUser.EmailUser = user.EmailUser;
            updateUser.SurnameUser = user.SurnameUser;
            updateUser.PasswordUser = user.PasswordUser;
            dbContext.SaveChanges();    
            return user;    
        }

        public List<UserRoleDto> GetUserRole()
        {
            var userRoles = dbContext.Users
            .Select(u => new UserRoleDto
        {
            IDUser = u.IDUser,
            NameUser = u.NameUser,
            SurnameUser = u.SurnameUser,
            EmailUser = u.EmailUser,
            TeamId = u.TeamId,
            NameRole = dbContext.Roles.FirstOrDefault(r => r.IDRole == u.IDRole)!= null ? dbContext.Roles.First(r => r.IDRole == u.IDRole).NameRole : null
        })
        .ToList();

            return userRoles;
        }

        public void DeleteUser(Guid id) 
        {
            var user = dbContext.Users.Find(id);    
            if (user != null) 
            {
                dbContext.Users.Remove(user);
                dbContext.SaveChanges();
            }
        }  

        public bool UserWithCredentialsExists(string email, string password)
        {
            User user = dbContext.Users.FirstOrDefault(u => u.EmailUser == email);

            if (user == null) { return false; }

            if (VerifyPassword(password, user.PasswordUser, user.Salt))
            {
                return true;
            }

            return false;


        }

        public bool VerifyPassword(string password, string savedHash, string savedSalt)
        {
            var saltBytes = Convert.FromBase64String(savedSalt);
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, iterations);
            if (Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256)) == savedHash)
            {
                return true;
            }
            return false;
        }

        public bool uniqueEmail(string email)
        {
            var unique = dbContext.Users.Any(u => u.EmailUser == email);
            return unique;
        }



    }
}
