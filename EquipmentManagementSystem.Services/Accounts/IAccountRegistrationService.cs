using EquipmentManagementSystem.Core.Domain.Accounts;

namespace EquipmentManagementSystem.Services.Accounts
{
    /// <summary>
    /// Account registration interface
    /// </summary>
    public partial interface IAccountRegistrationService
    {
        /// <summary>
        /// Validate customer
        /// </summary>
        /// <param name="usernameOrEmail">Username or email</param>
        /// <param name="password">Password</param>
        /// <returns>Result</returns>
        AccountLoginResults ValidateAccount(string usernameOrEmail, string password);

        /// <summary>
        /// Register customer
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Result</returns>
        AccountRegistrationResult RegisterAccount(AccountRegistrationRequest request);

        
    }
}