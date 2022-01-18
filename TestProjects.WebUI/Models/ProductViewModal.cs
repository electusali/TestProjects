using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using TestProjects.Entity.ComplexTypes;
using TestProjects.Entity.Concrete;

namespace TestProjects.WebUI.Models
{
    public class ProductViewModal
    {
        public Product product { get; set; }
        public List<ProductCategoryComplexData> Products { get; set; }
        public List<SelectListItem> categories { get; set; }
        public List<IFormFile> FormFiles { get; set; }
        public List<ProductImage> ProductImages { get; set; }
    }
}
