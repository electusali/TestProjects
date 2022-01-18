using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TestProject.Business.Abstarct;
using TestProjects.Entity.Concrete;
using TestProjects.WebUI.Models;

namespace TestProjects.WebUI.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        IProductServices _productServices;
        ICategoryService _categoryService;
        IProductImageService _productImageService;
        IHostingEnvironment _env;
        public ProductsController(IProductServices productServices,ICategoryService categoryService,
            IProductImageService productImageService,IHostingEnvironment env)
        {
            _productServices = productServices;
            _categoryService = categoryService;
            _productImageService = productImageService;
            _env = env;
        }
        public IActionResult GetProducts()
        {
            var productViewmodal = new ProductViewModal
            {
                Products = _productServices.GetProductWithCategory(),
                categories=LoadCategories()
            };
            return View(productViewmodal);
        }
        public IActionResult GetProductDetail(int id)
        {
            if (id>0)
            {
                var productIsValid=_productServices.GetById(id);
                var productImages=_productImageService.GetListProductId(id);
                var productViewModal = new ProductViewModal
                {
                    product = productIsValid,
                    ProductImages = productImages,
                    categories = LoadCategories()
                };
                return View(productViewModal);
            }
            return RedirectToAction("GetProducts");

        }
        private List<SelectListItem> LoadCategories()
        {
            List<SelectListItem> categories = (from category in _categoryService.GetList()

                 select new SelectListItem
                 {
                     Value = category.Id.ToString(),
                     Text=category.Name
                 }
            ).ToList();
            return categories;
        }
        public IActionResult Add(ProductViewModal productViewModal)
        {
            if (ModelState.IsValid)
            {
                var productsIsvalid = _productServices.GetByName(productViewModal.product.Name);
                if (productsIsvalid!=null)
                {
                    return RedirectToAction("GetProducts");
                }
                var productForAdd = new Product
                {
                    AddedDate = DateTime.Now,
                    AddedBy = "Ali Boran",
                    CategoryId = productViewModal.product.CategoryId,
                    Explanation = productViewModal.product.Explanation,
                    Heigth = productViewModal.product.Heigth,
                    Name = productViewModal.product.Name,
                    Weigth = productViewModal.product.Weigth,
                    Width = productViewModal.product.Width
                };
                try
                {
                    var addedProduct= _productServices.Add(productForAdd);
                    if (productViewModal.FormFiles!=null)
                    {
                        foreach (var image in productViewModal.FormFiles)
                        {
                            var uniqFileName=Guid.NewGuid().ToString()+"+_"+image.FileName;
                            var filePath = Path.DirectorySeparatorChar.ToString() + "ProductImages" + Path.DirectorySeparatorChar.ToString() + uniqFileName;
                            string uploadFolder = Path.Combine(_env.WebRootPath, "ProductImages");
                            var filePathForCopy=Path.Combine(uploadFolder, uniqFileName);
                            image.CopyTo(new FileStream(filePathForCopy, FileMode.Create));

                            var productImageForAdd = new ProductImage
                            {
                                AddedBy = "Ali Boran",
                                AddedDate = DateTime.Now,
                                ProductId = addedProduct.Id,
                                FileName = uniqFileName,
                                FilePath = filePath
                            };
                            _productImageService.Add(productImageForAdd);
                        }
                    }
                    return RedirectToAction("GetProducts");
                }
                catch (Exception)
                {

                    return RedirectToAction("GetProducts");
                }
            }
            return RedirectToAction("GetProducts");
        }
        public JsonResult Edit(int id)
        {
            if (id>0)
            {
                var result=_productServices.GetById(id);
                return Json(result);
            }
            return Json(0);
        }
        [HttpPost]
        public IActionResult Edit(ProductViewModal productViewModal)
        {
            if (ModelState.IsValid)
            {
                var productIsValid = _productServices.GetById(productViewModal.product.Id);
                if (productIsValid == null)
                {
                    return RedirectToAction("GetProducts");
                }
                var productForAdd = new Product
                {
                    AddedDate = productIsValid.AddedDate,
                    AddedBy = productIsValid.AddedBy,
                    CategoryId = productViewModal.product.CategoryId,
                    Explanation = productViewModal.product.Explanation,
                    Heigth = productViewModal.product.Heigth,
                    Name = productViewModal.product.Name,
                    Weigth = productViewModal.product.Weigth,
                    Width = productViewModal.product.Width,
                    Id=productIsValid.Id
                };
                try
                {
                    _productServices.Update(productForAdd);
                    return RedirectToAction("GetProducts");
                }
                catch (Exception)
                {
                    return RedirectToAction("GetProducts");
                }
            }
            return RedirectToAction("GetProducts");
        }
        public JsonResult Delete(int id)
        {
            if (id>0)
            {
                var productIsvalid= _productServices.GetById(id);
                if (productIsvalid==null)
                {
                    return Json(0);
                }
                _productServices.Delete(productIsvalid);
                return Json(1);
            }
            return Json(0);
        }
    }
}
