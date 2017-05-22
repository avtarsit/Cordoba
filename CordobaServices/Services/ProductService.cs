﻿using CordobaModels;
using CordobaModels.Entities;
using CordobaServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Services
{
    public class ProductService : IProductServices
    {
        private GenericRepository<LanguageEntity> objGenericRepository = new GenericRepository<LanguageEntity>();

        public List<ProductEntity> GetProductList()
        {
            List<ProductEntity> Product = new List<ProductEntity>();

            Product.Add(new ProductEntity() { product_id = 1, name = "GIORGIO ARMANI AR6011 300371 SUNGLASSES MATTE GUNMETAL SIZE 66", status = 1, StatusName = "Enabled", model = "LAR-002", quantity = 1, price = 2323, image = @"../../../Content/admin/images/Product/BU-026-300-40x40.jpg" });
            Product.Add(new ProductEntity() { product_id = 2, name = "KINDLE 80250901 FIRE HDX 8.9 WIFI 16GB BLACK", status = 1, StatusName = "Enabled", model = "ZW-1344", quantity = 2, price = 2323, image = @"../../../Content/admin/images/Product/DP-059-300-40x40.jpg" });
            Product.Add(new ProductEntity() { product_id = 3, name = "ROBERTS ECO PLAY DAB FM RDS DIGITAL RADIO BLACK", status = 1, StatusName = "Enabled", model = "RR-026", quantity = 3, price = 2323, image = @"../../../Content/admin/images/Product/DP-059-300-40x40.jpg" });
            Product.Add(new ProductEntity() { product_id = 4, name = "DRAPER 10347 STAINLESS STEEL FORK & SPADE WITH TROWEL & WEEDER SET", status = 1, StatusName = "Enabled", model = "DP-059", quantity = 3, price = 2323, image = @"../../../Content/admin/images/Product/DT-050-300-40x40.jpg" });
            Product.Add(new ProductEntity() { product_id = 5, name = "BUSHNELL 242410 10X42 EXCURSION HD BINOCULARS", status = 1, StatusName = "Enabled", model = "BU-026", quantity = 4, price = 232453, image = @"../../../Content/admin/images/Product/LAR-002-300-40x40.jpg" });
            Product.Add(new ProductEntity() { product_id = 6, name = "TAG HEUER WAT1110.BA0950 LINK GENTS WATCH", status = 1, StatusName = "Enabled", model = "TG-235", quantity = 2, price = 23234, image = @"../../../Content/admin/images/Product/RR-026-300-40x40.jpg" });
            Product.Add(new ProductEntity() { product_id = 7, name = "TAG HEUER WAR211A.BA0782 CARRERA GENTS WATCH", status = 1, StatusName = "Enabled", model = "TG-233", quantity = 2, price = 23233, image = @"../../../Content/admin/images/Product/RT-011-300-40x40.jpg" });
            Product.Add(new ProductEntity() { product_id = 8, name = "TAG HEUER WAY1413.BA0920 AQUARACER LADIES WATCH", status = 1, StatusName = "Enabled", model = "TG-230", quantity = 2, price = 23233, image = @"../../../Content/admin/images/Product/RT-013-300-40x40.jpg" });
            Product.Add(new ProductEntity() { product_id = 9, name = "TAG HEUER CAU1113.BA0858 FORMULA 1 GENTS INDY 500 WATCH", status = 1, StatusName = "Enabled", model = "TG-229", quantity = 1, price = 233, image = @"../../../Content/admin/images/Product/RT-015-300-40x40.jpg" });
            Product.Add(new ProductEntity() { product_id = 10, name = "TAG HEUER CAW2111.FC6183 MONACO GENTS CHRONOGRAPH WATCH", status = 1, StatusName = "Enabled", model = "TG-226", quantity = 2, price = 23343, image = @"../../../Content/admin/images/Product/RT-026-300-40x40.jpg" });
            Product.Add(new ProductEntity() { product_id = 11, name = "DARTINGTON ES724 ESSENTIALS WINE CARAFE", status = 1, StatusName = "Enabled", model = "TG-222", quantity = 200, price = 676, image = @"../../../Content/admin/images/Product/TG-222-300-40x40.jpg" });
            Product.Add(new ProductEntity() { product_id = 12, name = "RESTMOR SUPREME COBALT BLUE 500GPSM TOWEL BALE", status = 1, StatusName = "Enabled", model = "DT-050", quantity = 2, price = 676, image = @"../../../Content/admin/images/Product/TG-230-300-40x40.jpg" });
            Product.Add(new ProductEntity() { product_id = 13, name = "RESTMOR SUPREME SEAFOAM 500GPSM TOWEL BALE", status = 1, StatusName = "Enabled", model = "RT-026", quantity = 2, price = 6666, image = @"../../../Content/admin/images/Product/TG-235-300-40x40.jpg" });
            Product.Add(new ProductEntity() { product_id = 14, name = "RESTMOR SUPREME PARSLEY 500GPSM TOWEL BALE", status = 1, StatusName = "Enabled", model = "RT-019", quantity = 2, price = 2366, image = @"../../../Content/admin/images/Product/ZW-1344-300-40x40.jpg" });
            Product.Add(new ProductEntity() { product_id = 15, name = "RESTMOR SUPREME MAUVE 500GPSM TOWEL BALE", status = 1, StatusName = "Enabled", model = "RT-015", quantity = 2, price = 23236, image = @"../../../Content/admin/images/Product/ZW-146-300-40x40.jpg" });

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
                ProductEntity.status = 1;
            }
            return ProductEntity;
        }

        public int AddProductToCart(int store_id, int customer_id, int product_id, int qty, int cartgroup_id)
        {
            SqlParameter[] sqlParameter = new SqlParameter[] {
                                                   new SqlParameter("store_id", store_id)
                                                 , new SqlParameter("customer_id", customer_id)
                                                 , new SqlParameter("product_id", product_id)
                                                 , new SqlParameter("qty", qty)
                                                 , new SqlParameter("cartgroup_id", cartgroup_id)}; 
            
            int result = objGenericRepository.ExecuteSQL<int>("AddProductToCart", sqlParameter).FirstOrDefault();
            return result;

        }

        public int DeleteProductFromCart(int cart_id)
        {
            SqlParameter[] sqlParameter = new SqlParameter[] {
                                                   new SqlParameter("cart_id", cart_id)
                                               };

            int result = objGenericRepository.ExecuteSQL<int>("DeleteProductFromCart", sqlParameter).FirstOrDefault();
            return result;

        }


    }
}
