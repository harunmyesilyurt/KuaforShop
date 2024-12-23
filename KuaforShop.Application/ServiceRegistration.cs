using FluentValidation;
using KuaforShop.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace KuaforShop.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISaloonService, SaloonService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<IAppointmentService, AppointmentService>();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}