using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NextHouse.Application.Contracts.Persistence;
using NextHouse.Application.Contracts.Repositories;
using NextHouse.Persistence.Entities;
using NextHouse.Persistence.Repositories;
using NextHouse.Persistence.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Persistence
{
    public static class PersistenceServicesRegistry
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer("name=DefaultConnection");
            });

            services.AddScoped<IUnitOfWork, EfCoreUnitOfWork>();
            services.AddScoped<IPropertyRepository, PropertyRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            //services.AddTransient<SeedDb>();

            // Inrfastructure
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
                options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ApplicationScheme;
            }).AddIdentityCookies();

            services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 4;
                options.SignIn.RequireConfirmedEmail = false; // TODO: Change this to true in production
                options.User.RequireUniqueEmail = true;
            }).AddSignInManager<SignInManager<ApplicationUser>>()
              .AddEntityFrameworkStores<DataContext>()
              .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(15);
            });

            return services;
        }
    }
}
