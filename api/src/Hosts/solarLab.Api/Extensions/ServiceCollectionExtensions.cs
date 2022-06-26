using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using solarLab.AppServices.Repositories.Category;
using solarLab.AppServices.Repositories.Chat;
using solarLab.AppServices.Repositories.Comment;
using solarLab.AppServices.Repositories.Message;
using solarLab.AppServices.Repositories.Post;
using solarLab.AppServices.Services.Category;
using solarLab.AppServices.Services.Chat;
using solarLab.AppServices.Services.Comment;
using solarLab.AppServices.Services.Email;
using solarLab.AppServices.Services.File;
using solarLab.AppServices.Services.Identity;
using solarLab.AppServices.Services.Message;
using solarLab.AppServices.Services.Post;
using solarLab.AppServices.Services.User;
using solarLab.Contracts.Base;
using solarLab.Contracts.Category;
using solarLab.Contracts.Comment;
using solarLab.Contracts.Identity;
using solarLab.Contracts.Post;
using solarLab.Contracts.User;
using solarLab.Contracts.Validators.Base;
using solarLab.Contracts.Validators.Category;
using solarLab.Contracts.Validators.Comment;
using solarLab.Contracts.Validators.Identity;
using solarLab.Contracts.Validators.Post;
using solarLab.Contracts.Validators.User;
using solarLab.DataAccess;
using solarLab.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace solarLab.Api.Extensions
{
    /// <summary>
    /// Расширение для service collection
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Добавление сервисов для работы с сущностями
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddServices(this IServiceCollection services)
           => services
                .AddTransient<IPostService, PostService>()
                .AddTransient<ICommentService, CommentService>()
                .AddTransient<ICategoryService, CategoryService>()
                .AddTransient<IUserService, UserService>()
                .AddTransient<IIdentityService, IdentityService>()
                .AddTransient<IFileService, FileService>()
                .AddTransient<IChatService, ChatService>()
                .AddTransient<IMessageService, MessageService>()
                .AddTransient<IEmailService, EmailService>();

        /// <summary>
        /// Добавление репозиториев для работы с сущностями
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRepositories(this IServiceCollection services)
           => services
                .AddTransient<ICommentRepository, CommentRepository>()
                .AddTransient<IPostRepository, PostRepository>()
                .AddTransient<IChatRepository, ChatRepository>()
                .AddTransient<IMessageRepository, MessageRepository>()
                .AddTransient<ICategoryRepository, CategoryRepository>();

        /// <summary>
        /// Добавление валидаторов 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddValidators(this IServiceCollection services)
           => services
                .AddTransient<IValidator<BaseDto>, BaseDtoValidator>()
                .AddTransient<IValidator<CategoryDto>, CategoryDtoValidator>()
                .AddTransient<IValidator<CommentDto>, CommentDtoValidator>()
                .AddTransient<IValidator<LoginDto>, LoginDtoValidator>()
                .AddTransient<IValidator<RegisterDto>, RegisterDtoValidator>()
                .AddTransient<IValidator<PostDto>, PostDtoValidator>()
                .AddTransient<IValidator<UserDto>, UserDtoValidator>()
                .AddTransient<IValidator<ChangePasswordDto>, ChangePasswordDtoValidator>();


        /// <summary>
        /// Добавление и настройка identity
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
            })
                .AddEntityFrameworkStores<BaseDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }

        /// <summary>
        /// Добавление и настройка jwt аунтефикации
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Secret:Key"])),
                };
            }).AddGoogle(opt => {
                opt.ClientId = configuration["Authentication:Google:ClientId"];
                opt.ClientSecret = configuration["Authentication:Google:ClientSecret"];
            });

            return services;
        }

       /// <summary>
       /// Добавление и настройка swagger
       /// </summary>
       /// <param name="services"></param>
       /// <returns></returns>
        public static IServiceCollection AddSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "solarLab.Api", Version = "v1" });



                var currentAssembly = Assembly.GetExecutingAssembly();
                var xmlDocs = currentAssembly.GetReferencedAssemblies()
                .Union(new AssemblyName[] { currentAssembly.GetName() })
                .Select(a => Path.Combine(Path.GetDirectoryName(currentAssembly.Location), $"{a.Name}.xml"))
                .Where(f => File.Exists(f)).ToArray();

                Array.ForEach(xmlDocs, (d) =>
                {
                    opt.IncludeXmlComments(d);
                });

                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });

            return services;
        }

    }
}
