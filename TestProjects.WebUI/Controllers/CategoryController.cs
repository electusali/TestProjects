using Microsoft.AspNetCore.Mvc;
using System;
using TestProject.Business.Abstarct;
using TestProjects.Entity.Concrete;
using TestProjects.WebUI.Models;

namespace TestProjects.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService=categoryService;
        }
        public IActionResult GetCategories()
        {
            var catrgoriViewModal = new CategoryViewModel
            {
                Categoies = _categoryService.GetList()
            };
            return View(catrgoriViewModal);
        }
        public IActionResult Add(CategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                var categoryadd = new Category
                {
                    AddedBy = "Ali",
                    AddedDate = DateTime.Now,
                    IsActive = false,
                    Name = categoryViewModel.Category.Name,
                };
                try
                {
                    _categoryService.Add(categoryadd);
                    return RedirectToAction("GetCategories");
                }
                catch (Exception)
                {
                }
            }
            return RedirectToAction("GetCategories");
        }
        public JsonResult Edit(int id)
        {
            if (id==0)
            {
                return Json(0);
            }
            var categroy = _categoryService.GetByid(id);
            if (categroy==null)
            {
                return Json(0);
            }
            return Json(categroy);
        }
        [HttpPost]
        public IActionResult Edit(CategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                var categorIsValid=_categoryService.GetByid(categoryViewModel.Category.Id);
                if (categorIsValid == null)
                {
                    return RedirectToAction("GetCategories");
                }
                try
                {
                    var categoryAdd = new Category
                    {
                        AddedBy = categorIsValid.AddedBy,
                        AddedDate = categorIsValid.AddedDate,
                        Id = categorIsValid.Id,
                        IsActive = categoryViewModel.Category.IsActive,
                        Name = categoryViewModel.Category.Name
                    };
                    _categoryService.Update(categoryAdd);
                    return RedirectToAction("GetCategories");
                }
                catch (Exception)
                {
                    return RedirectToAction("GetCategories");
                }
            }
            return RedirectToAction("GetCategories");
        }
        public JsonResult Delete(int id)
        {
            if (id==0)
            {
                return Json(0);
            }
            var categoryIsValite = _categoryService.GetByid(id);
            if (categoryIsValite == null)
            {
                return Json(0);
            }
            try
            {
                _categoryService.Delete(categoryIsValite);
                return Json(1);
            }
            catch (Exception)
            {
                return Json(0);
            }
        }
    }
}
