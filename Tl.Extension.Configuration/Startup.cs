using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Tl.Extension.Configuration.Services;

namespace Tl.Extension.Configuration
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
            services.AddOptions();
            services.AddSingleton<IPersonService, PersonService>();

            services.Configure<PersonConfig>(Configuration.GetSection("JsonObject"));
            services.Configure<ContactListConfig>(Configuration.GetSection("JsonArray"));
            services.Configure<Dictionary<string, string>>(Configuration.GetSection("dic"));
           

            services.AddControllers();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // ChangeToken.OnChange(Configuration.GetReloadToken, ChangeCallBack);
        }

        public void ChangeCallBack()
        {
            Console.WriteLine("配置发生变化");
            Console.WriteLine($"最新的配置为：{JsonConvert.SerializeObject(Configuration.GetSection("ini").Get<PersonConfig>())}");
        }
    }
}
