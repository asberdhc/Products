using System;
using System.Collections.Generic;

namespace Products.Models
{
    public partial class CatSizes
    {
        public CatSizes()
        {
            SizeForProduct = new HashSet<SizeForProduct>();
        }

        public int IdSize { get; set; }
        public int IdType { get; set; }
        public string Code { get; set; }
        public string Unity { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }

        public CatTypeProduct IdTypeNavigation { get; set; }
        public ICollection<SizeForProduct> SizeForProduct { get; set; }
    }
}
