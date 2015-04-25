using EquipmentManagementSystem.Core.Domain.Accounts;
using System.Data.Entity.ModelConfiguration;
namespace EquipmentManagementSystem.Data.Mapping.Customers
{
    public partial class AccountMap : EntityTypeConfiguration<Account>
    {
        public AccountMap()
        {
            this.ToTable("Account");
            this.HasKey(c => c.Id);
            this.Property(u => u.Username).HasMaxLength(200);
            this.Property(u => u.Email).HasMaxLength(1000);
            this.Property(u => u.Fullname).HasMaxLength(1000);

            this.HasMany(c => c.AccountRoles)
                .WithMany()
                .Map(m => m.ToTable("Account_AccountRole_Mapping"));

        }
    }
}