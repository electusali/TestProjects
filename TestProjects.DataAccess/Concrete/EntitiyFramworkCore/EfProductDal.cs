using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProjects.Core.DataAccess.EntitiyFramworkCore;
using TestProjects.DataAccess.Abstarct;
using TestProjects.Entity.ComplexTypes;
using TestProjects.Entity.Concrete;

namespace TestProjects.DataAccess.Concrete.EntitiyFramworkCore
{
    public class EfProductDal : EfEntityRepositoryBase<Product, TestProjectDbContext>, IProductDal
    {
        public List<ProductCategoryComplexData> GetProductWithCategory()
        {
            using (var _context=new TestProjectDbContext())
            {
                var result = from p in _context.Products
                             join c in _context.Categories on p.CategoryId equals c.Id
                             select new ProductCategoryComplexData
                             {
                                 CategoryName = c.Name,
                                 Heigth = p.Heigth,
                                 ProductId = p.Id,
                                 ProductName = p.Name,
                                 Weigth = p.Weigth,
                                 Width = p.Width
                             };
                return result.ToList();
            }
        }
    }
}
