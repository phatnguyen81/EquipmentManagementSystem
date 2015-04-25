using EquipmentManagementSystem.Framework.Mvc;

namespace EquipmentManagementSystem.Framework.Mvc
{
    public class DeleteConfirmationModel : BaseEmsEntityModel
    {
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string WindowId { get; set; }
    }
}