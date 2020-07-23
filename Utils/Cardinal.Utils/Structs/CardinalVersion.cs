using Cardinal.Exceptions;
using Cardinal.Extensions;
using Cardinal.Utils.Enumerators;
using Cardinal.Utils.Localization;
using System;
using System.Text.RegularExpressions;

namespace Cardinal.Utils
{
    /// <summary>
    /// Representa uma versão de arquivo.
    /// </summary>
    public struct CardinalVersion : IComparable, IComparable<CardinalVersion>, IComparable<string>, IEquatable<CardinalVersion>, IEquatable<string>
    {
        /// <summary>
        /// Versão principal.
        /// </summary>
        public int Major { get; private set; }

        /// <summary>
        /// Versão secundária.
        /// </summary>
        public int Minor { get; private set; }

        /// <summary>
        /// Versão de atualização.
        /// </summary>
        public int Update { get; private set; }

        /// <summary>
        /// Versão de revisão.
        /// </summary>
        public int Revision { get; private set; }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="major">Versão principal</param>
        /// <param name="minor">Versão secundária</param>
        /// <param name="update">Versão de atualização</param>
        /// <param name="revision">Versão de revisão</param>
        internal CardinalVersion(int major = 0, int minor = 0, int update = 0, int revision = 0)
        {
            Major = major;
            Minor = minor;
            Update = update;
            Revision = revision;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool operator ==(CardinalVersion first, CardinalVersion second) => first.Equals(second);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool operator !=(CardinalVersion first, CardinalVersion second) => !first.Equals(second);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool operator >(CardinalVersion first, CardinalVersion second) => first.Newer(second);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool operator <(CardinalVersion first, CardinalVersion second) => first.Older(second);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool operator >=(CardinalVersion first, CardinalVersion second) => first.Equals(second) || first.Newer(second);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool operator <=(CardinalVersion first, CardinalVersion second) => first.Equals(second) || first.Older(second);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool operator >(CardinalVersion first, string second) => first.Newer(second);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool operator <(CardinalVersion first, string second) => first.Older(second);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool operator >=(CardinalVersion first, string second) => first.Equals(second) || first.Newer(second);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool operator <=(CardinalVersion first, string second) => first.Equals(second) || first.Older(second);

        /// <summary>
        /// Método estático para definir uma versão.
        /// </summary>
        /// <param name="major">Versão principal</param>
        /// <param name="minor">Versão secundária</param>
        /// <param name="update">Versão de atualização</param>
        /// <param name="revision">Versão de revisão</param>
        /// <returns>Instância de <see cref="CardinalVersion"/></returns>
        public static CardinalVersion Set(int major = 0, int minor = 0, int update = 0, int revision = 0)
        {
            return new CardinalVersion(major, minor, update, revision);
        }

        /// <summary>
        /// Método para transformar uma string válida em <see cref="CardinalVersion"/>.
        /// </summary>
        /// <param name="version">Valor string da versão</param>
        /// <returns>Instância de <see cref="CardinalVersion"/></returns>
        public static CardinalVersion Parse(string version)
        {
            var regex = new Regex(@"\d+(\.\d+)+");
            if (!regex.IsMatch(version))
            {
                throw new InvalidVersionException(Resource.ERROR_WRONG_VERSION_FORMAT.SetParameters("VERSION", version));
            }
            var result = default(CardinalVersion);
            var values = version.Split('.');
            try
            {
                switch (values.Length)
                {
                    case 4:
                        result = new CardinalVersion(int.Parse(values[0]), int.Parse(values[1]), int.Parse(values[2]), int.Parse(values[3]));
                        break;
                    case 3:
                        result = new CardinalVersion(int.Parse(values[0]), int.Parse(values[1]), int.Parse(values[2]));
                        break;
                    case 2:
                        result = new CardinalVersion(int.Parse(values[0]), int.Parse(values[1]));
                        break;
                    default:
                        result = new CardinalVersion(int.Parse(values[0]));
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new CardinalException(ex.Message, ex);
            }

            return result;
        }

        /// <summary>
        /// Método para transformar um <see cref="Version"/> válida em <see cref="CardinalVersion"/>.
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public static CardinalVersion Parse(Version version)
        {
            return Parse(version.ToString());
        }

        /// <summary>
        /// Método para comparar um objeto à esta versão.
        /// </summary>
        /// <param name="obj">Objeto à ser comparado</param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (obj is CardinalVersion)
            {
                return CompareTo((CardinalVersion)obj);
            }
            else
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Método para comparar uma versão à esta.
        /// </summary>
        /// <param name="other">Objeto à ser comparado</param>
        /// <returns></returns>
        public int CompareTo(CardinalVersion other)
        {
            var result = Compare(other);
            switch (result)
            {
                case VersionState.Current:
                    return 0;
                case VersionState.Newer:
                    return 1;
                case VersionState.Older:
                    return -1;
                default:
                    return -1;
            }
        }

        /// <summary>
        /// Método para comparar uma string à esta versão.
        /// </summary>
        /// <param name="version">Valor string da versão</param>
        /// <returns></returns>
        public int CompareTo(string version)
        {
            var other = Parse(version);
            return CompareTo(other);
        }

        /// <summary>
        /// Método para verfificar se a versão informada é igual à esta.
        /// </summary>
        /// <param name="other">Versão à ser comparada</param>
        /// <returns>Verdadeiro caso as versões sejam iguais ou falso caso contrário</returns>
        public bool Equals(CardinalVersion other)
        {
            return this.Compare(other) == VersionState.Current;
        }

        /// <summary>
        /// Método para verfificar se a versão informada é igual à esta.
        /// </summary>
        /// <param name="version">Versão à ser comparada</param>
        /// <returns>Verdadeiro caso as versões sejam iguais ou falso caso contrário</returns>
        public bool Equals(string version)
        {
            var other = Parse(version);
            return Equals(other);
        }

        /// <summary>
        /// Método para verfificar se a versão informada é mais recente à esta.
        /// </summary>
        /// <param name="other">Versão à ser comparada</param>
        /// <returns>Verdadeiro caso a versão informada seja mais recente ou falso caso contrário</returns>
        public bool Newer(CardinalVersion other)
        {
            return this.Compare(other) == VersionState.Newer;
        }

        /// <summary>
        /// Método para verfificar se a versão informada é mais recente à esta.
        /// </summary>
        /// <param name="version">Versão à ser comparada</param>
        /// <returns>Verdadeiro caso a versão informada seja mais recente ou falso caso contrário</returns>
        public bool Newer(string version)
        {
            var other = Parse(version);
            return this.Compare(other) == VersionState.Newer;
        }

        /// <summary>
        /// Método para verfificar se a versão informada é anterior à esta.
        /// </summary>
        /// <param name="other">Versão à ser comparada</param>
        /// <returns>Verdadeiro caso a versão informada seja mais antiga ou falso caso contrário</returns>
        public bool Older(CardinalVersion other)
        {
            return this.Compare(other) == VersionState.Older;
        }

        /// <summary>
        /// Método para verfificar se a versão informada é anterior à esta.
        /// </summary>
        /// <param name="version">Versão à ser comparada</param>
        /// <returns>Verdadeiro caso a versão informada seja mais antiga ou falso caso contrário</returns>
        public bool Older(string version)
        {
            var other = Parse(version);
            return this.Compare(other) == VersionState.Older;
        }

        /// <summary>
        /// Método que faz a comparação entre versõoes.
        /// </summary>
        /// <param name="other">Versão à ser comparada com esta.</param>
        /// <returns>Enumerador representando o resultado da comparação entre as versões.</returns>
        public VersionState Compare(CardinalVersion other)
        {
            if (this.Major == other.Major && this.Minor == other.Minor && this.Update == other.Update && this.Revision == other.Revision)
            {
                return VersionState.Current;
            }

            if (this.Major > other.Major)
            {
                return VersionState.Newer;
            }

            if (this.Major < other.Major)
            {
                return VersionState.Older;
            }

            if (this.Minor > other.Minor)
            {
                return VersionState.Newer;
            }

            if (this.Minor < other.Minor)
            {
                return VersionState.Older;
            }

            if (this.Update > other.Update)
            {
                return VersionState.Newer;
            }

            if (this.Update < other.Update)
            {
                return VersionState.Older;
            }

            if (this.Revision > other.Revision)
            {
                return VersionState.Newer;
            }

            if (this.Revision < other.Revision)
            {
                return VersionState.Older;
            }

            return VersionState.Unknow;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is CardinalVersion)
            {
                return Compare((CardinalVersion)obj) == VersionState.Current;
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
            hashCode = hashCode * -1521134295 + Update.GetHashCode();
            hashCode = hashCode * -1521134295 + Revision.GetHashCode();
            return hashCode;
        }

        /// <summary>
        /// Método que retorna a representação string desta instância.
        /// </summary>
        /// <returns>Representação string da instância desta classe.</returns>
        public override string ToString()
        {
            return this.Major + "." + this.Minor + ((this.Update > 0 || this.Revision > 0) ? "." + this.Update : string.Empty) + (this.Revision > 0 ? "." + this.Revision : string.Empty);
        }

        /// <summary>
        /// Método que retorna a representação string desta instância.
        /// </summary>
        /// <param name="showComplete">Indica se deve ser exibida a versão no formato completo.</param>
        /// <returns>Representação string da instância desta classe.</returns>
        public string ToString(bool showComplete)
        {
            return showComplete ? $"{this.Major}.{this.Minor}.{this.Update}].{this.Revision}" : this.ToString();
        }
    }
}
