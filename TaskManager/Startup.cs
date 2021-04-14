using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskManager.Data;
using TaskManager.Repositories;
using TaskManager.Interfaces;
using TaskManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TaskManager.Repositories.SQL;
using System;

namespace TaskManager
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
            services.AddControllers();
            services.AddDbContext<TaskManagerDbContext>(opt => opt.UseSqlServer
            (Configuration.GetConnectionString("TaskManagerConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
             {
                 options.Password.RequireDigit = true;
                 options.Password.RequireLowercase = true;
                 options.Password.RequiredLength = 8;
             }).AddEntityFrameworkStores<TaskManagerDbContext>()
                .AddRoles<IdentityRole>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["AuthorizationSettings:ValidAudience"],
                    ValidIssuer = Configuration["AuthorizationSettings:ValidIssuer"],
                    RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AuthorizationSettings:Key"])),
                    ValidateIssuerSigningKey = true,
                };
                options.SaveToken = true;// TODO: Append line
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                   new Microsoft.OpenApi.Models.OpenApiInfo
                   {
                       Title = "TaskManager",
                       Description = "TaskManager documentation",
                       Version = "v1"
                   });

                options.EnableAnnotations();
            });

            services.AddTransient<IMailService, MailService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IQuestRepository, QuestsRepository>();
            services.AddScoped<IFinishedQuestsRepository, MockFinishedQuestsRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseStaticFiles(); // wwwroot folder access

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            // http://localhost/swagger/
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("v1/swagger.json", "TaskManager"); });
        }
    }
}