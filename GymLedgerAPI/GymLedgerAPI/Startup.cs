using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using GymLedgerAPI.Data;
using Microsoft.EntityFrameworkCore;
using GymLedgerAPI.Domain.Interfaces;
using GymLedgerAPI.Data.Repositories;
using System.Security.Claims;
using GymLedgerAPI.Models;
using System;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using NSwag.Generation.Processors.Security;
using NSwag;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.HttpOverrides;

namespace GymLedgerAPI {
    public class Startup {
        private string _myToken = null;

        public Startup(IConfiguration configuration, IHostEnvironment env) {
            Configuration = configuration;
            Env = env;

        }



        public IConfiguration Configuration { get; }
        public IHostEnvironment Env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {

            services.AddMvc(option => option
                .EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddControllers();
            services.AddRazorPages();
            _myToken = Configuration["Tokens:Key"]; //test of token wel wordt opgehaald




            string connectionString = $"Server=127.0.0.1;Database=Gymledger;User=root;Password=rootroot;Persist Security Info=True";
            //string windowsConnection = Configuration.GetConnectionString("WindowsConnection");


            services.AddDbContextPool<ApplicationDbContext>(options =>
                options.UseMySql(connectionString, mySqlOptions => {
                    mySqlOptions.ServerVersion(new Version(8, 0, 17), ServerType.MySql).DisableBackslashEscaping();
                }
                ));

            //services.AddDbContextPool<ApplicationDbContext>(options =>
            //    options.UseSqlServer(windowsConnection));


            services.Configure<IdentityOptions>(options => {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });


            services.AddScoped<DataInit>();

            //services.AddIdentityCore<User>(cfg => cfg.User.RequireUniqueEmail = true)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            //services.AddAuthorization(options => {
            //    //Function policies
            //    options.AddPolicy("Gymnast", policy => policy.RequireClaim(ClaimTypes.Role, "gymnast"));
            //    options.AddPolicy("Coach", policy => policy.RequireClaim(ClaimTypes.Role, "coach"));
            //    options.AddPolicy("NonUser", policy => policy.RequireClaim(ClaimTypes.Role, "nonuser"));

            //});

            services.AddIdentity<User, IdentityRole>(options => {
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();


            services.AddScoped<IGymnastRepo, GymnastRepo>();
            services.AddScoped<ICoachRepo, CoachRepo>();
            //services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IExerciseRepo, ExerciseRepo>();
            services.AddScoped<ITrainingRepo, TrainingRepo>();
            services.AddScoped<IExerciseEvaluationRepo, ExerciseEvaluationRepo>();
            services.AddScoped<ICategoryRepo, CategoryRepo>();





            services.AddOpenApiDocument(c => {
                c.DocumentName = "apidocs";
                c.Title = "Gymnast Ledger API";
                c.Version = "v1";
                c.Description = "The gymnast API documentation description";

                c.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the textbox: Bearer {your JWT token}."
                });

                c.OperationProcessors.Add(
                    new AspNetCoreOperationSecurityScopeProcessor("JWT")); //adds the token when a request is send
            });



            services.AddAuthentication(x => {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x => {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = true //Ensure token hasn't expired
                };
            });

            services.AddCors(options => {
                options.AddPolicy("CorsPolicy", builder => builder
                    .AllowCredentials()
                    .SetIsOriginAllowed(host => true)
                    .WithMethods("GET", "POST", "PUT", "DELETE")
                    .WithHeaders("Accept", "Authorization", "Content-Type", "Origin", "X-Requested-With")
                );
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataInit dataInit) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });

            app.UseCors("CorsPolicy");

            app.UseForwardedHeaders(new ForwardedHeadersOptions {
                ForwardedHeaders = ForwardedHeaders.All
            });
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            //});

            app.UseOpenApi();
            app.UseSwaggerUi3();
            //app.UseSwagger();
            app.UseReDoc();

            dataInit.InitializeData().Wait();
        }
    }
}
