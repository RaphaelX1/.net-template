﻿using FluentValidation.AspNetCore;
using Hdn.Core.Architecture.Application.DependencyInjection;
using Hdn.Core.Architecture.Infrastructure.Context;
using Hdn.Core.Architecture.Infrastructure.DependencyInjection;
using Hdn.Core.Architecture.WebApi.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;

namespace Hdn.Core.Architecture.WebApi;

public class Startup
{
    public IConfiguration Configuration { get; }
    public Startup(IConfiguration configuration) =>
        Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

    public void ConfigureServices(IServiceCollection services)
    {
        // Add services to the container.
        //TODO: Adicionar depois um teste com identity local
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi(Configuration.GetSection("AzureAd"));

        services.AddApplication();
        services.AddInfrastructure(Configuration);

        //services.AddSingleton<ICurrentUserService, CurrentUserService>();//TODO: Add servico do usuario

        services.AddDatabaseDeveloperPageExceptionFilter();
        services.AddHttpContextAccessor();
        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

        services.AddControllersWithViews(options =>
            options.Filters.Add<ApiExceptionFilterAttribute>())
                .AddFluentValidation(x => x.AutomaticValidationEnabled = false);

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
    //TODO: adicionar esse cara junto com o extension startup
    //public void Configure(WebApplication app, IWebHostEnvironment environment)
    public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
    {
        // Configure the HTTP request pipeline.
        if (environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        app.UseHealthChecks("/health");
        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action=Index}/{id?}");
        });
        //app.MapControllers();
    }
}
