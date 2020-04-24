using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cardinal.AspNetCore
{
    /// <summary>
    /// Classe abstrata que serve de base para todas as entidades do sistema.
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// Índice único da entidade.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// Data de criação da entidade.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Data de modificação da entidade.
        /// </summary>
        public DateTime Modified { get; set; }

        /// <summary>
        /// Data de exclusão da entidade.
        /// </summary>
        public DateTime? Deleted { get; set; }

        /// <summary>
        /// Versão do registro da linha.
        /// </summary>
        public byte[] Version { get; set; }

        /// <summary>
        /// Método que traz uma cadeia de caracteres que representa o objeto atual.
        /// </summary>
        /// <returns>Cadeia de caracteres que representa o objeto atual.</returns>
        public override string ToString()
        {
            return $"[Id:{this.Id}][CREATED:{this.Created}][VERSION:{Version}]";
        }
    }
}
