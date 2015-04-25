using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EquipmentManagementSystem.Core.Domain.Transactions;

namespace EquipmentManagementSystem.Data.Mapping.Transactions
{
    public class TransactionTypeMap : EntityTypeConfiguration<TransactionType>
    {
        public TransactionTypeMap()
        {
            this.ToTable("TransactionType");
            this.HasKey(s => s.Id);
            this.Property(s => s.Code).IsRequired().HasMaxLength(20);
            this.Property(s => s.Name).IsRequired().HasMaxLength(1000);
        }
    }
}
