using KuaforShop.Persistence.Context;
using KuaforShop.Persistence.Repositories.User;
using Microsoft.EntityFrameworkCore;

namespace KuaforShop.Setup
{
    public class Module
    {
        private readonly IConfiguration _configuration;

        public Module(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services) 
        {
            services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(_configuration["ConnectionStrings:MsSql"]),ServiceLifetime.Singleton);

            services.AddSingleton<IUserRepository, UserRepository>();
        }
    }
}
