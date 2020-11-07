using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TodoListStart.Application.ApplicationServices;
using AutoMapper;
using TodoListStart.Application.Configuration.Models;
using TodoListStart.Application.Interfaces;
using Microsoft.OpenApi.Models;
using TodoListStart.Application.Services.Repository;
using TodoListStart.Application.Services.Validation;
using Microsoft.AspNet.OData.Builder;
using TodoListStart.Application.Models;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.OData.Edm;
using Microsoft.AspNetCore.Identity;
using TodoListStart.Application.Services;

namespace TodoListStart.Application
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlite("Data Source=Database");
            }, ServiceLifetime.Transient, ServiceLifetime.Transient);

            services.AddAutoMapper(config => config.AddProfile<ModelProfile>());
            services.AddTransient<IDateTimeService, DateTimeService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRepository, Repository>();
            services.AddTransient<IValidationService, ValidationService>();

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = true; // default
                options.Password.RequiredLength = 4; // min
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<AppDbContext>();
            services.AddMvcCore();
            
            //services.AddMvc(opt => opt.EnableEndpointRouting = false);
            //services.AddOData();
            /*services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TodoStart API", Version = "v1" });
            });*/
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            /*app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TodoStart API");
            });*/

            app.UseRouting();

            /*app.UseMvc(routeBuilder =>
            {
                routeBuilder.EnableDependencyInjection();
                routeBuilder.Select().Expand().Filter().OrderBy().MaxTop(100).Count();
                routeBuilder.MapODataServiceRoute("ODataRoute", "odata", GetEdmModel());
            });*/

            app.UseAuthentication();
            //app.UseMvc();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        /*private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Note>("Notes");
            builder.EntitySet<ListNote>("ListNotes");
            return builder.GetEdmModel();
        }*/
    }
}
