
namespace EquipmentManagementSystem.Core.Domain.Configuration
{
    /// <summary>
    /// Represents a setting
    /// </summary>
    public partial class Setting : BaseEntity
    {
        public Setting() { }
        
        public Setting(string name, string value) {
            this.Name = name;
            this.Value = value;
        }
        
        public string Name { get; set; }

        public string Value { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
