using WebApplication5.Models;

namespace WebApplication5.InterfaceRepository
{
    public interface IRoleRepository
    {
        List<Role> GetAllRoles();
        Role GetRoleById(Guid id);
        Role AddRole(Role role);
        Role UpdateRole(Role role);
        void DeleteRole(Guid id);
    }
}
