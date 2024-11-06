using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Backend.Infrastructure;
using Backend.Infrastructure.OperationRequests;
using Backend.Domain.Shared;
using Backend.Domain.OperationRequests;
using Backend.Domain.Patients;
using Backend.Infrastructure.Patients;
using Backend.Domain.Staffs;
using Backend.Infrastructure.Staffs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using System;
using Backend.Domain.Auth;
using Backend.Infrastructure.Shared.MessageSender;
using Backend.Domain.DomainLogs;
using Backend.Infrastructure.DomainLogs;
using Microsoft.OpenApi.Models;
using Backend.Domain.OperationTypes;
using Backend.Infrastructure.OperationTypes;

namespace Backend;
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

        services.AddHttpContextAccessor();

        ConfigureMyServices(services);

        services.AddLogging();

        services.AddDbContext<AppDbContext>(options => {
            options.UseMySql(connectionString, mySqlVersion, mySqlOptions => {
                mySqlOptions.SchemaBehavior(Pomelo.EntityFrameworkCore.MySql.Infrastructure.MySqlSchemaBehavior.Ignore);
            });
        });

        services.AddControllers().AddJsonOptions(options => {
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
        });

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options => {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter the token"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                },
                    new string[] {} // No specific scopes required
                }
            });

        });

        services.AddIdentity<IdentityUser, IdentityRole>(options => {
            // TODO: Mudar a politica da password
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 6;
        })
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();

        services.AddAuthentication(options => {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).
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

        services.AddAuthorization(options => {
            options.AddPolicy(HospitalRoles.Admin, policy => policy.RequireRole(HospitalRoles.Admin));
            options.AddPolicy(HospitalRoles.Doctor, policy => policy.RequireRole(HospitalRoles.Doctor));
            options.AddPolicy(HospitalRoles.Nurse, policy => policy.RequireRole(HospitalRoles.Nurse));
            options.AddPolicy(HospitalRoles.Technician, policy => policy.RequireRole(HospitalRoles.Technician));
            options.AddPolicy(HospitalRoles.Patient, policy => policy.RequireRole(HospitalRoles.Patient));
        });

        services.AddCors(options => {
            options.AddPolicy("AllowSpecificOrigin", builder => {
                builder.WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();
            });
        });
    }

    public void ConfigureMyServices(IServiceCollection services) {
        services.AddMvc(options => { options.SuppressAsyncSuffixInActionNames = false; });
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

        // Operation type
        services.AddTransient<IOperationTypeRepository, OperationTypeRepository>();
        services.AddTransient<AddOperationTypeService>();

        services.AddTransient<IDomainLogRepository, DomainLogRepository>();
        services.AddTransient<IMessageSenderService, EmailSenderService>();

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {

        app.UseCors("AllowSpecificOrigin");

        if (env.IsDevelopment()) {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else {
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();


        app.UseEndpoints(endpoints => {
            endpoints.MapControllers();
        });

    }
}