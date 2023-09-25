using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeAdmin.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            return services;
        }



    }
}
