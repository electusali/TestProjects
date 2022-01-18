using System.Collections.Generic;
using TestProjects.Entity.Concrete;

namespace TestProjects.WebUI.Models
{
    public class CategoryViewModel
    {
        public List<Category> Categoies { get; set; }
        public Category Category { get; set; }
    }
}
