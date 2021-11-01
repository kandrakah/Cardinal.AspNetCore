namespace Cardinal.AspNetCore
{
    /// <summary>
    /// Classe de configurações do certificado SSL.
    /// </summary>
    public class HostCertificateConfiguration
    {
        /// <summary>
        /// Indica que é necessário usar o certificado.
        /// </summary>
        public bool UseCertificate { get; set; } = false;

        /// <summary>
        /// Fonte do certificado.
        /// </summary>
        public CertificateSource Storage { get; set; } = CertificateSource.FileSystem;

        /// <summary>
        /// Diretório do arquivo de certificado.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Senha do certificado.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Impressão digital do certificado.
        /// </summary>
        public string Thumbprint { get; set; }
    }
}
