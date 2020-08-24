namespace Cardinal.AspNetCore.Identity
{
    /// <summary>
    /// Enumerador para indicar o tipo de validação de múltiplas permissões.
    /// </summary>
    public enum PermissionValidationType
    {
        /// <summary>
        /// Indica possibilidade de acesso anônimo.
        /// </summary>
        Annonymous,
        /// <summary>
        /// Indica que o usuário ativo precisa apenas estar autenticado.
        /// </summary>
        RequireAuthenticatedOnly,
        /// <summary>
        /// Indica que o usuário ativo deve ter ao menos uma das permissões.
        /// </summary>
        RequireOneOf,

        /// <summary>
        /// Indica que o usuário ativo deve ter todas as permissões.
        /// </summary>
        RequireAll
    }
}
