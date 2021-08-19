using Clinic_Web_Api.Entities;
using Clinic_Web_Api.Helpers;
using Clinic_Web_Api.Middleware;
using Clinic_Web_Api.Models;
using Clinic_Web_Api.Services;
using Clinic_Web_Api.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Web_Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            var defaultConnection = Configuration["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<ClinicDbContext>(options => options.UseSqlServer(defaultConnection));
            // DI
            services.AddTransient<ClinicDbContext>();
            services.AddTransient<IStaffService, StaffService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ISeminarService, SeminaService>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<ILectureService, LectureService>();

            services.AddScoped<IReceiptMedicineService, ReceiptMedicineServiceImplement>();
            services.AddScoped<IDetailOrderService, DetailOrderServiceImplement>();
            services.AddScoped<IReceiptScientificEquipmentService, ReceiptScientificEquipmentServiceImplement>();
            services.AddScoped<IPriceService, PriceMedicineServiceImplement>();
            services.AddScoped<IPriceService, PriceScientificEquipServiceImplement>();
            services.AddScoped<IMedicineService, MedicineServiceImplement>();
            services.AddScoped<IScientificEquipmentService, ScientificEquipmentServiceImplement>();





            // Jwt config
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = Configuration["Jwt:Issuer"],
                     ValidAudience = Configuration["Jwt:Issuer"],
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                 };
             });
            // ignore reference loop ef
            services.AddControllersWithViews()
             .AddNewtonsoftJson(options =>
             options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
         );
            services.AddCors();
            var mailsettings = Configuration.GetSection("MailSettings");  // read config in appsetting.json
            services.Configure<EmailSetting>(mailsettings);               // inject into EmailSetting 
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(builder => builder
             .AllowAnyHeader()
             .AllowAnyMethod()
             .SetIsOriginAllowed((host) => true)
             .AllowCredentials()
            );
            app.UseMiddleware<CorsMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseStaticFiles();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
