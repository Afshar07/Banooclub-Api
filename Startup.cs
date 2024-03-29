using BanooClub.Extensions;
using BanooClub.Extentions;
using BanooClub.HangFire;
using BanooClub.Hubs;
using BanooClub.Services.MessageServices;
using BanooClub.Services.PostServices;
using BanooClub.Settings;
using Hangfire;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace BanooClub
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _jwtSetting = configuration.GetSection(nameof(JwtSetting)).Get<JwtSetting>();
        }
        private readonly JwtSetting _jwtSetting;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
            services.AddControllersWithViews()
               .AddNewtonsoftJson(options =>
               options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
               );
            services.AddCustomHangFireServer(Configuration);
            services.AddStackExchangeRedisCache(options => options.Configuration = this.Configuration.GetConnectionString("redisServerUrl"));
            //services.AddStackExchangeRedisCache(options =>
            //{
            //    options.Configuration = "localhost:6379";
            //});
            services.AddDistributedMemoryCache();
            services.AddDbContext<BanooClubDBContext>(o =>
            { o.UseSqlServer(Configuration.GetConnectionString("BanooClub")); });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHostedService<Services.BackgroundServices.DiscountBackgroundService>();
            services.AddSession();
            services.AddDependency();
            services.AddSettings(Configuration);
            services.AddJwtAuthentication(_jwtSetting);
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BanooClub", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            });
            services.AddCors();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();


            //}

            app.UseMiddleware<ExceptionMiddleware>();


            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BanooClub");
                c.RoutePrefix = string.Empty;
                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
            });
            app.UseHttpsRedirection();

            app.UseCors(o =>
            {
                o.AllowAnyMethod();
                o.AllowAnyHeader();
                o.SetIsOriginAllowed(origin => true);
                o.AllowCredentials();
            });

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCustomHangfireDashbord(Configuration);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<MessageService>("/chatHub");
                endpoints.MapHub<PostService>("/postHub");
                endpoints.MapHangfireDashboard();
            });
        }
    }
}