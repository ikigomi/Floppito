using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using solarLab.Api.Extensions;
using solarLab.Api.Middlewares;
using solarLab.AppServices.Hubs;
using solarLab.DataAccess;
using solarLab.Domain.Entities;
using solarLab.Infrastructure.Filters;
using solarLab.Infrastructure.Repository;
using solarLab.Mapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace solarLab.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSignalR(o =>
            {
                o.MaximumReceiveMessageSize = null;
                o.EnableDetailedErrors = true;
            });

            services.AddMvc(opt => {

                opt.Filters.Add<ValidationFilter>();

            }).AddFluentValidation();

            services.AddHttpContextAccessor();

            services.AddDbContext<BaseDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ApplicationConnection")));
            services.AddScoped<DbContext>(s => s.GetRequiredService<BaseDbContext>());

            services.AddAutoMapper(typeof(MapperProfile));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));


            services.AddServices();
            services.AddRepositories();
            services.AddValidators();

            services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            }); //выключаем автоматический перехват model state чтобы ValidationFilter заработал


            services.AddIdentity();

            services.AddJwtAuthentication(Configuration);

            services.AddSwaggerGen();




            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "solarLab.Api v1"));
            }

            app.UseCors(options => options
               .SetIsOriginAllowed(origin => true)
               .AllowAnyHeader()
               .AllowCredentials()
               .AllowAnyMethod());

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseStaticFiles();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chat");
            });
        }
    }
}
