using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using NextHouse.Application.Utilites.Mediator;

namespace NextHouse.Application
{
    public static class ApplicationServicesRegistry
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Mediator
            services.AddTransient<IMediator, SimpleMediator>();

            services.Scan(scan => scan.FromAssembliesOf(typeof(IMediator))
                                      .AddClasses(classes => classes.AssignableTo(typeof(IRequestHandler<>)))
                                      .AsImplementedInterfaces()
                                      .WithScopedLifetime()
                                      .AddClasses(classes => classes.AssignableTo(typeof(IRequestHandler<,>)))
                                      .AsImplementedInterfaces()
                                      .WithScopedLifetime()
                                      .AddClasses(classes => classes.AssignableTo(typeof(IValidator<>)))
                                      .AsImplementedInterfaces()
                                      .WithScopedLifetime()
            );

            // Use cases
            //services.AddScoped<IRequestHandler<GetSectionsListQuery, PaginationResponse<SectionListItemDTO>>, GetSectionsListUseCase>();
            //services.AddScoped<IRequestHandler<GetSectionByIdQuery, SectionDetailDTO>, GetSectionByIdUseCase>();
            //services.AddScoped<IRequestHandler<CreateSectionCommand, Guid>, CreateSectionUseCase>();
            //services.AddScoped<IRequestHandler<UpdateSectionCommand>, UpdateSectionUseCase>();
            //services.AddScoped<IRequestHandler<DeleteSectionCommand>, DeleteSectionUseCase>();
            //services.AddScoped<IRequestHandler<ActivateSectionCommand>, ActivateSectionUseCase>();
            //services.AddScoped<IRequestHandler<DeactivateSeccionCommand>, DeactivateSeccionUseCase>();


            //services.AddScoped<IRequestHandler<GetSectionOptionsQuery, IReadOnlyList<SectionOptionDTO>>, GetSectionOptionsUseCase>();
            //services.AddScoped<IRequestHandler<GetBlogsListQuery, PaginationResponse<BlogListItemDTO>>, GetBlogsListUseCase>();
            //services.AddScoped<IRequestHandler<GetBlogByIdQuery, BlogDetailDTO?>, GetBlogByIdUseCase>();
            //services.AddScoped<IRequestHandler<CreateBlogCommand, Guid>, CreateBlogUseCase>();
            //services.AddScoped<IRequestHandler<UpdateBlogCommand>, UpdateBlogUseCase>();
            //services.AddScoped<IRequestHandler<DeleteBlogCommand>, DeleteBlogUseCase>();


            //services.AddScoped<IRequestHandler<LoginCommand, AccountSignInResult>, LoginUseCase>();
            //services.AddScoped<IRequestHandler<LogoutCommand>, LogoutUseCase>();

            //// Validators
            //services.AddValidatorsFromAssemblyContaining<CreateSectionCommandValidator>();
            //services.AddValidatorsFromAssemblyContaining<UpdateSectionCommandValidator>();

            //services.AddValidatorsFromAssemblyContaining<CreateBlogCommandValidator>();
            //services.AddValidatorsFromAssemblyContaining<UpdateBlogCommandValidator>();
            //services.AddValidatorsFromAssemblyContaining<DeleteBlogCommandValidator>();

            //services.AddValidatorsFromAssemblyContaining<LoginCommandValidator>();

            return services;
        }
    }
}
