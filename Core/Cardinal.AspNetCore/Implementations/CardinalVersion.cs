using Cardinal.AspNetCore.Exceptions;
using Cardinal.AspNetCore.Localization;
using Cardinal.AspNetCore.Utils;
using System;
using System.Reflection;

namespace Cardinal.AspNetCore
{
    /// <summary>
    /// Classe que representa a versão de um elemento da aplicação.
    /// </summary>
    public class CardinalVersion : ICardinalVersion
    {
        /// <summary>
        /// Valor maior da versão.
        /// </summary>
        public int Major { get; }

        /// <summary>
        /// Valor menor da versão.
        /// </summary>
        public int Minor { get; }

        /// <summary>
        /// Valor da atualização da versão.
        /// </summary>
        public int Build { get; }

        /// <summary>
        /// Valor da revisão da versão.
        /// </summary>
        public int Revision { get; }

        /// <summary>
        /// Método construtor.
        /// </summary>
        public CardinalVersion()
        {
            var version = Assembly.GetCallingAssembly().GetName().Version;
            this.Major = version.Major;
            this.Minor = version.Minor;
            this.Build = version.Build;
            this.Revision = version.Revision;
        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        public CardinalVersion(string version)
        {
            var values = version.Split('.');
            try
            {
                switch (values.Length)
                {
                    case 4:
                        this.Major = int.Parse(values[0]);
                        this.Minor = int.Parse(values[1]);
                        this.Build = int.Parse(values[2]);
                        this.Revision = int.Parse(values[3]);
                        break;
                    case 3:
                        this.Major = int.Parse(values[0]);
                        this.Minor = int.Parse(values[1]);
                        this.Build = int.Parse(values[2]);
                        break;
                    case 2:
                        this.Major = int.Parse(values[0]);
                        this.Minor = int.Parse(values[1]);
                        break;
                    default:
                        this.Major = 1;
                        this.Minor = 0;
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new InvalidVersionException(ResourceUtils.Translate(Resource.ERROR_WRONG_VERSION_FORMAT, ResourceUtils.Set("VERSION", version)), ex);
            }
        }

        /// <summary>
        /// Método Construtor.
        /// </summary>
        /// <param name="major">Valor maior da versão.</param>
        /// <param name="minor">Valor menor da versão.</param>
        /// <param name="build">Valor de build da versão.</param>
        /// <param name="revision">Valor da revisão da versão.</param>
        public CardinalVersion(int major, int minor, int build = 0, int revision = 0)
        {
            this.Major = major;
            this.Minor = minor;
            this.Build = build;
            this.Revision = revision;
        }

        /// <summary>
        /// Método que faz a comparação entre versõoes.
        /// </summary>
        /// <param name="version">Versão à ser comparada com esta.</param>
        /// <returns>Enumerador representando o resultado da comparação entre as versões.</returns>
        public VersionState Compare(ICardinalVersion version)
        {
            if (version == null)
            {
                return VersionState.Current;
            }

            if (this.Equals(version))
            {
                return VersionState.Current;
            }

            if (this.Major > version.Major)
            {
                return VersionState.Newer;
            }

            if (this.Major < version.Major)
            {
                return VersionState.Older;
            }

            if (this.Minor > version.Minor)
            {
                return VersionState.Newer;
            }

            if (this.Minor < version.Minor)
            {
                return VersionState.Older;
            }

            if (this.Build > version.Build)
            {
                return VersionState.Newer;
            }

            if (this.Build < version.Build)
            {
                return VersionState.Older;
            }

            if (this.Revision > version.Revision)
            {
                return VersionState.Newer;
            }

            if (this.Revision < version.Revision)
            {
                return VersionState.Older;
            }

            return VersionState.Unknow;
        }

        /// <summary>
        /// Método que verifica se a versão informada é igual ou mais nova que a versão registrada nessa instância.
        /// </summary>
        /// <param name="version">Versão à ser comparada.</param>
        /// <returns>Verdadeiro caso a versão informada seja igual ou mais recente que esta ou falso caso contrário.</returns>
        public bool EqualsOrNewer(ICardinalVersion version)
        {
            return (this.Compare(version).Equals(VersionState.Current) || this.Compare(version).Equals(VersionState.Newer));
        }

        /// <summary>
        /// Método que verifica se a versão informada é igual ou mais antiga que a versão registrada nessa instância.
        /// </summary>
        /// <param name="version">Versão à ser comparada.</param>
        /// <returns>Verdadeiro caso a versão informada seja igual ou mais antiga que esta ou falso caso contrário.</returns>
        public bool EqualsOrOlder(ICardinalVersion version)
        {
            return (this.Compare(version).Equals(VersionState.Current) || this.Compare(version).Equals(VersionState.Older));
        }

        /// <summary>
        /// Método que verifica se o objeto informado é igual à esta instância.
        /// </summary>
        /// <param name="obj">Objeto à ser comparado.</param>
        /// <returns>Verdadeiro caso os objetos sejam iguais e Falso caso contrário.</returns>
        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                if (obj.GetType().Equals(typeof(CardinalVersion)))
                {
                    var version = (CardinalVersion)obj;
                    return version != null && this.Major == version.Major && this.Minor == version.Minor && this.Build == version.Build && this.Revision == version.Revision;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// Método que calcula o hashCode desta instância.
        /// </summary>
        /// <returns>HashCode da instância dessa classe.</returns>
        public override int GetHashCode()
        {
            var hashCode = 599407372;
            hashCode = hashCode * -1521134295 + Major.GetHashCode();
            hashCode = hashCode * -1521134295 + Minor.GetHashCode();
            hashCode = hashCode * -1521134295 + Build.GetHashCode();
            hashCode = hashCode * -1521134295 + Revision.GetHashCode();
            return hashCode;
        }

        /// <summary>
        /// Método que retorna a representação string desta instância.
        /// </summary>
        /// <returns>Representação string da instância desta classe.</returns>
        public override string ToString()
        {
            return this.Major + "." + this.Minor + ((this.Build > 0 || this.Revision > 0) ? "." + this.Build : string.Empty) + (this.Revision > 0 ? "." + this.Revision : string.Empty);
        }

        /// <summary>
        /// Método que retorna a representação string desta instância.
        /// </summary>
        /// <param name="detailed">Indica se é necessário detalhar a versão.</param>
        /// <returns>Representação string da instância desta classe.</returns>
        public string ToString(bool detailed)
        {
            if (detailed)
            {
                return this.ToString();
            }
            else
            {
                return $"[Major:{this.Major}],[Minor:{this.Minor}],[Build:{this.Build}],[Revision:{this.Revision}]";
            }
        }
    }
}
