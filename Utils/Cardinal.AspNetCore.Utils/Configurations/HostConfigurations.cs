using System.Collections.Generic;

namespace Cardinal.Settings
{
    /// <summary>
    /// Classe de configurações de Host.
    /// </summary>
    public class HostConfigurations
    {
        /// <summary>
        /// Indica se deve ser utilizado o servidor Kestrel.
        /// </summary>
        public bool UseDefaults { get; set; } = true;

        /// <summary>
        /// Indica se deve ser usada a integração com o Internet Information Service.
        /// </summary>
        public bool UseIISIntegration { get; set; } = false;

        /// <summary>
        /// Lista de hosts ao qual o serviço deve responder.
        /// </summary>
        public IEnumerable<string> Hosts { get; set; }
    }
}
