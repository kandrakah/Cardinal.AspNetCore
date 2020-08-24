using Cardinal.AspNetCore.Identity.Localization;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Cardinal.AspNetCore
{
    /// <summary>
    /// Classe que representa o resultado de uma validação de token.
    /// </summary>
    public class TokenValidationResult
    {
        /// <summary>
        /// Atributo que indica se o token é válido.
        /// </summary>
        public bool IsValid => this.Status == TokenValidation.Valid;

        /// <summary>
        /// Atributo que indica o status da validação.
        /// </summary>
        public TokenValidation Status { get; }

        /// <summary>
        /// Atributo que indica a mensagem de validação.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Atributo que contém os dados do usuário presentes no token.
        /// </summary>
        public ClaimsPrincipal Principal { get; set; }

        /// <summary>
        /// Atributo que contém os dados validados do token.
        /// </summary>
        public SecurityToken SecurityToken { get; set; }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="status">Status da validação.</param>
        /// <param name="message">Mensagem de validação.</param>
        public TokenValidationResult(TokenValidation status, string message = null)
        {
            this.Status = status;
            this.Message = !string.IsNullOrEmpty(message) ? message : Resource.TOKEN_VALIDATION_VALID;
        }
    }
}
