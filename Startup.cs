using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFinal.DAL.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestFinal.Data;
using TestFinal.Models;
using TestFinal.DAL.Interfaces;
using TestFinal.Models.Repositores;

namespace TestFinal
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
            services.AddDbContext<MyDbContext>(options =>
            options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 10;
                options.Password.RequiredUniqueChars = 2;

            }).AddEntityFrameworkStores<MyDbContext>();

            //serviciu de autorizare
            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            }).AddXmlDataContractSerializerFormatters();

            //serviciu de identitate
            services.Configure<IdentityOptions>(options => 
            {
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 2;

            });
            services.AddControllersWithViews();

            //injectare servicii       
            services.AddScoped<IGenericRepository<Subscription>, GenericRepository<Subscription>>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();

            services.AddScoped<IGenericRepository<Offer>, GenericRepository<Offer>>();
            services.AddScoped<IOfferService, OfferService>();

            services.AddScoped<IGenericRepository<Contact>, GenericRepository<Contact>>();
            services.AddScoped<IContactService, ContactService>();

            services.AddScoped<IGenericRepository<Subcategory>, GenericRepository<Subcategory>>();
            services.AddScoped<ISubcategoryServices, SubcategoryService>();

            services.AddScoped<IGenericRepository<Product>, GenericRepository<Product>>();
            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IGenericRepository<Category>, GenericRepository<Category>>();
            services.AddScoped<ICategoryServices, CategoryServices>();

            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ShoppingCart>(sc => ShoppingCart.GetCart(sc));

            services.AddHttpContextAccessor();
            services.AddSession();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
            app.UseSession();


            app.UseRouting();

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
