using System;
using System.Collections.Generic;

namespace Products.Models
{
    public partial class Products
    {
        public Products()
        {
            Comments = new HashSet<Comments>();
            DetailProduct = new HashSet<DetailProduct>();
            ImagesProduct = new HashSet<ImagesProduct>();
            Qualification = new HashSet<Qualification>();
            SimilarProduct = new HashSet<SimilarProduct>();
            SizeForProduct = new HashSet<SizeForProduct>();
        }

        public int Id { get; set; }
        public int? IdType { get; set; }
        public int? IdColor { get; set; }
        public int? IdBrand { get; set; }
        public int? IdProvider { get; set; }
        public int IdCatalog { get; set; }
        public string Title { get; set; }
        public string Nombre { get; set; }
        public string Description { get; set; }
        public string Observations { get; set; }
        public decimal? PriceDistributor { get; set; }
        public decimal PriceClient { get; set; }
        public decimal PriceMember { get; set; }
        public bool IsEnabled { get; set; }
        public string Keywords { get; set; }
        public DateTime DateUpdate { get; set; }

        public CatBrands IdBrandNavigation { get; set; }
        public CatCatalogs IdCatalogNavigation { get; set; }
        public CatColors IdColorNavigation { get; set; }
        public CatProviders IdProviderNavigation { get; set; }
        public CatTypeProduct IdTypeNavigation { get; set; }
        public ICollection<Comments> Comments { get; set; }
        public ICollection<DetailProduct> DetailProduct { get; set; }
        public ICollection<ImagesProduct> ImagesProduct { get; set; }
        public ICollection<Qualification> Qualification { get; set; }
        public ICollection<SimilarProduct> SimilarProduct { get; set; }
        public ICollection<SizeForProduct> SizeForProduct { get; set; }
    }
}
