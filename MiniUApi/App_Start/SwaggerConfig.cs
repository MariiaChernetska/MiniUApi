using System.Web.Http;
using WebActivatorEx;
using MiniUApi;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace MiniUApi
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration 
                .EnableSwagger(c =>
                    {
                        
                        
                        c.SingleApiVersion("v1", "MiniUApi");

                        
                    })
                .EnableSwaggerUi(c =>
                    {
                        
                    });
        }
    }
}
