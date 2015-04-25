using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EquipmentManagementSystem.Core.Domain.Transactions;

namespace EquipmentManagementSystem.Data.Mapping.Transactions
{
    public class TransactionMap : EntityTypeConfiguration<Transaction>
    {
        public TransactionMap()
        {
            this.ToTable("Transaction");
            this.HasKey(s => s.Id);
            this.Property(s => s.Code).IsRequired().HasMaxLength(20);
            this.Property(s => s.Note).HasMaxLength(2000);

            this.HasMany(t => t.TransactionDetails)
                .WithRequired(td => td.Transaction)
                .HasForeignKey(td => td.TransactionId);
        }
    }
}
