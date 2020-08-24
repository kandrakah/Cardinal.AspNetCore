namespace Cardinal.AspNetCore
{
    /// <summary>
    /// Enumerador que define a forma de obtenção das permissões do usuário.
    /// </summary>
    public enum PermissionsValidationType
    {
        /// <summary>
        /// Por claims presentes na identidade.
        /// </summary>
        Claim,

        /// <summary>
        /// Por consulta direta à autoridade.
        /// </summary>
        Authority
    }
}
