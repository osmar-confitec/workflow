
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WorkFlowIdentity.Models
{
    public class UserStore : IUserStore<ApplicationUserWidoutEntity>,
                                IUserEmailStore<ApplicationUserWidoutEntity>,
                                IUserPhoneNumberStore<ApplicationUserWidoutEntity>,
                                IUserTwoFactorStore<ApplicationUserWidoutEntity>,
                                IUserPasswordStore<ApplicationUserWidoutEntity>,
                                IUserClaimStore<ApplicationUserWidoutEntity>,
                                IUserRoleStore<ApplicationUserWidoutEntity>

    {

        readonly string _connectionString;
        readonly IConfiguration _configuration;

        public UserStore(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }


        public async Task<IdentityResult> CreateAsync(ApplicationUserWidoutEntity user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);

                var query = $@"INSERT INTO [AspNetUsers] 
                                (  
                                    [Id],
                                    [UserName], 
                                    [NormalizedUserName], 
                                    [Email],
                                    [NormalizedEmail], 
                                    [EmailConfirmed], 
                                    [PasswordHash], 
                                    [PhoneNumber], 
                                    [PhoneNumberConfirmed], 
                                    [TwoFactorEnabled],
                                    [LockoutEnabled],
                                    [AccessFailedCount]
                                        )
                                VALUES (
                                    @Id,
                                    @UserName, 
                                    @NormalizedUserName, 
                                    @Email,
                                    @NormalizedEmail, 
                                    @EmailConfirmed, 
                                    @PasswordHash,
                                    @PhoneNumber, 
                                    @PhoneNumberConfirmed, 
                                    @TwoFactorEnabled,
                                    @LockoutEnabled,
                                    @AccessFailedCount
        
    );
                                SELECT CAST(SCOPE_IDENTITY() as int)";

                await connection.ExecuteAsync(query, user);
            }

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(ApplicationUserWidoutEntity user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                await connection.ExecuteAsync($"DELETE FROM [AspNetUsers] WHERE [Id] = @{nameof(ApplicationUserWidoutEntity.Id)}", user);
            }

            return IdentityResult.Success;
        }

        public async Task<ApplicationUserWidoutEntity> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                return await connection.QuerySingleOrDefaultAsync<ApplicationUserWidoutEntity>($@"SELECT * FROM [AspNetUsers]
                    WHERE [Id] = @{nameof(userId)}", new { userId });
            }
        }

        public async Task<ApplicationUserWidoutEntity> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                return await connection.QuerySingleOrDefaultAsync<ApplicationUserWidoutEntity>($@"SELECT * FROM [AspNetUsers]
                    WHERE [NormalizedUserName] = @{nameof(normalizedUserName)}", new { normalizedUserName });
            }
        }

        public Task<string> GetNormalizedUserNameAsync(ApplicationUserWidoutEntity user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetUserIdAsync(ApplicationUserWidoutEntity user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(ApplicationUserWidoutEntity user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task SetNormalizedUserNameAsync(ApplicationUserWidoutEntity user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.FromResult(0);
        }

        public Task SetUserNameAsync(ApplicationUserWidoutEntity user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.FromResult(0);
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationUserWidoutEntity user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                await connection.ExecuteAsync($@"UPDATE [AspNetUsers] SET
                    [UserName] = @{nameof(ApplicationUserWidoutEntity.UserName)},
                    [NormalizedUserName] = @{nameof(ApplicationUserWidoutEntity.NormalizedUserName)},
                    [Email] = @{nameof(ApplicationUserWidoutEntity.Email)},
                    [NormalizedEmail] = @{nameof(ApplicationUserWidoutEntity.NormalizedEmail)},
                    [EmailConfirmed] = @{nameof(ApplicationUserWidoutEntity.EmailConfirmed)},
                    [PasswordHash] = @{nameof(ApplicationUserWidoutEntity.PasswordHash)},
                    [PhoneNumber] = @{nameof(ApplicationUserWidoutEntity.PhoneNumber)},
                    [PhoneNumberConfirmed] = @{nameof(ApplicationUserWidoutEntity.PhoneNumberConfirmed)},
                    [TwoFactorEnabled] = @{nameof(ApplicationUserWidoutEntity.TwoFactorEnabled)}
                    WHERE [Id] = @{nameof(ApplicationUserWidoutEntity.Id)}", user);
            }

            return IdentityResult.Success;
        }

        public Task SetEmailAsync(ApplicationUserWidoutEntity user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            return Task.FromResult(0);
        }

        public Task<string> GetEmailAsync(ApplicationUserWidoutEntity user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(ApplicationUserWidoutEntity user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(ApplicationUserWidoutEntity user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public async Task<ApplicationUserWidoutEntity> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                return await connection.QuerySingleOrDefaultAsync<ApplicationUserWidoutEntity>($@"SELECT * FROM [AspNetUsers]
                    WHERE [NormalizedEmail] = @{nameof(normalizedEmail)}", new { normalizedEmail });
            }
        }

        public Task<string> GetNormalizedEmailAsync(ApplicationUserWidoutEntity user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedEmail);
        }

        public Task SetNormalizedEmailAsync(ApplicationUserWidoutEntity user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            return Task.FromResult(0);
        }

        public Task SetPhoneNumberAsync(ApplicationUserWidoutEntity user, string phoneNumber, CancellationToken cancellationToken)
        {
            user.PhoneNumber = phoneNumber;
            return Task.FromResult(0);
        }

        public Task<string> GetPhoneNumberAsync(ApplicationUserWidoutEntity user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(ApplicationUserWidoutEntity user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        public Task SetPhoneNumberConfirmedAsync(ApplicationUserWidoutEntity user, bool confirmed, CancellationToken cancellationToken)
        {
            user.PhoneNumberConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public Task SetTwoFactorEnabledAsync(ApplicationUserWidoutEntity user, bool enabled, CancellationToken cancellationToken)
        {
            user.TwoFactorEnabled = enabled;
            return Task.FromResult(0);
        }

        public Task<bool> GetTwoFactorEnabledAsync(ApplicationUserWidoutEntity user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.TwoFactorEnabled);
        }

        public Task SetPasswordHashAsync(ApplicationUserWidoutEntity user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(ApplicationUserWidoutEntity user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(ApplicationUserWidoutEntity user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        public async Task AddToRoleAsync(ApplicationUserWidoutEntity user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var normalizedName = roleName.ToUpper();
                var roleId = await connection.ExecuteScalarAsync<string>($"SELECT [Id] FROM [AspNetRoles] WHERE [NormalizedName] = @{nameof(normalizedName)}", new { normalizedName });
                if (string.IsNullOrEmpty(roleId))
                    roleId = await connection.ExecuteScalarAsync($"INSERT INTO [AspNetRoles]([Name], [NormalizedName]) VALUES(@{nameof(roleName)}, @{nameof(normalizedName)}); SELECT SCOPE_IDENTITY();",
                        new { roleName, normalizedName }) as string;

                await connection.ExecuteAsync($"IF NOT EXISTS(SELECT 1 FROM [AspNetUserRoles] WHERE [UserId] = @userId AND [RoleId] = @{nameof(roleId)}) " +
                    $"INSERT INTO [AspNetUserRoles]([UserId], [RoleId]) VALUES(@userId, @{nameof(roleId)})",
                    new { userId = user.Id, roleId });
            }
        }

        public async Task RemoveFromRoleAsync(ApplicationUserWidoutEntity user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var roleId = await connection.ExecuteScalarAsync<int?>("SELECT [Id] FROM [AspNetRoles] WHERE [NormalizedName] = @normalizedName", new { normalizedName = roleName.ToUpper() });
                if (!roleId.HasValue)
                    await connection.ExecuteAsync($"DELETE FROM [AspNetRoles] WHERE [UserId] = @userId AND [RoleId] = @{nameof(roleId)}", new { userId = user.Id, roleId });
            }
        }

        public async Task<IList<string>> GetRolesAsync(ApplicationUserWidoutEntity user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var queryResults = await connection.QueryAsync<string>("SELECT r.[Name] FROM [AspNetRoles] r INNER JOIN [AspNetUserRoles] ur ON ur.[RoleId] = r.Id " +
                    "WHERE ur.UserId = @userId", new { userId = user.Id });

                return queryResults.ToList();
            }
        }

        public async Task<bool> IsInRoleAsync(ApplicationUserWidoutEntity user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                var roleId = await connection.ExecuteScalarAsync<string>("SELECT [Id] FROM [AspNetRoles] WHERE [NormalizedName] = @normalizedName", new { normalizedName = roleName.ToUpper() });
                if (string.IsNullOrEmpty(roleId)) return false;
                var matchingRoles = await connection.ExecuteScalarAsync<int>($"SELECT COUNT(*) FROM [AspNetUserRoles] WHERE [UserId] = @userId AND [RoleId] = @{nameof(roleId)}",
                    new { userId = user.Id, roleId });

                return matchingRoles > 0;
            }
        }

        public async Task<IList<ApplicationUserWidoutEntity>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                var queryResults = await connection.QueryAsync<ApplicationUserWidoutEntity>("SELECT u.* FROM [AspNetUsers] u " +
                    "INNER JOIN [AspNetUserRoles] ur ON ur.[UserId] = u.[Id] INNER JOIN [AspNetRoles] r ON r.[Id] = ur.[RoleId] WHERE r.[NormalizedName] = @normalizedName",
                    new { normalizedName = roleName.ToUpper() });

                return queryResults.ToList();
            }
        }

        public void Dispose()
        {
            // Nothing to dispose.
        }

        public async Task<IList<Claim>> GetClaimsAsync(ApplicationUserWidoutEntity user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);

                var queryResults = await connection.QueryAsync<ClaimViewModel>(" SELECT r.[Id], r.[UserId], r.[ClaimType], r.[ClaimValue]  FROM [AspNetUserClaims] r  " +
                  " WHERE r.[UserId] = @userId", new { userId = user.Id });

                return queryResults.Select(x => new Claim(x.ClaimType, x.ClaimValue)).ToList();
            }
        }

        public async Task AddClaimsAsync(ApplicationUserWidoutEntity user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                string query = string.Empty;
                foreach (var item in claims)
                {
                    var AspNetUserClaimsId = Guid.NewGuid().ToString();

                    var Id = Guid.NewGuid().ToString();


                    query = $@"  INSERT INTO [AspNetUserClaims] 
                                (   
                                   
                                   
                                    [UserId], 
                                    [ClaimType],
                                    [ClaimValue])
                                VALUES (
                                   
                                    
                                    @UserId,
                                    @ClaimType,
                                    @ClaimValue )
                                    ";
                    await connection.ExecuteAsync(query, new { UserId = user.Id, ClaimType = item.Type, ClaimValue = item.Value });
                }

            }
        }

        public Task ReplaceClaimAsync(ApplicationUserWidoutEntity user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveClaimsAsync(ApplicationUserWidoutEntity user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IList<ApplicationUserWidoutEntity>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {

            throw new NotImplementedException();


        }
    }
}
