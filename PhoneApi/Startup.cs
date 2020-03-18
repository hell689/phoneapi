
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhoneApi.DBRepository;
using PhoneApi.DBRepository.Interfaces;
using PhoneApi.DBRepository.Repositories;
using PhoneApi.Services;
using PhoneApi.Services.Interfaces;

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
            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddControllers();
            
            services.AddScoped<IRepositoryContextFactory, RepositoryContextFactory>();
            services.AddScoped<IPhoneRepository>(provider => new PhoneRepository(Configuration.GetConnectionString("DefaultConnection"), provider.GetService<IRepositoryContextFactory>()));
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddScoped<IPhoneService, PhoneService>();
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


            //app.UseHttpsRedirection();

            app.UseRouting();
            app.UseStaticFiles();
            app.UseMvc(routes =>
              {
                  routes.MapRoute(
                      name: "DefaultApi",
                      template: "api/{controller}/{action}");
                  routes.MapSpaFallbackRoute("spa-fallback", new { controller = "Home", action = "Index" });
            });

            //app.UseAuthorization();

            /*app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });*/
        }
    }
}
