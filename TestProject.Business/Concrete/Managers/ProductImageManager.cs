using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Business.Abstarct;
using TestProjects.DataAccess.Abstarct;
using TestProjects.Entity.Concrete;

namespace TestProject.Business.Concrete.Managers
{
    public class ProductImageManager : IProductImageService
    {
        IProductImageDal _productImageDal;
        public ProductImageManager(IProductImageDal productImageDal)
        {
            _productImageDal = productImageDal;
        }
        public ProductImage Add(ProductImage productImage)
        {
            return _productImageDal.Add(productImage);
        }

        public void Delete(ProductImage productImage)
        {
            _productImageDal.Delete(productImage);
        }

        public ProductImage GetById(int id)
        {
            return _productImageDal.GetT(d => d.Id == id);
        }

        public List<ProductImage> GetList()
        {
            return _productImageDal.GetAll();
        }

        public List<ProductImage> GetListProductId(int id)
        {
            return _productImageDal.GetAll(d=>d.ProductId == id);
        }

        public ProductImage Update(ProductImage productImage)
        {
            return _productImageDal.Update(productImage);
        }
    }
}
