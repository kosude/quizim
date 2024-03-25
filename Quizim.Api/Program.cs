/*
 *   Copyright (c) 2024 Jack Bennett.
 *   All Rights Reserved.
 *
 *   See the LICENCE file for more information.
 */

using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Quizim.Api.Conventions;

namespace Quizim.Api
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

            builder.Services.AddAuthorization();
            builder.Services.AddControllers(o =>
            {
                o.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
            });

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
                app.UseHsts();
            }

            app.UseAuthorization();
            app.UseHttpsRedirection();

            app.MapControllers();
        }
    }
}
