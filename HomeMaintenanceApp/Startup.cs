using homeMaintenance.Application.Configurations;
using homeMaintenance.Application.Interfaces;
using homeMaintenance.Application.Mappers;
using homeMaintenance.Application.Ports.In;
using homeMaintenance.Application.Ports.Out;
using homeMaintenance.Application.Queries.GetPositions;
using homeMaintenance.Application.Services;
using homeMaintenance.Application.Services.Helpers;
using homeMaintenance.Infrastructure.Data;
using homeMaintenance.Infrastructure.HostedServices;
using homeMaintenance.Infrastructure.Repositories;
using HomeMaintenanceApp.Web.Extensions;
using MediatR;
using Stripe;
using ReviewService = homeMaintenance.Application.Services.ReviewService;

namespace HomeMaintenanceApp
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add MediatR and AutoMapper
            services.AddMediatR(typeof(PositionsQuery).Assembly);
            services.AddAutoMapper(typeof(AutoMapperProfiles));

            // Application Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IImageStorageService, AwsImageStorageService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
            services.AddScoped<IUserRegistrationService, UserRegistrationService>();

            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddTransient<PositionRepository>();
            services.AddScoped<IDbHelper,DbHelper>();

            // Hosted Services
            services.AddHostedService<SeedDataHostedService>();

            // Congfiguration
            services.AddSingleton<IDbConfig, DbConfig>();
            services.AddSingleton<IAwsConfig, AwsConfig>();

            // Configuration Options
            var stripeSettings = _configuration.GetSection("Stripe").Get<StripeSettings>();
            StripeConfiguration.ApiKey = stripeSettings.SecretKey;

            // Add Framework Services
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.RunApplicationMigrations();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Home Maintenance API v1"));
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors("AllowAllOrigins");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
