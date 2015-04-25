using EquipmentManagementSystem.Core.Domain.Accounts;
using System.Data.Entity.ModelConfiguration;

namespace EquipmentManagementSystem.Data.Mapping.Customers
{
    public partial class AccountRoleMap : EntityTypeConfiguration<AccountRole>
    {
        public AccountRoleMap()
        {
            this.ToTable("AccountRole");
            this.HasKey(cr => cr.Id);
            this.Property(cr => cr.Name).IsRequired().HasMaxLength(255);
        }
    }
}