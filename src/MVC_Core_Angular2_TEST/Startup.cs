using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using ACME.Models;
//using ACME.IdentityModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
//using SimpleTokenProvider;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace ACME
{
    public class Startup
    {

        private IConfigurationRoot _config;
        private IHostingEnvironment _env;

        private static readonly string secretKey = "secretKey123";

        public Startup(IHostingEnvironment env)
        {
            _env = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(_env.ContentRootPath)
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();

            _config = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
          
       
        {
            services.AddSingleton(_config);

            //services.AddStormpath();
          //  services.AddScoped<TokenProviderMiddleware>();

            services.AddIdentity<ProductUser, IdentityRole>(config =>
             {
                 config.User.RequireUniqueEmail = true;
                 config.Password.RequiredLength = 4;
                 config.Password.RequireDigit = true;
                 config.Password.RequireNonAlphanumeric = true;
                 
               //  config.Cookies.ApplicationCookie.LoginPath
             })
             .AddEntityFrameworkStores<ProductContext>();

            services.AddLogging();

            services.AddDbContext<ProductContext>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ProductContext>();

            //services.AddCors(options => options.AddPolicy("AllowAll", x => x.AllowAnyOrigin()));
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("http://localhost:3000/"));
            });


            // Define CORS Policy
            var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.WithHeaders("*");
            corsBuilder.AllowAnyMethod();
            corsBuilder.WithOrigins("http://localhost:3000");
            //corsBuilder.AllowAnyOrigin();
            corsBuilder.AllowCredentials();
            corsBuilder.WithExposedHeaders().AllowAnyHeader();
            corsBuilder.SetPreflightMaxAge(TimeSpan.FromSeconds(30));

            services.AddCors(options =>
            {
                options.AddPolicy("localhost", corsBuilder.Build());
            });



           // services.AddTransient<ProductContextSeedData>();
            services.AddMvc();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
           // secretKey contains a secret passphrase only your server knows
            //var secretKey = "secretKey";
            //var signinKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            //var options = new TokenProviderOptions
            //{
            //    Audience = "ExampleAudience",
            //    Issuer = "ExampleIssuer",
            //    SigningCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256),
            //};

            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Shows UseCors with CorsPolicyBuilder.
           // app.UseCors("AllowSpecificOrigin");

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Welcome to ACME!");
            //});

            app.UseStaticFiles();


            //var tokenValidationParameters = new TokenValidationParameters
            //{
            //    // The signing key must match!
            //    ValidateIssuerSigningKey = true,
            //    IssuerSigningKey = signinKey,

            //    // Validate the JWT Issuer (iss) claim
            //    ValidateIssuer = true,
            //    ValidIssuer = "ExampleIssuer",

            //    // Validate the JWT Audience (aud) claim
            //    ValidateAudience = true,
            //    ValidAudience = "ExampleAudience",

            //    // Validate the token expiry
            //    ValidateLifetime = true,

            //    // If you want to allow a certain amount of clock drift, set that here:
            //    ClockSkew = TimeSpan.Zero
            //};

            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    AutomaticAuthenticate = true,
            //    AutomaticChallenge = true,
            //    AuthenticationScheme = "Cookie",
            //    CookieName = "access_token",
            //    TicketDataFormat = new CustomJwtDataFormat(
            //        SecurityAlgorithms.HmacSha256,
            //        tokenValidationParameters)
            //});


            //    app.UseIdentity();

            //Add JWT generation endpoint:
            //Uncomment this-------------------------------------------------------------------------------------------------------
            //var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            //var options = new TokenProviderOptions
            //{
            //    Audience = "ExampleAudience",
            //    Issuer = "ExampleIssuer",
            //    SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
            //};

            //app.UseMiddleware<TokenProviderMiddleware>(Options.Create(options));

            //Uncomment this-------------------------------------------------------------------------------------------------------

            // secretKey contains a secret passphrase only your server knows
            var secretKey = "mysupersecret_secretkey!123";
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer = "ExampleIssuer",

                // Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                ValidAudience = "ExampleAudience",

                // Validate the token expiry
                ValidateLifetime = true,

                // If you want to allow a certain amount of clock drift, set that here:
                ClockSkew = TimeSpan.Zero
            };

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = tokenValidationParameters
            });

            //var options = new TokenProviderOptions
            //{
            //    Audience = "ExampleAudience",
            //    Issuer = "ExampleIssuer",
            //    SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
            //};

           // app.UseMiddleware<TokenProviderMiddleware>(Options.Create(options));

            app.UseMvc(config => {
                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "api", action = "products" }
                    );
            });

            app.UseCors("localhost");

          //  seeder.EnsureSeedData().Wait();
        }
    }
}
