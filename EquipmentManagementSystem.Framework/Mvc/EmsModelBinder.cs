using System.Web.Mvc;

namespace EquipmentManagementSystem.Framework.Mvc
{
    public class EmsModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var model = base.BindModel(controllerContext, bindingContext);
            if (model is BaseEmsModel)
            {
                ((BaseEmsModel)model).BindModel(controllerContext, bindingContext);
            }
            return model;
        }
    }
}
