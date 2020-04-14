using eDayCar.Domain.Repositories.Concrete;
using eDayCar_api.Controllers;
using eDayCar_api.Repositories;
using eDayCar_api.Repositories.Abstract;
using eDayCar_api.Repositories.Concrete;
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
using System.Text;
using System.Threading.Tasks;

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
            services.AddTransient<IRequestRepository, RequestRepository>();
            services.AddTransient<IChatRepository, ChatRepository>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IPassengerRepository, PassengerRepository>();
            services.AddTransient<ITripRepository, TripRepository>();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowMyOrigin",
                builder => { builder.WithOrigins("http://localhost:4200"); builder.AllowAnyMethod(); builder.AllowAnyHeader(); builder.AllowCredentials(); });
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("AllowMyOrigin"));
            });
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                   .AddJwtBearer(options =>
                   {
                       options.RequireHttpsMetadata = false;
                       options.SaveToken = true;
                       options.TokenValidationParameters = new TokenValidationParameters
                       {
                           ValidateIssuerSigningKey = true,
                           ValidateIssuer = false,
                           ValidateAudience = false,
                           IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("трлолдлжлжлжлжцд"))
                       };
                       options.Events = new JwtBearerEvents
                       {
                           OnMessageReceived = context =>
                           {
                               var accessToken = context.Request.Query["access_token"];

                               // If the request is for our hub...
                               var path = context.HttpContext.Request.Path;
                               if (!string.IsNullOrEmpty(accessToken) &&
                                   (path.ToString().Contains("/MessageHub")))
                               {
                                   // Read the token out of the query string
                                   context.Token = accessToken;
                               }
                               return Task.CompletedTask;
                           }
                       };
                   });
            //for SignalR
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_0);

            services.AddAuthorization();

            services.AddSignalR();
        

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("AllowMyOrigin");
            app.UseAuthentication();

            //for SignalR

            app.UseStaticFiles();

            app.UseSignalR(options =>
            {
                options.MapHub<MessageHub>("/MessageHub");
            });

            app.UseMvc();
        }
    }
}
