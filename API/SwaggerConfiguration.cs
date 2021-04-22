namespace Masivian.Roulette
{
    using Microsoft.Extensions.Configuration;

    public class SwaggerConfiguration
    {
        public SwaggerConfiguration(IConfiguration configuration)
        {
            if (configuration != null)
            {
                EndpointDescription = "Prueba Clean Code";
                EndpointSwaggerJson = "/swagger/v1/swagger.json";
                ContactName = "Santiago Giraldo Escudero";
                ContactUrl = new System.Uri("https://www.epm.com.co/");
                ContactEmail = "santiago.197@hotmail.com";
                DocInfoTitle = "Clean Code API";
                DocInfoVersion = "v1";
                DocInfoDescription = "Prueba entrevista Clean Code";
            }
        }

        public string EndpointDescription { get; }

        public string EndpointSwaggerJson { get; }

        public string ContactName { get; }

        public System.Uri ContactUrl { get; }

        public string ContactEmail { get; }

        public string DocInfoTitle { get; }

        public string DocInfoVersion { get; }
        public string DocInfoDescription { get; }
    }
}