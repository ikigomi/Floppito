using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using solarLab.AppServices.Services.Category;
using solarLab.Contracts.Category;
using solarLab.Contracts.Enums;
using solarLab.Domain.Entities;
using solarLab.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace solarLab.Api
{
    public class Program
    {
        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File($"logs/logs_{DateTime.Today.ToShortDateString()}.txt")
            .CreateLogger();
            ApplicationUser createdUser;
            bool fillDb = false;

            try
            {
                Log.Information("Starting web host");
                var host = CreateHostBuilder(args).Build();

                // Создание админа для разработки
                using (var scope = host.Services.CreateScope())
                {
                    var usermanager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();

                    var user = new ApplicationUser
                    {
                        Name = "Администратор",
                        UserName = "Admin",
                        Email = "Admin@gmail.com",
                        BirthDate = DateTime.Today,
                        Gender = Gender.Male,
                        Avatar = File.ReadAllBytes("wwwroot/images/avatar/defaultAvatar.jpg"),
                        EmailConfirmed=true
                    };

                    createdUser = usermanager.FindByNameAsync(user.UserName).Result;

                    if (createdUser == null)
                    {

                        var password = "Admin!";
                        var cRes = usermanager.CreateAsync(user, password).Result;
                        var rRes = usermanager.AddToRoleAsync(user, Roles.Admin.ToString()).Result;
                        createdUser = usermanager.FindByNameAsync(user.UserName).Result;
                    }

                }


                //заполнение бд тестовыми данными
                //if (fillDb)
                //{
                //    var categories = new List<CategoryEntity>();
                //    var posts = new List<PostEntity>();
                //    var comments = new List<CommentEntity>();

                //    using (var scope = host.Services.CreateScope())
                //    {
                //        var categoryService = scope.ServiceProvider.GetService<IRepository<CategoryEntity>>();

                //        var categoryTitles = new string[] { "Машины", "Бытовая техника", "Товары для дома", "Электроника", "Мебель" };

                //        //создаем категории
                //        foreach (var title in categoryTitles)
                //        {
                //            categories.Add(new CategoryEntity
                //            {
                //                Title = title,
                //            });
                //        }

                //        //добавляем категории в бд
                //        foreach (var category in categories)
                //        {
                //            categoryService.AddAsync(category).Wait();
                //        }

                //        categories = categoryService.GetAll().ToList();
                //    }

                //    using (var scope = host.Services.CreateScope())
                //    {
                //        var postService = scope.ServiceProvider.GetService<IRepository<PostEntity>>();
                //        var postDescription = "Lorem ipsum dolor sit, amet consectetur adipisicing elit. Dolorum fugit aliquid ad porro optio laudantium molestias architecto. Debitis deleniti accusantium harum facere incidunt, minus dolore inventore cum odit aliquam quia.";


                //        //создаем посты 
                //        foreach (var category in categories)
                //        {
                //            for (int i = 0; i < 10; i++)
                //            {
                //                posts.Add(new PostEntity
                //                {
                //                    Title = $"Тестовый пост [№ {i}]",
                //                    CategoryId = category.Id,
                //                    Description = postDescription + $"\nКатегория: {category.Title}",
                //                    PostStatusId = PostStatus.New,
                //                    CreatorId = createdUser.Id,
                //                });
                //            }
                //        }

                //        //добавляем посты в бд
                //        foreach (var post in posts)
                //        {
                //            postService.AddAsync(post).Wait();
                //        }
                //        posts = postService.GetAll().ToList();
                //    }


                //    using (var scope = host.Services.CreateScope())
                //    {
                //        var commentService = scope.ServiceProvider.GetService<IRepository<CommentEntity>>();
                //        var commentBody = "Lorem ipsum dolor sit, amet consectetur adipisicing elit. Recusandae debitis quo cumque repudiandae tempora quos nesciunt accusantium consequatur aliquam expedita?";


                //        //создаем комментарии
                //        foreach (var post in posts)
                //        {
                //            for (int i = 0; i < 5; i++)
                //            {
                //                comments.Add(new CommentEntity
                //                {
                //                    CommentBody = $"Тестовый комментарий [№ {i}]\n" + commentBody + $"\nПост: {post.Title}",
                //                    PostId = post.Id,
                //                    CreatorId = createdUser.Id,
                //                });
                //            }
                //        }

                //        //добавляем комментарии в бд
                //        foreach (var comment in comments)
                //        {
                //            commentService.AddAsync(comment).Wait();
                //        }
                //    }
                //}

                host.Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
