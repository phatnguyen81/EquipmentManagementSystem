using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EquipmentManagementSystem.Framework.Controllers;
using EquipmentManagementSystem.Framework.Datatables;
using EquipmentManagementSystem.Services.Catalog;
using EquipmentManagementSystem.Web.Extensions;
using EquipmentManagementSystem.Web.Models.Catalog;
using Nop.Web.Framework.Controllers;

namespace EquipmentManagementSystem.Web.Controllers
{
    [Authorize]
    public class CategoryController : BaseController
    {
        #region Fields
        
        private readonly ICategoryService _categoryService;

        #endregion

        #region Ctor
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        #endregion

        #region Actions
        // GET: Category
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            var model = new CategoryListModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult List(DataSourceRequest command, CategoryListModel model)
        {
            var categories = _categoryService.GetAllCategories(model.SearchCategoryName,
                command.Page, command.PageSize, true);
            var gridModel = new DataSourceResult
            {
                data = categories.Select(x =>
                {
                    var categoryModel = x.ToModel();
                    return categoryModel;
                }),
                recordsTotal = categories.TotalCount,
                recordsFiltered = categories.TotalCount
            };
            return Json(gridModel);
        }

        public ActionResult Create()
        {
            var model = new CategoryModel();

            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Create(CategoryModel model, bool continueEditing)
        {

            if (ModelState.IsValid)
            {
                var category = model.ToEntity();
                _categoryService.InsertCategory(category);
            
                SuccessNotification("Thêm loại sản phẩm thành công");
                return continueEditing ? RedirectToAction("Edit", new { id = category.Id }) : RedirectToAction("List");
            }

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var category = _categoryService.GetCategoryById(id);
            if (category == null || category.Deleted)
                //No category found with the specified id
                return RedirectToAction("List");

            var model = category.ToModel();

            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Edit(CategoryModel model, bool continueEditing)
        {

            var category = _categoryService.GetCategoryById(model.Id);
            if (category == null || category.Deleted)
                //No category found with the specified id
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                category = model.ToEntity(category);

                _categoryService.UpdateCategory(category);

                SuccessNotification("Thay đổi thông tin thành công");

                return continueEditing ? RedirectToAction("Edit", new { id = category.Id }) : RedirectToAction("List");
            }


            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var category = _categoryService.GetCategoryById(id);
            if (category == null)
                //No category found with the specified id
                return RedirectToAction("List");

            _categoryService.DeleteCategory(category);

            SuccessNotification("Xóa loại sản phẩm thành công");
            return RedirectToAction("List");
        }
        #endregion
    }
}