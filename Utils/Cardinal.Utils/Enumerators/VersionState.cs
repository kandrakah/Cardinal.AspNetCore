namespace Cardinal.Utils.Enumerators
{
    /// <summary>
    /// Enumerador que classifica a comparação entre versões.
    /// </summary>
    public enum VersionState
    {
        /// <summary>
        /// Comparação desconhecida.
        /// </summary>
        Unknow,

        /// <summary>
        /// Indica que a versão comparada é mais nova.
        /// </summary>
        Newer,

        /// <summary>
        /// Indica que a versão comparada é mais antiga.
        /// </summary>
        Older,

        /// <summary>
        /// Indica que a versão comparada é equivalente.
        /// </summary>
        Current
    }
}
