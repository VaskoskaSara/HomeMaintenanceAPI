using homeMaintenance.Application.Mappers;
using homeMaintenance.Application.Ports.In;
using homeMaintenance.Application.Ports.Out;
using homeMaintenance.Application.Queries.GetPositions;
using homeMaintenance.Application.Services;
using homeMaintenance.Domain.Entities;
using homeMaintenance.Infrastructure.Repositories;
using HomeMaintenanceApp.Web;
using HomeMaintenanceApp.Web.Extensions;
using MediatR;
using System.Reflection;
using TicketApp;

namespace HomeMaintenanceApp
{
    public class Startup
    {
        public readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if(_configuration == null)
            {
                throw new ArgumentNullException();
            }
            services.AddMediatR(typeof(PositionsQuery).Assembly);
            services.AddAutoMapper(typeof(AutoMapperProfiles));
            services.AddScoped<IServiceContainer, ServiceContainer>();
            services.AddScoped<IDbConfig, DbConfig>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITransactionWrapper,TransactionWrapper>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if(app == null)
            {
                throw new ArgumentNullException();
            }

            app.RunApplicationMigrations();

            if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }
                else
                {
                    app.UseExceptionHandler("/Home/Error");
                    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                    app.UseHsts();
                }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseRouting();
            app.UseCors("AllowAllOrigins");
            app.UseAuthentication();
            // app.UseAuthorization();
            // app.UseEndpoints(endpoints => endpoints.MapControllers());
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllers();
            });
        }
    }
}
