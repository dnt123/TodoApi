﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TodoApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace TodoApi
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
            services.AddScoped<IMyDependency, MyDependency>();
            services.AddSingleton<IFileProvider>(
                new PhysicalFileProvider(Directory.GetCurrentDirectory()));
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddDbContext<TodoContext>(opt =>
              opt.UseInMemoryDatabase("TodoList"));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddMvc(options =>
            {
                options.OutputFormatters.Add(new PdfFormatter());
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = "yourdomain.com",
                   ValidAudience = "yourdomain.com",
                   IssuerSigningKey = new SymmetricSecurityKey(
                       Encoding.UTF8.GetBytes(Configuration["SecurityKey"]))
               };
           });

            services.AddSession();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseAuthentication();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseSession();
            app.UseMvc();
           
        }
    }

    internal class PdfFormatter : IOutputFormatter
    {
        public bool CanWriteResult(OutputFormatterCanWriteContext context)
        {
            throw new NotImplementedException();
        }

        public Task WriteAsync(OutputFormatterWriteContext context)
        {
            throw new NotImplementedException();
        }
    }
}
