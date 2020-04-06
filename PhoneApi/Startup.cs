
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using PhoneApi.DBRepository;
using PhoneApi.DBRepository.Interfaces;
using PhoneApi.DBRepository.Repositories;
using PhoneApi.Services;
using PhoneApi.Services.Interfaces;
using PhoneApi.Utils.Auth;

namespace PhoneApi
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
            services.AddMvc();
            services.AddControllers();
            
            services.AddScoped<IRepositoryContextFactory, RepositoryContextFactory>();
            services.AddScoped<IPhoneRepository>(provider => new PhoneRepository(Configuration.GetConnectionString("DefaultConnection"), provider.GetService<IRepositoryContextFactory>()));
            services.AddScoped<ICabinetRepository>(provider => new CabinetRepository(Configuration.GetConnectionString("DefaultConnection"), provider.GetService<IRepositoryContextFactory>()));
            services.AddScoped<IEmployeeRepository>(provider => new EmployeeRepository(Configuration.GetConnectionString("DefaultConnection"), provider.GetService<IRepositoryContextFactory>()));
            services.AddScoped<ICabinetPhoneRepository>(provider => new CabinetPhoneRepository(Configuration.GetConnectionString("DefaultConnection"), provider.GetService<IRepositoryContextFactory>()));
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddScoped<IPhoneService, PhoneService>();
            services.AddScoped<ICabinetService, CabinetService>();
            services.AddScoped<ICabinetPhoneService, CabinetPhoneService>();
            services.AddScoped<IEmployeeService, EmployeeService>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
           {
               options.RequireHttpsMetadata = false;
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidIssuer = AuthOptions.ISSUER,
                   ValidateAudience = true,
                   ValidAudience = AuthOptions.AUDIENCE,
                   ValidateLifetime = true,
                   IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                   ValidateIssuerSigningKey = true,
               };
           });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [System.Obsolete]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware();
            }

            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "PhoneApi",
                    pattern: "api/phone/{id?}",
                    defaults: new { controller = "Phone" });
                endpoints.MapControllerRoute(
                    name: "CabinetApi",
                    pattern: "api/cabinet/{id?}",
                    defaults: new { controller = "Cabinet" });
                endpoints.MapControllerRoute(
                    name: "EmployeeApi",
                    pattern: "api/employee/{id?}",
                    defaults: new { controller = "Employee" });
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "",
                    defaults: new { controller = "Home", action = "Index" });
            });
            
        }
    }
}
