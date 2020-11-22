namespace Cardinal.AspNetCore
{
    /// <summary>
    /// Classe estática para nomes de claims padrões.
    /// </summary>
    public static class DefaultClaims
    {
        /// <summary>
        /// Identificador do usuário.
        /// </summary>
        public static string SUB => "sub";

        /// <summary>
        /// Identificador do cliente do usuário.
        /// </summary>
        public static string CLIENT_ID => "client_id";

        /// <summary>
        /// Nome do usuário.
        /// </summary>
        public static string NAME => "name";

        /// <summary>
        /// Nome de família do usuário.
        /// </summary>
        public static string FAMILY_NAME => "family_name";

        /// <summary>
        /// Nome atribuído ao usuário.
        /// </summary>
        public static string GIVEN_NAME => "given_name";

        /// <summary>
        /// Sobrenome do usuário.
        /// </summary>
        public static string MIDDLE_NAME => "middle_name";

        /// <summary>
        /// Apelido do usuário.
        /// </summary>
        public static string NICKNAME => "nickname";

        /// <summary>
        /// Nome de usuário preferencial.
        /// </summary>
        public static string PREFERED_USERNAME => "prefered_username";

        /// <summary>
        /// Perfil do usuário.
        /// </summary>
        public static string PROFILE => "profile";

        /// <summary>
        /// Imagem do usuário.
        /// </summary>
        public static string PICTURE => "picture";

        /// <summary>
        /// Website do usuário.
        /// </summary>
        public static string WEBSITE => "website";

        /// <summary>
        /// Gênero do usuário.
        /// </summary>
        public static string GENDER => "gender";

        /// <summary>
        /// Data de nascimento do usuário.
        /// </summary>
        public static string BIRTHDATE => "birthdate";

        /// <summary>
        /// Informações de zona do usuário.
        /// </summary>
        public static string ZONEINFO => "zoneinfo";

        /// <summary>
        /// Localização do usuário.
        /// </summary>
        public static string LOCALE => "locale";

        /// <summary>
        /// Data de atualização do usuário.
        /// </summary>
        public static string UPDATED_AT => "updated_at";

        /// <summary>
        /// Nome de usuário.
        /// </summary>
        public static string USERNAME => "username";

        /// <summary>
        /// Permissões do usuário.
        /// </summary>
        public static string PERMISSIONS => "permissions";

        /// <summary>
        /// Nome padrão para a permissão mestre.
        /// </summary>
        public static string ROOT_PERMISSION => "ROOT";
    }
}
