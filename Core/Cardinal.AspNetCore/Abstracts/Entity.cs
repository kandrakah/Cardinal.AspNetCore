using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

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
        /// Versão do registro da linha.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public byte[] Version { get; set; }

        /// <summary>
        /// Método que traz uma cadeia de caracteres que representa o objeto atual.
        /// </summary>
        /// <returns>Cadeia de caracteres que representa o objeto atual.</returns>
        public override string ToString()
        {
            var properties = this.GetType().GetProperty("Name").GetCustomAttributes(true).ToDictionary(a => a.GetType().Name, a => a);
            var result = $"[Id:{this.Id}]";

            foreach(var property in properties)
            {
                result += $"[{property.Key}:{property.Value}]";
            }

            return result;
        }
    }
}
