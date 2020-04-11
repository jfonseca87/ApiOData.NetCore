using ApiODATACore.Models;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OData.Edm;

namespace ApiODATACore
{
    public class Startup
    {
        private readonly IConfiguration _conf;

        public Startup(IConfiguration conf)
        {
            _conf = conf;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options => options.EnableEndpointRouting = false);

            services.AddOData();

            services.AddDbContext<TestContext>(options => {
                options.UseSqlServer(_conf.GetConnectionString("SqlConnection"));
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseMvc(configure =>
            {
                configure.Select().Filter().MaxTop(100).Count().Expand();
                configure.MapODataServiceRoute(
                    "odata",
                    null,
                    GetEdmModel()
                );
            });
        }

        IEdmModel GetEdmModel()
        {
            var odataBuilder = new ODataConventionModelBuilder();
            odataBuilder.EntitySet<People>("People");
            odataBuilder.EntitySet<Comments>("Comments");

            return odataBuilder.GetEdmModel();
        }
    }
}
