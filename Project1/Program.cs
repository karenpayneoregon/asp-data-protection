using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Project1.Classes;
using Project1.Data;

namespace Project1;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddRazorPages();

        builder.Services.AddDataProtection()
            .SetDefaultKeyLifetime(TimeSpan.FromDays(7));

        builder.Services.AddSession(options => {
            options.IdleTimeout = TimeSpan.FromMinutes(30);
        });

        SetupLogging.Development();

        /*
         * Important
         * When configuring the DbContext for development as per below we are exposing
         * sensitive information and should never happen in staging or production thus
         * the environment check.
         */
        if (builder.Environment.IsDevelopment())
        {

            SetupLogging.Development();

            builder.Services.AddDbContextPool<Context>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("OnlyConnection"))
                    .EnableSensitiveDataLogging()
                    .LogTo(new DbContextToFileLogger().Log));
        }
        else
        {

            SetupLogging.Production();

            builder.Services.AddDbContextPool<Context>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("OnlyConnection")));
        }

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseSession();

        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();

        app.Run();
    }
}
