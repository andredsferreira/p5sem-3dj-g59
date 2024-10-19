using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DDDSample1.Infrastructure;
using DDDSample1.Infrastructure.OperationRequests;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.OperationRequests;
using DDDSample1.Domain.Patients;
using DDDSample1.Infrastructure.Patients;
using DDDSample1.Domain.Staffs;
using DDDSample1.Infrastructure.Staffs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using System;

namespace DDDSample1 {
    public class Startup {

        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {
            // Variáveis para simplificar código
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            var mySqlVersion = MySqlServerVersion.AutoDetect(connectionString);
            var jwtSettings = Configuration.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

            ConfigureMyServices(services);

            services.AddLogging();

            services.AddDbContext<DDDSample1DbContext>();

            services.AddDbContext<IdentityContext>();

            // services.AddControllers().AddNewtonsoftJson();

            services.AddControllers().AddJsonOptions(options => {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });

            services.AddEndpointsApiExplorer();

            services.AddIdentity<IdentityUser, IdentityRole>(options => {
                // TODO: Mudar a politica da password
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<IdentityContext>()
            .AddDefaultTokenProviders();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
                AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters {
                        ValidateIssuer = true,
                        ValidIssuer = jwtSettings["Issuer"],
                        ValidateAudience = true,
                        ValidAudience = jwtSettings["Audience"],
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ClockSkew = TimeSpan.Zero
                    };
                });
        }

        public void ConfigureMyServices(IServiceCollection services) {
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            // Operation request services
            services.AddTransient<IOperationRequestRepository, OperationRequestRepository>();
            services.AddTransient<OperationRequestService>();

            // Patient services
            services.AddTransient<IPatientRepository, PatientRepository>();
            services.AddTransient<PatientService>();

            // Staff services
            services.AddTransient<IStaffRepository, StaffRepository>();
            services.AddTransient<StaffService>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });

        }
    }
}
