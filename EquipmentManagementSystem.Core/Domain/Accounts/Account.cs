using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EquipmentManagementSystem.Core.Domain.Accounts
{
    /// <summary>
    /// Represents a customer
    /// </summary>
    public partial class Account : BaseEntity
    {
        private ICollection<AccountRole> _accountRoles;

        /// <summary>
        /// Ctor
        /// </summary>
        public Account()
        {
            this.AccountGuid = Guid.NewGuid();
        }

        /// <summary>
        /// Gets or sets the customer Guid
        /// </summary>
        public Guid AccountGuid { get; set; }

        [Index(IsUnique = true)] 
        public string Username { get; set; }

        public string Fullname { get; set; }

        /// <summary>
        /// Gets or sets the email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets the password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the password salt
        /// </summary>
        public string PasswordSalt { get; set; }

      

        /// <summary>
        /// Gets or sets a value indicating whether the customer is active
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the customer has been deleted
        /// </summary>
        public bool Deleted { get; set; }


        /// <summary>
        /// Gets or sets the last IP address
        /// </summary>
        public string LastIpAddress { get; set; }

        /// <summary>
        /// Gets or sets the date and time of entity creation
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the date and time of last login
        /// </summary>
        public DateTime? LastLoginDateUtc { get; set; }

        /// <summary>
        /// Gets or sets the date and time of last activity
        /// </summary>
        public DateTime LastActivityDateUtc { get; set; }
        
        #region Navigation properties

        public virtual ICollection<AccountRole> AccountRoles
        {
            get { return _accountRoles ?? (_accountRoles = new List<AccountRole>()); }
            protected set { _accountRoles = value; }
        }

      
        #endregion
    }
}