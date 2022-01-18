using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProjects.Core.Entities;

namespace TestProjects.Entity.Concrete
{
    public class Product : IEntitiy
    {
        public int Id { get; set; }
        public string  Name { get; set; }
        public int CategoryId { get; set; }
        public string Heigth { get; set; }
        public string Weigth { get; set; }
        public string Width { get; set; }
        public string Explanation { get; set; }
        public string AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
    }
}
