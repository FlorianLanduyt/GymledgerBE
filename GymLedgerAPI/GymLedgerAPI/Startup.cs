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
using Microsoft.OpenApi.Models;
using System;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GymLedgerAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }
       public IHostEnvironment Env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddRazorPages();


            services.AddMvc(option => option
                .EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);


            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));

            //services.AddDbContextPool<ApplicationDbContext>(options =>
            //     options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));

            if (Env.IsDevelopment())
            {
                string connectionString = $"Server=127.0.0.1;Database=Gymledger;User=root;Password=rootroot";
                services.AddDbContextPool<ApplicationDbContext>(options => options.UseMySql(connectionString, mySqlOptions =>
                {
                    mySqlOptions.ServerVersion(new Version(8, 0, 17), ServerType.MySql).DisableBackslashEscaping();
                }
                ));
            }
        

            services.AddScoped<DataInit>();

            services.AddIdentityCore<User>(cfg => cfg.User.RequireUniqueEmail = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthorization(options => {
                //Function policies
                options.AddPolicy("Gymnast", policy => policy.RequireClaim(ClaimTypes.Role, "gymnast"));
                options.AddPolicy("Coach", policy => policy.RequireClaim(ClaimTypes.Role, "coach"));
                options.AddPolicy("NonUser", policy => policy.RequireClaim(ClaimTypes.Role, "nonuser"));
                
            });

            services.AddScoped<IGymnastRepo, GymnastRepo>();
            services.AddScoped<ICoachRepo, CoachRepo>();
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IExerciseRepo, ExerciseRepo>();
            services.AddScoped<ITrainingRepo, TrainingRepo>();
            services.AddScoped<IExerciseEvaluationRepo, ExerciseEvaluationRepo>();


   //         services.AddOpenApiDocument(c =>
			//{
			//	c.DocumentName = "apidocs";
			//	c.Title = "Gymnast Ledger API";
			//	c.Version = "v1";
			//	c.Description = "The gymnast API documentation description";
			//	c.DocumentProcessors.Add(new SecurityDefinitionAppender("JWT Token", new SwaggerSecurityScheme
			//	{
			//		Type = SwaggerSecuritySchemeType.ApiKey,
			//		Name = "Authorization",
			//		In = SwaggerSecurityApiKeyLocation.Header,
			//		Description = "Copy 'Bearer' + valid JWT token into field"
			//	}));
			//	c.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT Token"));
			//});

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {
                    Title = "Gymnast Ledger API",
                    Version = "v1"
                });
            });


            services.AddAuthentication(x =>
			{
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(x =>
			{
				x.RequireHttpsMetadata = false;
				x.SaveToken = true;
				x.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"])),
					ValidateIssuer = false,
					ValidateAudience = false,
					RequireExpirationTime = true //Ensure token hasn't expired
				};
			});

            services.AddCors(options => options.AddPolicy("AllowAllOrigins", builder => builder.AllowAnyOrigin()));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataInit dataInit)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseCors("AllowAllOrigins");

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseSwagger();

            dataInit.InitializeData().Wait();
        }
    }
}
