using CordobaServices.Interfaces;
using CordobaServices.Helpers;
using CordobaServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CordobaModels.Entities;



namespace CordobaAPI.API
{
    public class ProductApiController : ApiController
    {
        public IProductServices _ProductServices;
        public ProductApiController()
        {
            _ProductServices = new ProductService();
        }
        [HttpPost]
        public TableParameter<ProductEntity> GetProductList(int StoreId, int LoggedInUserId, int PageIndex, string name, decimal? Price, int? status, string Model, int? Quantity, TableParameter<ProductEntity> tableParameter)
        {
            try
            {

                tableParameter.PageIndex = PageIndex;
                string sortColumn = tableParameter.SortColumn.Desc ? tableParameter.SortColumn.Column + " desc" : tableParameter.SortColumn.Column + " asc";
                var result = _ProductServices.GetProductList(StoreId, LoggedInUserId, sortColumn, tableParameter, name,  Price,  status, Model,  Quantity).ToList();
                int totalRecords = 0;
                if (result != null && result.Count > 0)
                {
                    totalRecords = result.FirstOrDefault().TotalRecords;
                }
                return new TableParameter<ProductEntity>
                {
                    aaData = result.ToList(),
                    iTotalRecords = totalRecords,
                    iTotalDisplayRecords = totalRecords
                };
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpGet]
        public HttpResponseMessage GetProductById(int StoreId, int LoggedInUserId, int product_id)
        {
            try
            {
                var result = _ProductServices.GetProductById(StoreId, LoggedInUserId,product_id);
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong! Please try again later.");
            }
            catch (Exception)
            {
                throw;
            }

        }
        [HttpGet]
        public HttpResponseMessage AddProductToCart(int store_id, int LoggedInUserId, int customer_id, int product_id, int qty, int cartgroup_id)
        {
            try
            {
                var result = _ProductServices.AddProductToCart(  store_id, LoggedInUserId, customer_id,  product_id,  qty,  cartgroup_id);             
                    return Request.CreateResponse(HttpStatusCode.OK, result);
               
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpGet]
        public HttpResponseMessage DeleteProductFromCart(int StoreId, int LoggedInUserId, int cart_id)
        {
            try
            {
                var result = _ProductServices.DeleteProductFromCart(StoreId, LoggedInUserId, cart_id);
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong! Please try again later.");
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost]
        public HttpResponseMessage InsertUpdateProduct(int StoreId, int LoggedInUserId, ProductEntity productEntity)
        {
            try
            {
                var result = _ProductServices.InsertUpdateProduct(StoreId, LoggedInUserId, productEntity);
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong! Please try again later.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public HttpResponseMessage GetProductListByCategoryAndStoreId(int StoreID, int LoggedInUserId, int CategoryId, int Customer_Id = 0, string WhatAreYouLookingFor = "")
        {
            try
            {
                var result = _ProductServices.GetProductListByCategoryAndStoreId(StoreID, LoggedInUserId, CategoryId, Customer_Id, WhatAreYouLookingFor);
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong! Please try again later.");
            }
            catch (Exception)
            {
                throw;
            }
        }



        [HttpPost]
        public HttpResponseMessage DeleteProduct(int StoreId, int LoggedInUserId, int product_id)
        {
            try
            {
                var result = _ProductServices.DeleteProduct(StoreId, LoggedInUserId, product_id);
                if (result>0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong! Please try again later.");
            }
            catch (Exception ex )
            {
                throw ex;
            }
        }

        [HttpGet]
        public HttpResponseMessage GetProductDetailForLayout(int StoreID, int LoggedInUserId, int ProductId)
        {
            try
            {
                var result = _ProductServices.GetProductDetailForLayout(StoreID, LoggedInUserId, ProductId);
             
                    return Request.CreateResponse(HttpStatusCode.OK, result);              
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public HttpResponseMessage GetRelatedProductList(int StoreID, int LoggedInUserId, int SelectedProductId, int RelatedProductId)
        {
            try
            {
                var result = _ProductServices.GetRelatedProductList(StoreID, LoggedInUserId, SelectedProductId, RelatedProductId);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public HttpResponseMessage InsertAsHotProduct(int LoggedInUserId, HotSpecialProductEntity hotSpecialProductEntity)
        {
            try
            {
                var result = _ProductServices.InsertAsHotProduct(LoggedInUserId, hotSpecialProductEntity);
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong! Please try again later.");
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        [HttpPost]
        public HttpResponseMessage InsertAsSpecialProduct(int LoggedInUserId, HotSpecialProductEntity hotSpecialProductEntity)
        {
            try
            {
                var result = _ProductServices.InsertAsSpecialProduct( LoggedInUserId, hotSpecialProductEntity);
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong! Please try again later.");
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet]
        public HttpResponseMessage GetHotOrSpecialProductById(int language_id, int store_id, int LoggedInUserId, int product_id)
        {
            try
            {
                var result = _ProductServices.GetHotOrSpecialProductById(language_id, store_id, LoggedInUserId, product_id);
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong! Please try again later.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public HttpResponseMessage GetHotOrSpecialProductDetailById(int store_id, int LoggedInUserId, bool IsHotProduct, int product_id)
        {
            try
            {
                var result = _ProductServices.GetHotOrSpecialProductDetailById(store_id, LoggedInUserId, IsHotProduct, product_id);
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong! Please try again later.");
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}