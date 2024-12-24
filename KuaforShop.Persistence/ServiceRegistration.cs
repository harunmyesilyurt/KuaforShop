using KuaforShop.Persistence.Repositories.Appointment;
using KuaforShop.Persistence.Repositories.Employee;
using KuaforShop.Persistence.Repositories.Service;
using KuaforShop.Persistence.Repositories.User;
using KuaforShop.Persistence.Repositories.Expertise;
using Microsoft.Extensions.DependencyInjection;
using KuaforShop.Persistence.Repositories.Saloon;

namespace KuaforShop.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISaloonRepository, SaloonRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IExpertiseRepository, ExpertiseRepository>();
            // Add repositories, DbContext, and other services
            services.AddScoped<ISaloonRepository, SaloonRepository>();
        }
    }
}