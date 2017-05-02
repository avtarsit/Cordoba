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
            Product.Add(new ProductEntity() { ProductId = 1, ProductName = "GIORGIO ARMANI AR6011 300371 SUNGLASSES MATTE GUNMETAL SIZE 66", StatusId = 1, StatusName = "Enabled", Model = "LAR-002", Quantity = 1, Price = 2323,ImagePath=@"../../../Content/admin/images/Product/BU-026-300-40x40.jpg" });
            Product.Add(new ProductEntity() { ProductId = 2, ProductName = "KINDLE 80250901 FIRE HDX 8.9 WIFI 16GB BLACK", StatusId = 1, StatusName = "Enabled", Model = "ZW-1344", Quantity = 2, Price = 2323, ImagePath =@"../../../Content/admin/images/Product/DP-059-300-40x40.jpg" });
            Product.Add(new ProductEntity() { ProductId = 3, ProductName = "ROBERTS ECO PLAY DAB FM RDS DIGITAL RADIO BLACK", StatusId = 1, StatusName = "Enabled", Model = "RR-026", Quantity = 3, Price = 2323, ImagePath = @"../../../Content/admin/images/Product/DP-059-300-40x40.jpg" });
            Product.Add(new ProductEntity() { ProductId = 4, ProductName = "DRAPER 10347 STAINLESS STEEL FORK & SPADE WITH TROWEL & WEEDER SET", StatusId = 1, StatusName = "Enabled", Model = "DP-059", Quantity = 3, Price = 2323, ImagePath = @"../../../Content/admin/images/Product/DT-050-300-40x40.jpg" });
            Product.Add(new ProductEntity() { ProductId = 5, ProductName = "BUSHNELL 242410 10X42 EXCURSION HD BINOCULARS", StatusId = 1, StatusName = "Enabled", Model = "BU-026", Quantity = 4, Price = 232453, ImagePath = @"../../../Content/admin/images/Product/LAR-002-300-40x40.jpg" });
            Product.Add(new ProductEntity() { ProductId = 6, ProductName = "TAG HEUER WAT1110.BA0950 LINK GENTS WATCH", StatusId = 1, StatusName = "Enabled", Model = "TG-235", Quantity = 2, Price = 23234, ImagePath = @"../../../Content/admin/images/Product/RR-026-300-40x40.jpg" });
            Product.Add(new ProductEntity() { ProductId = 7, ProductName = "TAG HEUER WAR211A.BA0782 CARRERA GENTS WATCH", StatusId = 1, StatusName = "Enabled", Model = "TG-233", Quantity = 2, Price = 23233, ImagePath = @"../../../Content/admin/images/Product/RT-011-300-40x40.jpg" });
            Product.Add(new ProductEntity() { ProductId = 8, ProductName = "TAG HEUER WAY1413.BA0920 AQUARACER LADIES WATCH", StatusId = 1, StatusName = "Enabled", Model = "TG-230", Quantity = 2, Price = 23233, ImagePath = @"../../../Content/admin/images/Product/RT-013-300-40x40.jpg" });
            Product.Add(new ProductEntity() { ProductId = 9, ProductName = "TAG HEUER CAU1113.BA0858 FORMULA 1 GENTS INDY 500 WATCH", StatusId = 1, StatusName = "Enabled", Model = "TG-229", Quantity = 1, Price = 233, ImagePath = @"../../../Content/admin/images/Product/RT-015-300-40x40.jpg" });
            Product.Add(new ProductEntity() { ProductId = 10, ProductName = "TAG HEUER CAW2111.FC6183 MONACO GENTS CHRONOGRAPH WATCH", StatusId = 1, StatusName = "Enabled", Model = "TG-226", Quantity = 2, Price = 23343, ImagePath = @"../../../Content/admin/images/Product/RT-026-300-40x40.jpg" });
            Product.Add(new ProductEntity() { ProductId = 11, ProductName = "DARTINGTON ES724 ESSENTIALS WINE CARAFE", StatusId = 1, StatusName = "Enabled", Model = "TG-222", Quantity = 200, Price = 676, ImagePath = @"../../../Content/admin/images/Product/TG-222-300-40x40.jpg" });
            Product.Add(new ProductEntity() { ProductId = 12, ProductName = "RESTMOR SUPREME COBALT BLUE 500GPSM TOWEL BALE", StatusId = 1, StatusName = "Enabled", Model = "DT-050", Quantity = 2, Price = 676, ImagePath = @"../../../Content/admin/images/Product/TG-230-300-40x40.jpg" });
            Product.Add(new ProductEntity() { ProductId = 13, ProductName = "RESTMOR SUPREME SEAFOAM 500GPSM TOWEL BALE", StatusId = 1, StatusName = "Enabled", Model = "RT-026", Quantity = 2, Price = 6666, ImagePath = @"../../../Content/admin/images/Product/TG-235-300-40x40.jpg" });
            Product.Add(new ProductEntity() { ProductId = 14, ProductName = "RESTMOR SUPREME PARSLEY 500GPSM TOWEL BALE", StatusId = 1, StatusName = "Enabled", Model = "RT-019", Quantity = 2, Price = 2366, ImagePath = @"../../../Content/admin/images/Product/ZW-1344-300-40x40.jpg" });
            Product.Add(new ProductEntity() { ProductId = 15, ProductName = "RESTMOR SUPREME MAUVE 500GPSM TOWEL BALE", StatusId = 1, StatusName = "Enabled", Model = "RT-015", Quantity = 2, Price = 23236, ImagePath = @"../../../Content/admin/images/Product/ZW-146-300-40x40.jpg" });
            return Product;
        }

        public ProductEntity GetProductById(int ProductId)
        {
            ProductEntity ProductEntity = new ProductEntity();
            if (ProductId > 0)
            {
                ProductEntity = (from t in GetProductList()
                                where t.ProductId == ProductId
                                select t).FirstOrDefault();
            }
            else
            {
                ProductEntity = new ProductEntity();
                ProductEntity.StatusId = 1;
            }
            return ProductEntity;
        }
    }
}
