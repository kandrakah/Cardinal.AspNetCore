using Cardinal.AspNetCore.IdentityServer.Builders;
using Cardinal.AspNetCore.IdentityServer.Hashers;
using Cardinal.AspNetCore.IdentityServer.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Cardinal.Extensions
{
    public static class IPasswordHashBuilderExtensions
    {
        public static IServiceCollection UseArgon2<TUser>(this IPasswordHashBuilder builder) where TUser : class
        {
            builder.Services.Configure<ImprovedPasswordHasherOptions>(options =>
            {
                options.Strenght = builder.Options.Strenght;
                options.MemLimit = builder.Options.MemLimit;
                options.OpsLimit = builder.Options.OpsLimit;
                options.WorkFactor = builder.Options.WorkFactor;
                options.SaltRevision = builder.Options.SaltRevision;
            });
            return builder.Services.AddScoped<IPasswordHasher<TUser>, Argon2Id<TUser>>();
        }
    }
}
