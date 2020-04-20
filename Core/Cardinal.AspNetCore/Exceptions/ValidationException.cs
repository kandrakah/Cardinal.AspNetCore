using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json;

namespace Cardinal.AspNetCore.Exceptions
{
    public class Validation
    {
        public string Field { get; }

        public string Message { get; }

        public Validation(string field, string message)
        {
            this.Field = field;
            this.Message = message;
        }

        public override string ToString()
        {
            return $"[FIELD:{this.Field}][MESSAGE:{this.Message}]";
        }
    }

    public class ValidationResponse
    {
        public string Message { get; set; }

        public IEnumerable<Validation> Validations { get; set; }
    }

    public class ValidationException : CardinalException
    {
        /// <summary>
        /// Coleção de validações contidas na exceção.
        /// </summary>
        public ICollection<Validation> Validations { get; }

        /// <summary>
        /// Método construtor.
        /// </summary>
        public ValidationException() : base()
        {
            this.Validations = new List<Validation>();
        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="message">Mensagem de exceção.</param>
        public ValidationException(string message) : base(message)
        {
            this.Validations = new List<Validation>();
        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="message">Mensagem de exceção.</param>
        /// <param name="innerException">Exceção interior.</param>
        public ValidationException(string message, Exception innerException) : base(message, innerException)
        {
            this.Validations = new List<Validation>();
        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="info"> The System.Runtime.Serialization.SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The System.Runtime.Serialization.StreamingContext that contains contextual information about the source or destination.</param>
        public ValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            this.Validations = new List<Validation>();
        }

        /// <summary>
        /// Método que adiciona uma mensagem de validação à lista.
        /// </summary>
        /// <param name="field">Campo relacionado à validação.</param>
        /// <param name="message">Mensagem de validação.</param>
        public void Add(string field, string message)
        {
            this.Validations.Add(new Validation(field, message));
        }

        /// <summary>
        /// Método que indica se há mensagens de validação na exceção.
        /// </summary>
        /// <returns>Verdadeiro caso exista alguma mensagem de validação ou falso caso contrário.</returns>
        public bool Any()
        {
            return this.Validations.Any();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ValidationResponse GetValidationResponse()
        {
            var response = new ValidationResponse (){ Message = this.Message, Validations = this.Validations };
            return response;
        }
    }
}
