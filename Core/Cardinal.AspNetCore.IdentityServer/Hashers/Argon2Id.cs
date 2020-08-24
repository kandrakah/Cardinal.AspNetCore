using Cardinal.AspNetCore.IdentityServer.Enumerators;
using Cardinal.AspNetCore.IdentityServer.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Sodium;
using System;
using System.Runtime.InteropServices;

namespace Cardinal.AspNetCore.IdentityServer.Hashers
{
    public class Argon2Id<TUser> : IPasswordHasher<TUser> where TUser : class
    {
        private readonly ImprovedPasswordHasherOptions _options;

        /// <summary>
        /// Creates a new instance of <see cref="PasswordHasher{TUser}"/>.
        /// </summary>
        /// <param name="optionsAccessor">The options for this instance.</param>
        public Argon2Id(IOptions<ImprovedPasswordHasherOptions> optionsAccessor = null)
        {
            _options = optionsAccessor?.Value ?? new ImprovedPasswordHasherOptions();
        }

        public string HashPassword(TUser user, string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password), $"{nameof(password)} should not be null");
            }

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), $"{nameof(user)} should not be null");
            }

            if (_options.OpsLimit.HasValue && _options.MemLimit.HasValue)
            {
                return PasswordHash.ArgonHashString(password, _options.OpsLimit.Value, _options.MemLimit.Value);
            }

            // Removing trailing 0x00. Some database providers doesnt support it.:
            // https://github.com/npgsql/efcore.pg/issues/1069
            return _options.Strenght switch
            {
                PasswordHasherStrenght.Interactive => PasswordHash.ArgonHashString(password).Replace("\0", string.Empty),
                PasswordHasherStrenght.Moderate => PasswordHash.ArgonHashString(password, PasswordHash.StrengthArgon.Moderate).Replace("\0", string.Empty),
                PasswordHasherStrenght.Sensitive => PasswordHash.ArgonHashString(password, PasswordHash.StrengthArgon.Sensitive).Replace("\0", string.Empty),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public PasswordVerificationResult VerifyHashedPassword(TUser user, string hashedPassword, string providedPassword)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), $"{nameof(user)} should not be null");
            }

            if (string.IsNullOrEmpty(hashedPassword))
            {
                throw new ArgumentNullException(nameof(hashedPassword), $"{nameof(hashedPassword)} should not be null");
            }

            if (string.IsNullOrEmpty(providedPassword))
            {
                throw new ArgumentNullException(nameof(providedPassword), $"{nameof(providedPassword)} should not be null");
            }

            return PasswordHash.ArgonHashStringVerify(hashedPassword, providedPassword) ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
        }


    }

    public static class SodiumLb
    {
        [DllImport("libsodium", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int crypto_pwhash_str(
            byte[] buffer,
            byte[] password,
            long passwordLen,
            long opsLimit,
            int memLimit);
    }
}
