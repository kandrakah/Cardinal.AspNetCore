namespace Cardinal.AspNetCore.Identity
{
    /// <summary>
    /// Enumerador que categoriza os tipos de métodos de endpoint para permissões.
    /// </summary>
    public enum Method
    {
        /// <summary>
        /// Método de obtenção de dados.
        /// </summary>
        Get,

        /// <summary>
        /// Método de adição de dados.
        /// </summary>
        Post,

        /// <summary>
        /// Método de atualização de dados.
        /// </summary>
        Put,

        /// <summary>
        /// Método de exclusão de dados.
        /// </summary>
        Delete,

        /// <summary>
        /// Método especial para sistema.
        /// </summary>
        System
    }
}
