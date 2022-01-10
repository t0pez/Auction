using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AuctionBLL.Dto;

namespace AuctionBLL.Interfaces
{
    /// <summary>
    /// Service for work with users
    /// </summary>
    public interface IUsersService : IDisposable
    {
        /// <summary>
        /// Gets all users
        /// </summary>
        /// <returns>Mapped users collection</returns>
        Task<IEnumerable<UserDto>> GetAllAsync();
        /// <summary>
        /// Gets user by id
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>Mapped user</returns>
        /// <exception cref="InvalidOperationException">When user not found</exception>
        Task<UserDto> GetByUserIdAsync(string userId);
        /// <summary>
        /// Gets all user roles
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>Role (as a string) collection</returns>
        Task<IEnumerable<string>> GetUserRolesAsync(string userId);
        /// <summary>
        /// Creates user
        /// </summary>
        /// <param name="newUser">User creation model</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">When user already exists</exception>
        Task CreateAsync(UserDto newUser);
        /// <summary>
        /// Adds new money to users wallet
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="money">Money</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">When user not found</exception>
        /// <exception cref="ArgumentException">When money of same currency already exists</exception>
        Task CreateUserMoneyAsync(string userId, MoneyDto money);
        /// <summary>
        /// Get user claims
        /// </summary>
        /// <param name="user">User login model</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">When user not found</exception>
        Task<ClaimsIdentity> LoginAsync(UserDto user);
        /// <summary>
        /// Adds user to role
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="role">Role</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">When user not found</exception>
        Task AddRoleAsync(string userId, string role);
        /// <summary>
        /// Removes user from role
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="role">Role</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">When user not found</exception>
        Task RemoveRoleAsync(string userId, string role);
        /// <summary>
        /// Adds money to user money
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="moneyId">Money id</param>
        /// <param name="addedAmount">Amount to add</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">When amount to add is not correct</exception>
        /// <exception cref="InvalidOperationException">When user not found</exception>
        Task<UserDto> TopUpUserBalanceAsync(string userId, Guid moneyId, decimal addedAmount);
    }
}
