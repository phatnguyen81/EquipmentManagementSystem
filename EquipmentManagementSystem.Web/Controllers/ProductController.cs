using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EquipmentManagementSystem.Core.Domain.Catalog;
using EquipmentManagementSystem.Framework.Controllers;
using EquipmentManagementSystem.Framework.Datatables;
using EquipmentManagementSystem.Services.Catalog;
using EquipmentManagementSystem.Web.Extensions;
using EquipmentManagementSystem.Web.Models.Catalog;
using Nop.Web.Framework.Controllers;

namespace EquipmentManagementSystem.Web.Controllers
{
    [Authorize]
    public class ProductController : BaseController
    {
        #region Fields

        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        #endregion

        #region Ctor
        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        #endregion

        #region Utilities

        [NonAction]
        protected virtual void PrepareProductModel(ProductModel model, Product product)
        {
            if (model == null)
                throw new ArgumentNullException("model");
            var allCategories = _categoryService.GetAllCategories(showHidden: true);
            foreach (var category in allCategories)
            {
                model.AvailableCategories.Add(new SelectListItem
                {
                    Text = category.Name,
                    Value = category.Id.ToString()
                });
            }
        }

        protected virtual void PrepareProductListModel(ProductListModel model)
        {
            var allCategories = _categoryService.GetAllCategories(showHidden: true);
            foreach (var category in allCategories)
            {
                model.AvailableCategories.Add(new SelectListItem
                {
                    Text = category.Name,
                    Value = category.Id.ToString()
                });
            }
        }
        protected virtual ProductModel PrepareProductModelForList(Product product)
        {
            return new ProductModel
            {
                Id = product.Id,
                Name = product.Name,
                Sku = product.Sku,
                CategoryName = product.Category.Name
            };
        }
        #endregion 

        #region Actions
        // GET: Product
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            var model = new ProductListModel();
            PrepareProductListModel(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult List(DataSourceRequest command, ProductListModel model)
        {
            var products = _productService.SearchProducts(model.SearchKeyword, model.SearchCategoryId,
                command.Page, command.PageSize, true, true);
            var gridModel = new DataSourceResult
            {
                data = products.Select(x =>
                {
                    var productModel = PrepareProductModelForList(x);
                    return productModel;
                }),
                recordsTotal = products.TotalCount,
                recordsFiltered = products.TotalCount
            };
            return Json(gridModel);
        }

        public ActionResult Create()
        {
            var model = new ProductModel();
            PrepareProductModel(model, null);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Create(ProductModel model, bool continueEditing)
        {

            if (ModelState.IsValid)
            {
                var product = model.ToEntity();
                _productService.InsertProduct(product);

                SuccessNotification("Thêm loại sản phẩm thành công");
                return continueEditing ? RedirectToAction("Edit", new { id = product.Id }) : RedirectToAction("List");
            }
            PrepareProductModel(model, null);
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null || product.Deleted)
                //No product found with the specified id
                return RedirectToAction("List");

            var model = product.ToModel();
            PrepareProductModel(model, null);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Edit(ProductModel model, bool continueEditing)
        {

            var product = _productService.GetProductById(model.Id);
            if (product == null || product.Deleted)
                //No product found with the specified id
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                product = model.ToEntity(product);

                _productService.UpdateProduct(product);

                SuccessNotification("Thay đổi thông tin thành công");

                return continueEditing ? RedirectToAction("Edit", new { id = product.Id }) : RedirectToAction("List");
            }

            PrepareProductModel(model, null);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
                //No product found with the specified id
                return RedirectToAction("List");

            _productService.DeleteProduct(product);

            SuccessNotification("Xóa loại sản phẩm thành công");
            return RedirectToAction("List");
        }
        #endregion
    }
}