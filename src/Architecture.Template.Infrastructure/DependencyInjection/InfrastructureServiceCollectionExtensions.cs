using Domain.Interfaces.Repository;
using Infrastructure.Context;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("HdnCoreArchitectureDbInMemory"));
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }

        services.AddScoped<ITodoItemRepository, TodoItemRepository>();
        services.AddScoped<ITodoListRepository, TodoListRepository>();

        //TODO: recuperar o user do identity com a biblioteca do AD
        //services
        //    .AddDefaultIdentity<ApplicationUser>()
        //    .AddRoles<IdentityRole>()
        //    .AddEntityFrameworkStores<ApplicationDbContext>();

        //services.AddIdentityServer()
        //    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

        //services.AddAuthentication()
        //    .AddIdentityServerJwt();

        //services.AddAuthorization(options => 
        //    options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator")));

        return services;
    }
}
