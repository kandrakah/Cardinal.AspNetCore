
namespace Cardinal.AspNetCore
{
    /// <summary>
    /// Interface que representa a estrutura da versão de um componente do sistema, seja uma biblioteca, executável ou módulo.
    /// </summary>
    public interface ICardinalVersion
    {
        /// <summary>
        /// Número majoritário de versão.
        /// </summary>
        int Major { get; }

        /// <summary>
        /// Número minoritário de versão.
        /// </summary>
        int Minor { get; }

        /// <summary>
        /// Número de atualização de versão.
        /// </summary>
        int Build { get; }

        /// <summary>
        /// Número de revisão de versão.
        /// </summary>
        int Revision { get; }

        /// <summary>
        /// Método para a comparação entre versões.
        /// </summary>
        /// <param name="version">Versão à ser comparada à esta.</param>
        /// <returns>Enumerador do tipo <see cref="VersionState"/> que indica o resultado da comparação.</returns>
        VersionState Compare(ICardinalVersion version);

        /// <summary>
        /// Método para verifica se esta versão é igual ou mais nova que a versão informada.
        /// </summary>
        /// <param name="version">Versão à ser comparada à esta.</param>
        /// <returns>Verdadeiro caso esta versão seja igual ou mais recente que a versão informada e Falso caso contrário.</returns>
        bool EqualsOrNewer(ICardinalVersion version);

        /// <summary>
        /// Método para verifica se esta versão é igual ou mais antiga que a versão informada.
        /// </summary>
        /// <param name="version">Versão à ser comparada à esta.</param>
        /// <returns>Verdadeiro caso esta versão seja igual ou mais antiga que a versão informada e Falso caso contrário.</returns>
        bool EqualsOrOlder(ICardinalVersion version);
    }
}
