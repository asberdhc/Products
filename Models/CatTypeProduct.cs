using System;
using System.Collections.Generic;

namespace Products.Models
{
    public partial class CatTypeProduct
    {
        public CatTypeProduct()
        {
            CatSizes = new HashSet<CatSizes>();
            CatTypeDetails = new HashSet<CatTypeDetails>();
            Products = new HashSet<Products>();
        }

        public int IdType { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime DateUpdate { get; set; }
        public bool IsEnabled { get; set; }

        public ICollection<CatSizes> CatSizes { get; set; }
        public ICollection<CatTypeDetails> CatTypeDetails { get; set; }
        public ICollection<Products> Products { get; set; }
    }
}
