using System;
using System.Collections.Generic;

namespace Products.Models
{
    public partial class CatCatalogs
    {
        public CatCatalogs()
        {
            Products = new HashSet<Products>();
        }

        public int IdCatalog { get; set; }
        public int IdProvider { get; set; }
        public string Season { get; set; }
        public string StarActiveDate { get; set; }
        public string EndActiveDate { get; set; }
        public string DateUpdate { get; set; }
        public string IsEnabled { get; set; }

        public CatProviders IdProviderNavigation { get; set; }
        public ICollection<Products> Products { get; set; }
    }
}
