using EquipmentManagementSystem.Core.Domain.Accounts;

namespace EquipmentManagementSystem.Services.Accounts
{
    public class AccountRegistrationRequest
    {
        public Account Account { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsApproved { get; set; }

        public AccountRegistrationRequest(Account account, string email, string username,
            string password, 
            bool isApproved = true)
        {
            this.Account = account;
            this.Email = email;
            this.Username = username;
            this.Password = password;
            this.IsApproved = isApproved;
        }
    }
}
