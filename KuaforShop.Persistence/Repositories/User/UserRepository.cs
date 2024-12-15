using KuaforShop.Core.Entities;
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
        public UserRepository(AppDbContext context) : base(context) { }
    }
}
