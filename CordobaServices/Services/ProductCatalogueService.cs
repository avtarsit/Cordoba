using CordobaModels.Entities;
using CordobaServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Services
{
    public class ProductCatalogueService : IProductCatalogueServices
    {
        public List<ProductCatalogueEntity> GetProductCatalogueList()
        {
            List<ProductCatalogueEntity> ProductCatalogue = new List<ProductCatalogueEntity>();
            ProductCatalogue.Add(new ProductCatalogueEntity() { ProductCatalogueId = 1, ProductCatalogueName = "CVD Edenred Vouchers" });
            ProductCatalogue.Add(new ProductCatalogueEntity() { ProductCatalogueId = 2, ProductCatalogueName = "Edenred" });
            ProductCatalogue.Add(new ProductCatalogueEntity() { ProductCatalogueId = 3, ProductCatalogueName = "Miscellaneous Suppliers" });
            ProductCatalogue.Add(new ProductCatalogueEntity() { ProductCatalogueId = 4, ProductCatalogueName = "Xmas Hampers" });
            ProductCatalogue.Add(new ProductCatalogueEntity() { ProductCatalogueId = 5, ProductCatalogueName = "SSG Procurement UK (Without Nursery or Apple)" });
            ProductCatalogue.Add(new ProductCatalogueEntity() { ProductCatalogueId = 6, ProductCatalogueName = "PB Jigsaw24 Multilingual (Apple)" });
            ProductCatalogue.Add(new ProductCatalogueEntity() { ProductCatalogueId = 7, ProductCatalogueName = "AV Promotions (Kindle)" });
            ProductCatalogue.Add(new ProductCatalogueEntity() { ProductCatalogueId = 8, ProductCatalogueName = "Procurement (Australia)" });
            ProductCatalogue.Add(new ProductCatalogueEntity() { ProductCatalogueId = 9, ProductCatalogueName = "Procurement (India)" });
            ProductCatalogue.Add(new ProductCatalogueEntity() { ProductCatalogueId = 10, ProductCatalogueName = "Procurement (Canada)" });
            ProductCatalogue.Add(new ProductCatalogueEntity() { ProductCatalogueId = 11, ProductCatalogueName = "Somcan (Canada)" });
            ProductCatalogue.Add(new ProductCatalogueEntity() { ProductCatalogueId = 12, ProductCatalogueName = "Spicers Hampers" });
            ProductCatalogue.Add(new ProductCatalogueEntity() { ProductCatalogueId = 13, ProductCatalogueName = "Harco (United States)" });
            ProductCatalogue.Add(new ProductCatalogueEntity() { ProductCatalogueId = 14, ProductCatalogueName = "Edenred (India)" });
            ProductCatalogue.Add(new ProductCatalogueEntity() { ProductCatalogueId = 15, ProductCatalogueName = "Edenred (India)" });
            return ProductCatalogue;
        }

        public ProductCatalogueEntity GetProductCatalogueById(int ProductCatalogueId = 0)
        {
            ProductCatalogueEntity ProductCatalogueEntity = new ProductCatalogueEntity();
            if (ProductCatalogueId > 0)
            {
                ProductCatalogueEntity = (from t in GetProductCatalogueList()
                                  where t.ProductCatalogueId == ProductCatalogueId
                                  select t).FirstOrDefault();
            }
            else
            {
                ProductCatalogueEntity = new ProductCatalogueEntity();
            }
            return ProductCatalogueEntity;

        }
    }
}
