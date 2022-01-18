using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProjects.Core.DataAccess.EntitiyFramworkCore;
using TestProjects.DataAccess.Abstarct;
using TestProjects.Entity.Concrete;

namespace TestProjects.DataAccess.Concrete.EntitiyFramworkCore
{
    public class EfCategoryDal :EfEntityRepositoryBase<Category,TestProjectDbContext> ,ICategoryDal
    {
    }
}
