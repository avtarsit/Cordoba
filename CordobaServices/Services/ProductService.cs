using CordobaModels.Entities;
using CordobaServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Services
{
    public class ProductService : IProductServices
    {
        public List<ProductEntity> GetProductList()
        {
            List<ProductEntity> Product = new List<ProductEntity>();

            Product.Add(new ProductEntity() { product_id = 1, name = "GIORGIO ARMANI AR6011 300371 SUNGLASSES MATTE GUNMETAL SIZE 66", StatusId = 1, StatusName = "Enabled", Model = "LAR-002", Quantity = 1, Price = 2323,ImagePath=@"../../../Content/admin/images/Product/BU-026-300-40x40.jpg" } );
            Product.Add(new ProductEntity() { product_id = 2, name = "KINDLE 80250901 FIRE HDX 8.9 WIFI 16GB BLACK", StatusId = 1, StatusName = "Enabled", Model = "ZW-1344", Quantity = 2, Price = 2323, ImagePath =@"../../../Content/admin/images/Product/DP-059-300-40x40.jpg" });
            Product.Add(new ProductEntity() { product_id = 3, name = "ROBERTS ECO PLAY DAB FM RDS DIGITAL RADIO BLACK", StatusId = 1, StatusName = "Enabled", Model = "RR-026", Quantity = 3, Price = 2323, ImagePath = @"../../../Content/admin/images/Product/DP-059-300-40x40.jpg" });
            Product.Add(new ProductEntity() { product_id = 4, name = "DRAPER 10347 STAINLESS STEEL FORK & SPADE WITH TROWEL & WEEDER SET", StatusId = 1, StatusName = "Enabled", Model = "DP-059", Quantity = 3, Price = 2323, ImagePath = @"../../../Content/admin/images/Product/DT-050-300-40x40.jpg" });
            Product.Add(new ProductEntity() { product_id = 5, name = "BUSHNELL 242410 10X42 EXCURSION HD BINOCULARS", StatusId = 1, StatusName = "Enabled", Model = "BU-026", Quantity = 4, Price = 232453, ImagePath = @"../../../Content/admin/images/Product/LAR-002-300-40x40.jpg" });
            Product.Add(new ProductEntity() { product_id = 6, name = "TAG HEUER WAT1110.BA0950 LINK GENTS WATCH", StatusId = 1, StatusName = "Enabled", Model = "TG-235", Quantity = 2, Price = 23234, ImagePath = @"../../../Content/admin/images/Product/RR-026-300-40x40.jpg" });
            Product.Add(new ProductEntity() { product_id = 7, name = "TAG HEUER WAR211A.BA0782 CARRERA GENTS WATCH", StatusId = 1, StatusName = "Enabled", Model = "TG-233", Quantity = 2, Price = 23233, ImagePath = @"../../../Content/admin/images/Product/RT-011-300-40x40.jpg" });
            Product.Add(new ProductEntity() { product_id = 8, name = "TAG HEUER WAY1413.BA0920 AQUARACER LADIES WATCH", StatusId = 1, StatusName = "Enabled", Model = "TG-230", Quantity = 2, Price = 23233, ImagePath = @"../../../Content/admin/images/Product/RT-013-300-40x40.jpg" });
            Product.Add(new ProductEntity() { product_id = 9, name = "TAG HEUER CAU1113.BA0858 FORMULA 1 GENTS INDY 500 WATCH", StatusId = 1, StatusName = "Enabled", Model = "TG-229", Quantity = 1, Price = 233, ImagePath = @"../../../Content/admin/images/Product/RT-015-300-40x40.jpg" });
            Product.Add(new ProductEntity() { product_id = 10, name = "TAG HEUER CAW2111.FC6183 MONACO GENTS CHRONOGRAPH WATCH", StatusId = 1, StatusName = "Enabled", Model = "TG-226", Quantity = 2, Price = 23343, ImagePath = @"../../../Content/admin/images/Product/RT-026-300-40x40.jpg" });
            Product.Add(new ProductEntity() { product_id = 11, name = "DARTINGTON ES724 ESSENTIALS WINE CARAFE", StatusId = 1, StatusName = "Enabled", Model = "TG-222", Quantity = 200, Price = 676, ImagePath = @"../../../Content/admin/images/Product/TG-222-300-40x40.jpg" });
            Product.Add(new ProductEntity() { product_id = 12, name = "RESTMOR SUPREME COBALT BLUE 500GPSM TOWEL BALE", StatusId = 1, StatusName = "Enabled", Model = "DT-050", Quantity = 2, Price = 676, ImagePath = @"../../../Content/admin/images/Product/TG-230-300-40x40.jpg" });
            Product.Add(new ProductEntity() { product_id = 13, name = "RESTMOR SUPREME SEAFOAM 500GPSM TOWEL BALE", StatusId = 1, StatusName = "Enabled", Model = "RT-026", Quantity = 2, Price = 6666, ImagePath = @"../../../Content/admin/images/Product/TG-235-300-40x40.jpg" });
            Product.Add(new ProductEntity() { product_id = 14, name = "RESTMOR SUPREME PARSLEY 500GPSM TOWEL BALE", StatusId = 1, StatusName = "Enabled", Model = "RT-019", Quantity = 2, Price = 2366, ImagePath = @"../../../Content/admin/images/Product/ZW-1344-300-40x40.jpg" });
            Product.Add(new ProductEntity() { product_id = 15, name = "RESTMOR SUPREME MAUVE 500GPSM TOWEL BALE", StatusId = 1, StatusName = "Enabled", Model = "RT-015", Quantity = 2, Price = 23236, ImagePath = @"../../../Content/admin/images/Product/ZW-146-300-40x40.jpg" });

            return Product;
        }

        public ProductEntity GetProductById(int product_id)
        {
            ProductEntity ProductEntity = new ProductEntity();
            List<ProductDescriptionList> productDescriptionList = new List<ProductDescriptionList>();
            if (product_id > 0)
            {
                ProductEntity = (from t in GetProductList()
                                where t.product_id == product_id
                                select t).FirstOrDefault();
                ProductEntity.ProductDescriptionList = productDescriptionList;
            }
            else
            {
                ProductEntity = new ProductEntity();
                ProductEntity.ProductDescriptionList = productDescriptionList;
                ProductEntity.StatusId = 1;
            }
            return ProductEntity;
        }


    }
}
