using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using NSwag.SwaggerGeneration.Processors.Security;
using NSwag;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using GymLedgerAPI.Data;
using Microsoft.EntityFrameworkCore;
using GymLedgerAPI.Domain.Interfaces;
using GymLedgerAPI.Data.Repositories;

namespace GymLedgerAPI
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

            services.AddControllers();
            services.AddRazorPages();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));

            services.AddScoped<DataInit>();
            services.AddScoped<IGymnastRepo, GymnastRepo>();
            services.AddScoped<ICoachRepo, CoachRepo>();
            services.AddScoped<IExerciseRepo, ExerciseRepo>();
            services.AddScoped<ITrainingRepo, TrainingRepo>();


            services.AddOpenApiDocument(c =>
			{
				c.DocumentName = "apidocs";
				c.Title = "Gymnast Ledger API";
				c.Version = "v1";
				c.Description = "The gymnast API documentation description";
				c.DocumentProcessors.Add(new SecurityDefinitionAppender("JWT Token", new SwaggerSecurityScheme
				{
					Type = SwaggerSecuritySchemeType.ApiKey,
					Name = "Authorization",
					In = SwaggerSecurityApiKeyLocation.Header,
					Description = "Copy 'Bearer' + valid JWT token into field"
				}));
				c.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT Token"));
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


		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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


            app.UseSwaggerUi3();
            app.UseSwagger();
        }
    }
}
