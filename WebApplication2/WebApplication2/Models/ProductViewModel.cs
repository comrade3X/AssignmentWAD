using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class ProductViewModel
    {

        public IEnumerable<Product> Items { get; set; }
        public Pager Pager { get; set; }
        public virtual int CategoryId { get; set; }
        public virtual string CategoryName { get; set; }
        public virtual List<ProductImage> ListImages { get; set; }
        public virtual List<Product> RelatedProducts { get; set; }
        public virtual Product Prodcut { get; set; }

    }
}