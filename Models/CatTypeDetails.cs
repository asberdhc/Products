using System;
using System.Collections.Generic;

namespace Products.Models
{
    public partial class CatTypeDetails
    {
        public CatTypeDetails()
        {
            DetailProduct = new HashSet<DetailProduct>();
        }

        public int IdTypeDetail { get; set; }
        public int IdType { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public CatTypeProduct IdTypeNavigation { get; set; }
        public ICollection<DetailProduct> DetailProduct { get; set; }
    }
}
