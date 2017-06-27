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
    public class UserStore<TUser> : LiteDBStore<TUser>, IUserStore<TUser>
        where TUser : LiteDBUser
    {
        public UserStore(IOptions<ChaosLiteDBStoreOptions> optionsAccessor) : base(optionsAccessor)
        {
        }

        public Task<TUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            var user = this.Collection.FindOne(x => x.NormalizedName == normalizedUserName);
            return Task.FromResult(user);
        }

        public Task<string> GetNormalizedUserNameAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.NormalizedName);
        }

        public Task<string> GetUserIdAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(this.ConvertIdToString(user.Id));
        }

        public Task<string> GetUserNameAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.UserName);
        }

        public Task SetNormalizedUserNameAsync(TUser user, string normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            user.NormalizedName = normalizedName;

            return Task.FromResult(0);
        }

        public Task SetUserNameAsync(TUser user, string userName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            user.UserName = userName;

            return Task.FromResult(0);
        }

        async Task<IdentityResult> IUserStore<TUser>.CreateAsync(TUser user, CancellationToken cancellationToken)
        {
            var result = await base.CreateAsync(user, cancellationToken);
            if (result.Succeeded)
            {
                return IdentityResult.Success;
            }

            return IdentityResult.Failed(result.Errors.Select(x => new IdentityError() { Code = x.Code, Description = x.Description }).ToArray());
        }

        async Task<IdentityResult> IUserStore<TUser>.DeleteAsync(TUser user, CancellationToken cancellationToken)
        {
            var result = await base.DeleteAsync(user, cancellationToken);
            if (result.Succeeded)
            {
                return IdentityResult.Success;
            }

            return IdentityResult.Failed(result.Errors.Select(x => new IdentityError() { Code = x.Code, Description = x.Description }).ToArray());
        }

        async Task<IdentityResult> IUserStore<TUser>.UpdateAsync(TUser user, CancellationToken cancellationToken)
        {
            var result = await base.UpdateAsync(user, cancellationToken);
            if (result.Succeeded)
            {
                return IdentityResult.Success;
            }

            return IdentityResult.Failed(result.Errors.Select(x => new IdentityError() { Code = x.Code, Description = x.Description }).ToArray());
        }
    }
}
