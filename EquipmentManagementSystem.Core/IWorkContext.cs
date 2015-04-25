using EquipmentManagementSystem.Core.Domain.Accounts;

namespace EquipmentManagementSystem.Core
{
    /// <summary>
    /// Work context
    /// </summary>
    public interface IWorkContext
    {
        /// <summary>
        /// Gets or sets the current account
        /// </summary>
        Account CurrentAccount { get; set; }
    }
}
