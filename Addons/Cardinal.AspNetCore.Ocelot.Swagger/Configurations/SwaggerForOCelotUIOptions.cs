using Swashbuckle.AspNetCore.SwaggerUI;

namespace Cardinal.AspNetCore.Ocelot
{
    /// <summary>
    /// Configuration for Swagger UI.
    /// </summary>
    /// <seealso cref="SwaggerUIOptions" />
    public class SwaggerForOCelotUIOptions : SwaggerUIOptions
    {
        /// <summary>
        /// The end point base path. The final path to swagger endpoint is
        /// <see cref="EndPointBasePath"/> + <see cref="SwaggerEndPointOptions.Key"/>
        /// </summary>
        public string EndPointBasePath { get; set; } = "/swagger/v1";
    }
}
