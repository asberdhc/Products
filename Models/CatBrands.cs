using System;
using System.Collections.Generic;

namespace Products.Models
{
    public partial class CatBrands
    {
        public CatBrands()
        {
            Products = new HashSet<Products>();
        }

        public int IdBrand { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime DateUpdate { get; set; }

        public ICollection<Products> Products { get; set; }
    }
}
