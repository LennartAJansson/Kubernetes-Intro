namespace Auth.Module.Features;

using System.Threading.Tasks;

using Auth.Module.Model;

public interface IRoleService
{
  Task<AuthRole> CreateRole(string roleName);

  Task<AuthRole> DeleteRole(string roleName);

  Task<IEnumerable<AuthRole>> GetRoles();
}