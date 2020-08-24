using Cardinal.AspNetCore.IdentityServer.Enumerators;
using Cardinal.AspNetCore.IdentityServer.Options;
using Microsoft.Extensions.DependencyInjection;

namespace Cardinal.AspNetCore.IdentityServer.Builders
{
    public interface IPasswordHashBuilder
    {
        /// <summary>Gets the services.</summary>
        /// <value>The services.</value>
        IServiceCollection Services { get; }
        ImprovedPasswordHasherOptions Options { get; }
        IPasswordHashBuilder WithMemLimit(int memLimit);

        IPasswordHashBuilder WithOpsLimit(long opsLimit);

        IPasswordHashBuilder WithStrenghten(PasswordHasherStrenght strenght);
        IPasswordHashBuilder ChangeWorkFactor(int workFactor);
        IPasswordHashBuilder ChangeSaltRevision(BcryptSaltRevision saltRevision);
    }
}
