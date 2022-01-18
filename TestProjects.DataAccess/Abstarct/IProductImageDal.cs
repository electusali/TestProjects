using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProjects.Core.DataAccess;
using TestProjects.Entity.Concrete;

namespace TestProjects.DataAccess.Abstarct
{
    public interface IProductImageDal:IEntitiyRepository<ProductImage>
    {

    }
}
