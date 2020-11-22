namespace Cardinal.AspNetCore
{
    /// <summary>
    /// Classe de configurações do usuário de sistema.
    /// </summary>
    public class SystemUserConfigurations
    {
        /// <summary>
        /// Nome padrão da permissão mestre.
        /// </summary>
        public string DefaultMasterPermission { get; set; } = DefaultClaims.ROOT_PERMISSION;

        /// <summary>
        /// Definições das claims.
        /// </summary>
        public SystemUserClaimsConfigurations Claims { get; set; } = new SystemUserClaimsConfigurations();

        /// <summary>
        /// Método que traz uma cadeia de caracteres que representa o objeto atual.
        /// </summary>
        /// <returns>Cadeia de caracteres que representa o objeto atual.</returns>
        public override string ToString()
        {
            return $"[SystemUser]";
        }
    }
}
