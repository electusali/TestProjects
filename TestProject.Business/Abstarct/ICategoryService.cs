using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProjects.Entity.Concrete;

namespace TestProject.Business.Abstarct
{
    public interface ICategoryService
    {
        Category Add(Category category);
        Task<Category> AddAsync(Category category);
        Category Update(Category category);
        Task<Category> UpdateAsync(Category category);
        void Delete(Category category);
        Category GetByid(int id);
        List<Category> GetList();

    }
}
