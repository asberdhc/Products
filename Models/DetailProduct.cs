using System;
using System.Collections.Generic;

namespace Products.Models
{
    public partial class DetailProduct
    {
        public int IdDetail { get; set; }
        public int IdProduct { get; set; }
        public int IdTypeDetail { get; set; }
        public string Description { get; set; }
        public string DateUpdate { get; set; }

        public Products IdProductNavigation { get; set; }
        public CatTypeDetails IdTypeDetailNavigation { get; set; }
    }
}
