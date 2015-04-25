using EquipmentManagementSystem.Core.Domain.Accounts;

namespace EquipmentManagementSystem.Services.Authentication
{
    /// <summary>
    /// Authentication service interface
    /// </summary>
    public partial interface IAuthenticationService 
    {
        void SignIn(Account account, bool createPersistentCookie);
        void SignOut();
        Account GetAuthenticatedAccount();
    }
}