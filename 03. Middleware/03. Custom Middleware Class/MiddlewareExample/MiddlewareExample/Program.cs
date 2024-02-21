using MiddlewareExample.CustomMiddleware;

namespace MiddlewareExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddTransient<MyCustomMiddleware>();

            var app = builder.Build();

           

            //Middleware Chain 1
            app.Use(async(HttpContext context,RequestDelegate next)=>{
                await context.Response.WriteAsync("1st Middleware starts\n");
                await next(context);
                await context.Response.WriteAsync("1st Middleware Ends\n");

            });

            app.UseMiddleware<MyCustomMiddleware>();

            //Middleware Chain 2
            app.Use(async (HttpContext context, RequestDelegate next) => {
                await context.Response.WriteAsync("3rd Middleware\n");
                await next(context);
            });

            app.Run(async (HttpContext context) => {
                await context.Response.WriteAsync("Terminating Middleware\n");
            });

            app.Run();
        }
    }
}
