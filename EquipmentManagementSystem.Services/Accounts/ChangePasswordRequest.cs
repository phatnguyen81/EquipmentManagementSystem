using EquipmentManagementSystem.Core.Domain.Accounts;

namespace EquipmentManagementSystem.Services.Accounts
{
    public class ChangePasswordRequest
    {
        public string Email { get; set; }
        public bool ValidateRequest { get; set; }
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }

        public ChangePasswordRequest(string email, bool validateRequest, string newPassword, string oldPassword = "")
        {
            this.Email = email;
            this.ValidateRequest = validateRequest;
            this.NewPassword = newPassword;
            this.OldPassword = oldPassword;
        }
    }
}
