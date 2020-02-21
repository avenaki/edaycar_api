using eDayCar.Domain.Repositories.Concrete;
using eDayCar_api.Repositories;
using eDayCar_api.Services.Abstract;
using eDayCar_api.Services.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace eDayCar_api
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
           

            services.AddSingleton(new MongoContext("mongodb+srv://rwuser:NZkcEuHhzsZGZRgf@cluster0-yzlmt.azure.mongodb.net/test?retryWrites=true"));
            services.AddTransient<IDriverRepository, DriverRepository>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IPassengerRepository, PassengerRepository>();
            services.AddTransient<ITripRepository, TripRepository>();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowMyOrigin",
                builder => { builder.AllowAnyOrigin(); builder.AllowAnyMethod(); builder.AllowAnyHeader(); });
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("AllowMyOrigin"));
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                   .AddJwtBearer(options =>
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

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("AllowMyOrigin");
            app.UseMvc();
        }
    }
}
