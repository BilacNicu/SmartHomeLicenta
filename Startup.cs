using Microsoft.AspNetCore.Builder;

namespace API_Licenta
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // specify the URL of the API
            services.AddHttpClient("MyAPI", c =>
            {
                c.BaseAddress = new Uri("https://localhome:80/");
            });

            // other service configurations
            // ...
        }

    }
}
