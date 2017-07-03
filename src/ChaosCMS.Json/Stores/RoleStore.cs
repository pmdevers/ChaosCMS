using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ChaosCMS.Json.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace ChaosCMS.Json.Stores
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TRole"></typeparam>
    public class RoleStore<TRole> : JsonStore<TRole>, IRoleStore<TRole>
        where TRole : JsonRole
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="optionsAccessor"></param>
        public RoleStore(IOptions<ChaosJsonStoreOptions> optionsAccessor) : base(optionsAccessor)
        {
        }

        /// <inheritdoc />
        public Task<TRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            var found = this.Collection.FirstOrDefault(x => x.NormalizedName.Equals(normalizedRoleName));
            return Task.FromResult(found);
        }

        /// <inheritdoc />
        public Task<string> GetNormalizedRoleNameAsync(TRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            return Task.FromResult(role.NormalizedName);
        }

        /// <inheritdoc />
        public Task<string> GetRoleIdAsync(TRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            return Task.FromResult(this.ConvertIdToString(role.Id));
        }

        /// <inheritdoc />
        public Task<string> GetRoleNameAsync(TRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            return Task.FromResult(role.Name);
        }

        /// <inheritdoc />
        public Task SetNormalizedRoleNameAsync(TRole role, string normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            role.NormalizedName = normalizedName;

            return Task.FromResult(false);
        }

        /// <inheritdoc />
        public Task SetRoleNameAsync(TRole role, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            role.Name = roleName;

            return Task.FromResult(false);
        }

        /// <inheritdoc />
        async Task<IdentityResult> IRoleStore<TRole>.CreateAsync(TRole role, CancellationToken cancellationToken)
        {
            var result = await CreateAsync(role, cancellationToken);
            if (!result.Succeeded)
            {
                return IdentityResult.Failed(result.Errors.Select(x => new IdentityError { Code = x.Code, Description = x.Description }).ToArray());
            }
            return IdentityResult.Success;
        }

        /// <inheritdoc />
        async Task<IdentityResult> IRoleStore<TRole>.DeleteAsync(TRole role, CancellationToken cancellationToken)
        {
            var result = await DeleteAsync(role, cancellationToken);
            if (!result.Succeeded)
            {
                return IdentityResult.Failed(result.Errors.Select(x => new IdentityError { Code = x.Code, Description = x.Description }).ToArray());
            }
            return IdentityResult.Success;
        }

        /// <inheritdoc />
        async Task<IdentityResult> IRoleStore<TRole>.UpdateAsync(TRole role, CancellationToken cancellationToken)
        {
            var result = await UpdateAsync(role, cancellationToken);
            if (!result.Succeeded)
            {
                return IdentityResult.Failed(result.Errors.Select(x => new IdentityError { Code = x.Code, Description = x.Description }).ToArray());
            }
            return IdentityResult.Success;
        }
    }
}