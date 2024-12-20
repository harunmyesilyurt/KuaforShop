using KuaforShop.Core.Entities;

namespace KuaforShop.Persistence.Repositories.User
{
    public interface IUserRepository : IRepository<Users>
    {
        Task<Users> GetByUsernameAsync(string username);
        Task<bool> IsUsernameExistAsync(string username);
        Task<List<Users>> GetByRoleAsync(Core.Enumertaions.enmRoles role);
    }
}
