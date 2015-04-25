using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EquipmentManagementSystem.Core.Domain.Transactions;

namespace EquipmentManagementSystem.Data.Mapping.Transactions
{
    public class TransactionDetailMap : EntityTypeConfiguration<TransactionDetail>
    {
        public TransactionDetailMap()
        {
            this.ToTable("TransactionDetail");
            this.HasKey(s => s.Id);

            this.HasRequired(td => td.Transaction)
                .WithMany(t => t.TransactionDetails)
                .HasForeignKey(td => td.TransactionId);
        }
    }
}
