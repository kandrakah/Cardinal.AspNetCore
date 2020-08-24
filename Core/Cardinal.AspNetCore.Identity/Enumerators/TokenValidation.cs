namespace Cardinal.AspNetCore
{
    /// <summary>
    /// Enumerador do resultado da validação do token.
    /// </summary>
    public enum TokenValidation
    {
        /// <summary>
        /// Token válido
        /// </summary>
        Valid,

        /// <summary>
        /// Token expirado
        /// </summary>
        Expired,

        /// <summary>
        /// Audiência inválida
        /// </summary>
        InvalidAudience,

        /// <summary>
        /// Token vencido
        /// </summary>
        InvalidLifetime,

        /// <summary>
        /// Assinatura inválida
        /// </summary>
        InvalidSignature,

        /// <summary>
        /// Sem data de expiração.
        /// </summary>
        NoExpiration,

        /// <summary>
        /// Início de validade posterior a atual.
        /// </summary>
        NotYetValid,

        /// <summary>
        /// 
        /// </summary>
        ReplayAdd,

        /// <summary>
        /// 
        /// </summary>
        ReplayDetected,

        /// <summary>
        /// Falha geral
        /// </summary>
        Error,
    }
}
