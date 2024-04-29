using WebApplication5.Entities;
using WebApplication5.Models;

namespace WebApplication5.InterfaceRepository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly UserContext dbContext;

        public RoleRepository(UserContext dbContext) 
        {
            this.dbContext = dbContext;
        }

        public List<Role> GetAllRoles() 
        {
            return dbContext.Roles.ToList();
        } 

        public Role GetRoleById(Guid id) 
        {
            return dbContext.Roles.Find(id);
        }

        public Role AddRole(Role role) 
        {
            dbContext.Roles.Add(role);  
            dbContext.SaveChanges();
            return role;    
        }

        public Role UpdateRole(Role role) 
        {
            //dbContext.Roles.Update(role);
            Role updateRole = GetRoleById(role.IDRole);
            updateRole.NameRole = role.NameRole;
            dbContext.SaveChanges();
            return role;
        }

        public void DeleteRole(Guid id) 
        {
            var role = dbContext.Roles.Find(id);
            if (role != null) 
            {
                dbContext.Roles.Remove(role);
                dbContext.SaveChanges();
            }
        }
    }
}
