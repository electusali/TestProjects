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
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }
        public Category Add(Category category)
        {
            return _categoryDal.Add(category);
        }

        public async Task<Category> AddAsync(Category category)
        {
            return await _categoryDal.AddAsync(category);
        }

        public void Delete(Category category)
        {
            _categoryDal.Delete(category);
        }

        public Category GetByid(int id)
        {
            return _categoryDal.GetT(d=>d.Id == id);
        }

        public List<Category> GetList()
        {
            return _categoryDal.GetAll();
        }

        public Category Update(Category category)
        {
            return _categoryDal.Update(category);
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            return await _categoryDal.UpdateAsync(category);
        }
    }
}
