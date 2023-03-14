using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookProject.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace BookProject
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        
        public IConfiguration Configuration { get; set; }
        
        public Startup (IConfiguration temp)
        {
            Configuration = temp;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(); // tells ASP we are using the MVC pattern

            services.AddDbContext<BookstoreContext>(options =>
            {
                options.UseSqlite(Configuration["ConnectionStrings:BookDBConnection"]);
            });

            services.AddScoped<IBookProjectRepository, EFBookProjectRepository>(); // each http request gets its own repository?
            services.AddScoped<IPurchaseRepository, EFPurchaseRepository>();

            services.AddRazorPages();

            services.AddDistributedMemoryCache(); // allows us to use sessions
            services.AddSession();

            services.AddScoped<Cart>(x => SessionCart.GetCart(x)); // when we see this cart, go get the instance of the cart already set up or make a new cart
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles(); // tells ASP to use the files in the wwwroot file

            app.UseSession(); // lets us use sessions

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("typepage",
                     "{category}/{pageNum}",
                     new { Controller = "Home", action = "Index" }
                     );
                endpoints.MapControllerRoute( // this part fixes the url to be cleaner 
                    name: "Paging",
                    pattern: "{pageNum}", // this will be the last part of the url /{pageNum}
                    defaults: new { Controller = "Home", action = "Index", pageNum = 1}
                    );
                endpoints.MapControllerRoute("type",
                    "{category}",
                    new { Controller = "Home", action = "Index", pageNum = 1 });
                endpoints.MapDefaultControllerRoute(); // set up the controller route

                endpoints.MapRazorPages();
            });
        }
    }
}
