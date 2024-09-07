using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.DataTransferObject.Admin
{
    public class CategoryModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool isActive { get; set; }
        public ICollection<ProductModel> ProductNavigation { get; set; }
    }
}
