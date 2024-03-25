/*
 *   Copyright (c) 2024 Jack Bennett.
 *   All Rights Reserved.
 *
 *   See the LICENCE file for more information.
 */

namespace Quizim.Frontend
{
    class Program
    {
        static void Main(string[] args)
        {
            WebApplication app = BuildApp(args);
            ConfigApp(ref app);

            app.Run();
        }

        static WebApplication BuildApp(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorPages();

            return builder.Build();
        }

        static void ConfigApp(ref WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();
        }
    }
}
