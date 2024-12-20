using KuaforShop.Core.Entities;
using KuaforShop.Core.Enumertaions;
using KuaforShop.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuaforShop.Persistence.Repositories.User
{
    public class UserRepository : Repository<Users>, IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Users> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task<bool> IsUsernameExistAsync(string username)
        {
            return await _context.Users.AnyAsync(x => x.Username == username);
        }

        public async Task<List<Users>> GetByRoleAsync(enmRoles role)
        {
            return await _context.Users
                //.Where(x => x.Role == role)
                .ToListAsync();
        }
    }
}
