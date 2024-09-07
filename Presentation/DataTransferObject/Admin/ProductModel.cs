using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.DataTransferObject.Admin
{
    public class ProductModel
    {
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public string Description { get; set; }
        public bool isActive { get; set; }
        public string ImageUrl { get; set; }
        public CategoryModel CategoryNavigation { get; set; }
    }
}
