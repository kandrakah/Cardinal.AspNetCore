namespace Cardinal.Settings
{
    public class SwaggerSecuritySettings
    {
        public SwaggerSecurityDefinitionSettings Definitions { get; set; } = new SwaggerSecurityDefinitionSettings();

        public SwaggerSecurityRequerimentSettings Requeriment { get; set; } = new SwaggerSecurityRequerimentSettings();

        public override string ToString()
        {
            return $"[DEFINITIONS:{this.Definitions}][REQUERIMENTS:{this.Requeriment}]";
        }
    }
}
