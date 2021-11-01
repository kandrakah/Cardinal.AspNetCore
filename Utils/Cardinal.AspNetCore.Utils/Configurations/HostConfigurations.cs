using System.Collections.Generic;

namespace Cardinal.AspNetCore
{
    /// <summary>
    /// Classe de configurações de Host.
    /// </summary>
    public class HostConfigurations
    {
        /// <summary>
        /// Indica se deve ser utilizado o servidor Kestrel.
        /// </summary>
        public bool UseKestrel { get; set; } = true;

        /// <summary>
        /// Indica se deve ser usada a integração com o Internet Information Service.
        /// </summary>
        public bool UseIISIntegration { get; set; } = false;

        /// <summary>
        /// Configurações do certificado.
        /// </summary>
        public HostCertificateConfiguration Certificate { get; set; } = new HostCertificateConfiguration();

        /// <summary>
        /// Lista de hosts ao qual o serviço deve responder.
        /// </summary>
        public IEnumerable<string> Hosts { get; set; }        
    }
}
