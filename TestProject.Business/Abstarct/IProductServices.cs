using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProjects.Entity.ComplexTypes;
using TestProjects.Entity.Concrete;

namespace TestProject.Business.Abstarct
{
    public interface IProductServices
    {
        Product Add (Product product);
        Task<Product> AddAsync (Product product);
        Product Update (Product product);
        Task<Product> UpdateAsync(Product product);
        void Delete (Product product);
        Product GetByName (string name);
        Product GetById(int id);
        List<Product> GetList ();
        List<Product> GetListCategoryId(int categoryid);
        Product GetProduct(string name);
        List<ProductCategoryComplexData> GetProductWithCategory();
    }
}
