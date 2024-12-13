using Inspire.Erp.WebReport.App_Start;
using Swashbuckle.Application;
using System.Web.Http;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]
namespace Inspire.Erp.WebReport.App_Start
{
    public class SwaggerConfig
    {
  
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("Erp API V1", "Inspire Solution");
                })
                .EnableSwaggerUi(c =>
                {
                });
        }
    }
}