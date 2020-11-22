namespace Cardinal.AspNetCore
{
    /// <summary>
    /// Enumerador que define se é requerida uma ou todas as permissões.
    /// </summary>
    public enum PermissionCheckMethod
    {
        /// <summary>
        /// Ao menos uma permissão é requerida.
        /// </summary>
        Any,

        /// <summary>
        /// Todas as permissões são requeridas.
        /// </summary>
        All
    }
}
