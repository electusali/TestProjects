using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Business.Abstarct;
using TestProjects.DataAccess.Abstarct;
using TestProjects.Entity.ComplexTypes;
using TestProjects.Entity.Concrete;

namespace TestProject.Business.Concrete.Managers
{
    public class ProductManager : IProductServices
    {
        IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }
        public Product Add(Product product)
        {
           return _productDal.Add(product);
        }

        public Task<Product> AddAsync(Product product)
        {
            return _productDal.AddAsync(product);
        }

        public void Delete(Product product)
        {
             _productDal.Delete(product);
        }

        public Product GetById(int id)
        {
            return _productDal.GetT(d=>d.Id == id);
        }

        public Product GetByName(string name)
        {
            return _productDal.GetT(d=>d.Name==name);
        }

        public List<Product> GetList()
        {
            return _productDal.GetAll();
        }

        public List<Product> GetListCategoryId(int categoryid)
        {
            return _productDal.GetAll(d=>d.CategoryId==categoryid);
        }

        public Product GetProduct(string name)
        {
            return _productDal.GetT(d => d.Name == name);
        }

        public List<ProductCategoryComplexData> GetProductWithCategory()
        {
            return _productDal.GetProductWithCategory();
        }

        public Product Update(Product product)
        {
           return _productDal.Update(product);
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            return await _productDal.UpdateAsync(product);
        }
    }
}
