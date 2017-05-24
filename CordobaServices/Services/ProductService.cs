using CordobaModels;
using CordobaModels.Entities;
using CordobaServices.Interfaces;
using CordobaServices.Helpers;
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
        //private GenericRepository<LanguageEntity> objGenericRepository = new GenericRepository<LanguageEntity>();

        private GenericRepository<ProductEntity> objGenericRepository = new GenericRepository<ProductEntity>();
        public  IEnumerable<ProductEntity> GetProductList(string sortColumn, TableParameter<ProductEntity> filter, string PageFrom = "")
        {
            try
            {
                var paramOrderBy = new SqlParameter { ParameterName = "OrderBy", DbType = DbType.String, Value = sortColumn };
                var paramPageSize = new SqlParameter { ParameterName = "PageSize", DbType = DbType.Int32, Value = filter != null ? filter.iDisplayLength : 10 };
                var paramPageIndex = new SqlParameter { ParameterName = "PageIndex", DbType = DbType.Int32, Value = filter != null ? filter.PageIndex : 1 };
                var paramPageFrom = new SqlParameter { ParameterName = "PageFrom", DbType = DbType.String, Value = PageFrom };
                var query = objGenericRepository.ExecuteSQL<ProductEntity>("GetProductList", paramOrderBy, paramPageSize, paramPageIndex, paramPageFrom).AsQueryable();
                return query;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ProductEntity GetProductById(int product_id)
        {
            ProductEntity ProductEntity = new ProductEntity();
            List<ProductDescriptionList> ProductDescriptionList = new List<ProductDescriptionList>();
            List<CatalogueEntity> CatalogueList = new List<CatalogueEntity>();

            if (product_id > 0)
            {
                var paramProductId = new SqlParameter { ParameterName = "product_id", DbType = DbType.Int32, Value = product_id };
                var result = objGenericRepository.ExecuteSQL<ProductEntity>("GetProductById", paramProductId).FirstOrDefault();
                ProductEntity = result;

                var paramProductIdForDesc = new SqlParameter { ParameterName = "product_id", DbType = DbType.Int32, Value = product_id };
                var DescResult = objGenericRepository.ExecuteSQL<ProductDescriptionList>("GetProductDescriptionList", paramProductIdForDesc).ToList<ProductDescriptionList>();
                if (DescResult != null)
                    ProductDescriptionList = DescResult.ToList();
            }
            else
            {
                ProductEntity = new ProductEntity();
                ProductEntity.ProductDescriptionList = ProductDescriptionList;
                ProductEntity.status = 1;
            }

            var paramProductIdForProduct = new SqlParameter
            {
                ParameterName = "product_id",
                DbType = DbType.Int32,
                Value = product_id
            };
            var catalogueResult = objGenericRepository.ExecuteSQL<CatalogueEntity>("GetProductToCatalogueList", paramProductIdForProduct).ToList<CatalogueEntity>();
            if (catalogueResult != null)
                CatalogueList = catalogueResult.ToList();

            ProductEntity.ProductDescriptionList = ProductDescriptionList;
            ProductEntity.CatalogueList = CatalogueList;
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


        public int InsertUpdateProduct(ProductEntity productEntity)
        {
            string ProductDescriptionXml = Helpers.ConvertToXml<ProductDescriptionList>.GetXMLString(productEntity.ProductDescriptionList);
            
            SqlParameter[] sqlParameter = new SqlParameter[] {
                                                   new SqlParameter("product_id", productEntity.product_id)
                                                 , new SqlParameter("Image", productEntity.Image ??  (object)DBNull.Value)
                                                 , new SqlParameter("Model", productEntity.Model ??  (object)DBNull.Value)
                                                 , new SqlParameter("Price", productEntity.Price)
                                                 , new SqlParameter("Quantity", productEntity.Quantity ??  (object)DBNull.Value )
                                                 , new SqlParameter("minimum", productEntity.minimum ??  (object)DBNull.Value)
                                                 , new SqlParameter("subtract", productEntity.subtract)
                                                 , new SqlParameter("stock_status_id", productEntity.stock_status_id)
                                                 , new SqlParameter("shipping", productEntity.shipping)
                                                 , new SqlParameter("date_added", productEntity.date_added)
                                                 , new SqlParameter("status", productEntity.status)
                                                 , new SqlParameter("sort_order", productEntity.sort_order)
                                                 , new SqlParameter("cost", productEntity.cost)
                                                 , new SqlParameter("country_id", productEntity.country_id)
                                                 , new SqlParameter("manufacturer_id", productEntity.manufacturer_id)
                                                 , new SqlParameter("category_id", productEntity.category_id)
                                                 , new SqlParameter("supplier_id", productEntity.supplier_id)
                                                 , new SqlParameter("CatalogueIdCSV", productEntity.CatalogueIdCSV)
                                                 , new SqlParameter("ProductDescriptionXml", ProductDescriptionXml)
                                                };
            int result = objGenericRepository.ExecuteSQL<int>("InsertUpdateProduct", sqlParameter).FirstOrDefault();
            return result;
        }

        public List<ProductEntity> GetProductListByCategoryAndStoreId(int StoreID, int CategoryId)
        {
            SqlParameter[] sqlParameter = new SqlParameter[] {
                                                   new SqlParameter("StoreID", StoreID)
                                                   , new SqlParameter("CategoryId",CategoryId)
                                               };

            var ProductList = objGenericRepository.ExecuteSQL<ProductEntity>("GetProductListByCategoryAndStoreId", sqlParameter).ToList();
           return ProductList;

        }

    }      
}
