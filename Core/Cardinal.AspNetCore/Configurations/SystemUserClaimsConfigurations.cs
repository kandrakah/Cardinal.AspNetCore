namespace Cardinal.AspNetCore
{
    /// <summary>
    /// Classe de configurações de claims do usuário de sistema.
    /// </summary>
    public class SystemUserClaimsConfigurations
    {
        /// <summary>
        /// Identificador do usuário.
        /// </summary>
        public string Sub { get; set; } = DefaultClaims.SUB;

        /// <summary>
        /// Identificador do cliente do usuário.
        /// </summary>
        public string ClientId { get; set; } = DefaultClaims.CLIENT_ID;

        /// <summary>
        /// Nome do usuário.
        /// </summary>
        public string Name{ get; set; } = DefaultClaims.NAME;

        /// <summary>
        /// Nome de família do usuário.
        /// </summary>
        public string FamilyName { get; set; } = DefaultClaims.FAMILY_NAME;

        /// <summary>
        /// Nome atribuído ao usuário.
        /// </summary>
        public string GivenName { get; set; } = DefaultClaims.GIVEN_NAME;

        /// <summary>
        /// Sobrenome do usuário.
        /// </summary>
        public string MiddleName { get; set; } = DefaultClaims.GIVEN_NAME;

        /// <summary>
        /// Apelido do usuário.
        /// </summary>
        public string Nickname { get; set; } = DefaultClaims.NICKNAME;

        /// <summary>
        /// Nome de usuário preferencial.
        /// </summary>
        public string PreferedUsername { get; set; } = DefaultClaims.PREFERED_USERNAME;

        /// <summary>
        /// Perfil do usuário.
        /// </summary>
        public string Profile { get; set; } = DefaultClaims.PROFILE;

        /// <summary>
        /// Imagem do usuário.
        /// </summary>
        public string Picture { get; set; } = DefaultClaims.PICTURE;

        /// <summary>
        /// Website do usuário.
        /// </summary>
        public string Website { get; set; } = DefaultClaims.WEBSITE;

        /// <summary>
        /// Gênero do usuário.
        /// </summary>
        public string Gender { get; set; } = DefaultClaims.GENDER;

        /// <summary>
        /// Data de nascimento do usuário.
        /// </summary>
        public string Birthdate { get; set; } = DefaultClaims.BIRTHDATE;

        /// <summary>
        /// Informações de zona do usuário.
        /// </summary>
        public string ZoneInfo { get; set; } = DefaultClaims.ZONEINFO;

        /// <summary>
        /// Localização do usuário.
        /// </summary>
        public string Locale { get; set; } = DefaultClaims.LOCALE;

        /// <summary>
        /// Data de atualização do usuário.
        /// </summary>
        public string UpdatedAt { get; set; } = DefaultClaims.UPDATED_AT;

        /// <summary>
        /// Nome de usuário.
        /// </summary>
        public string Username { get; set; } = DefaultClaims.USERNAME;

        /// <summary>
        /// Permissões do usuário.
        /// </summary>
        public string Permissions { get; set; } = DefaultClaims.PERMISSIONS;

        /// <summary>
        /// Método que traz uma cadeia de caracteres que representa o objeto atual.
        /// </summary>
        /// <returns>Cadeia de caracteres que representa o objeto atual.</returns>
        public override string ToString()
        {
            return $"[SystemUserClaims]";
        }
    }
}
