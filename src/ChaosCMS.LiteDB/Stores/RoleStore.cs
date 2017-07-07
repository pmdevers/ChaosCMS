using ChaosCMS.LiteDB.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace ChaosCMS.LiteDB.Stores
{
    public class RoleStore<TRole> : LiteDBStore<TRole>, IRoleStore<TRole>
        where TRole : LiteDBRole
    {
        public RoleStore(ChaosLiteDBFactory factory) : base(factory)
        {
        }

        public Task<TRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            var role = this.Collection.FindOne(x => x.NormalizedName == normalizedRoleName);
            return Task.FromResult(role);

        }

        public Task<string> GetNormalizedRoleNameAsync(TRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if(role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            return Task.FromResult(role.NormalizedName);
        }

        public Task<string> GetRoleIdAsync(TRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            return Task.FromResult(ConvertIdToString(role.Id));
        }

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

        public Task SetNormalizedRoleNameAsync(TRole role, string normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            role.NormalizedName = normalizedName;
            return Task.FromResult(0);
        }

        public Task SetRoleNameAsync(TRole role, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            role.Name = roleName;
            return Task.FromResult(0);
        }

        async Task<IdentityResult> IRoleStore<TRole>.CreateAsync(TRole role, CancellationToken cancellationToken)
        {
            var result = await base.CreateAsync(role, cancellationToken);
            if (result.Succeeded)
            {
                return IdentityResult.Success;
            }

            return IdentityResult.Failed(result.Errors.Select(x => new IdentityError() { Code = x.Code, Description = x.Description }).ToArray());
        }

        async Task<IdentityResult> IRoleStore<TRole>.DeleteAsync(TRole role, CancellationToken cancellationToken)
        {
            var result = await base.DeleteAsync(role, cancellationToken);
            if (result.Succeeded)
            {
                return IdentityResult.Success;
            }

            return IdentityResult.Failed(result.Errors.Select(x => new IdentityError() { Code = x.Code, Description = x.Description }).ToArray());
        }

        async Task<IdentityResult> IRoleStore<TRole>.UpdateAsync(TRole role, CancellationToken cancellationToken)
        {
            var result = await base.UpdateAsync(role, cancellationToken);
            if (result.Succeeded)
            {
                return IdentityResult.Success;
            }

            return IdentityResult.Failed(result.Errors.Select(x => new IdentityError() { Code = x.Code, Description = x.Description }).ToArray());
        }
    }
}
