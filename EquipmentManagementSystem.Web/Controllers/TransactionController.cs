using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EquipmentManagementSystem.Framework.Controllers;
using EquipmentManagementSystem.Services.Catalog;
using EquipmentManagementSystem.Services.Warehouses;
using EquipmentManagementSystem.Web.Extensions;
using EquipmentManagementSystem.Web.Models.Transactions;

namespace EquipmentManagementSystem.Web.Controllers
{
    public class TransactionController : BaseController
    {
        #region Fields

        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IWarehouseProfileService _warehouseProfileService;
       
        #endregion

        #region Ctor
        public TransactionController(IWarehouseProfileService warehouseProfileService, ICategoryService categoryService, IProductService productService)
        {
            _warehouseProfileService = warehouseProfileService;
            _categoryService = categoryService;
            _productService = productService;
        }

        #endregion

        #region Utilities

        [NonAction]
        protected virtual void PrepareImportModel(ImportModel model)
        {
            model.AvailableWarehouseProfiles =
                _warehouseProfileService.GetAllWarehouseProfiles().Select(q => new SelectListItem
                {
                    Text = q.Name,
                    Value = q.Id.ToString()
                }).ToList();
            model.Categories = _categoryService.GetAllCategories().Select(q=>q.ToModel()).ToList();
            model.Categories.ForEach(q =>
            {
                q.Products = _productService.SearchProducts(categoryId: q.Id).Select(p => p.ToModel()).ToList();
            });
        }

        #endregion

        #region Actions
        // GET: Transaction
        public ActionResult Import()
        {
            var model = new ImportModel();
            PrepareImportModel(model);
            return View(model);
        }

        
        #endregion

        #region Ajax
        [HttpPost]
        public JsonResult  _AjaxAddImport(ImportModel model)
        {
            return Json(new
            {
                ErrCode = 0
            });
        }

        #endregion
    }
}
