using System.Collections.Generic;

namespace EquipmentManagementSystem.Core.Domain.Accounts
{
    /// <summary>
    /// Represents a customer role
    /// </summary>
    public partial class AccountRole : BaseEntity
    {
        /// <summary>
        /// Gets or sets the customer role name
        /// </summary>
        public string Name { get; set; }

      

        /// <summary>
        /// Gets or sets a value indicating whether the customer role is active
        /// </summary>
        public bool Active { get; set; }

     
    }

}