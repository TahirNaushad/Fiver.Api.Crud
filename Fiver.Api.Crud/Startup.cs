using Fiver.Api.Crud.OtherLayers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Fiver.Api.Crud
{
    public class Startup
    {
        public void ConfigureServices(
            IServiceCollection services)
        {
            services.AddSingleton<IMovieService, MovieService>();
            services.AddSingleton<IReviewService, ReviewService>();

            services.AddMvc();
        }

        public void Configure(
            IApplicationBuilder app, 
            IHostingEnvironment env)
        {
            app.UseExceptionHandler(configure =>
            {
                configure.Run(async context =>
                {
                    var ex = context.Features
                                    .Get<IExceptionHandlerFeature>()
                                    .Error;

                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync($"{ex.Message}");
                });
            });

            app.UseMvcWithDefaultRoute();
        }
    }
}
