using CordobaModels;
using CordobaModels.Entities;
using CordobaServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CordobaServices.Helpers;

namespace CordobaServices.Services
{
    public class OrderService : IOrderService
    {
        private GenericRepository<OrderEntity> objGenericRepository = new GenericRepository<OrderEntity>();

        public List<OrderEntity> GetOrderDetails(int orderId)
        {
            List<OrderEntity> orders = new List<OrderEntity>();
            var paramOrderId = new SqlParameter { ParameterName = "order_id", DbType = DbType.Int32, Value = orderId };
            orders = objGenericRepository.ExecuteSQL<OrderEntity>("GetOrderDetails", paramOrderId).ToList();

            if (orders != null && orders.Count != 0)
            {
                List<OrderProductEntity> orderProducts = new List<OrderProductEntity>();
                var paramOrderProductOrderId = new SqlParameter { ParameterName = "order_id", DbType = DbType.Int32, Value = orderId };
                orderProducts = objGenericRepository.ExecuteSQL<OrderProductEntity>("GetOrderProductByOrderId", paramOrderProductOrderId).ToList();

                List<OrderHistoryEntity> orderHistory = new List<OrderHistoryEntity>();
                var paramOrderHistoryOrderId = new SqlParameter { ParameterName = "order_id", DbType = DbType.Int32, Value = orderId };
                orderHistory = objGenericRepository.ExecuteSQL<OrderHistoryEntity>("GetOrderHistoryByOrderId", paramOrderHistoryOrderId).ToList();

                orders[0].orderProductEntity = orderProducts;
                orders[0].orderHistoryEntity = orderHistory;
            }

            return orders;
        }

        public int InsertOrderHistory(OrderHistoryEntity objHistoryEntity)
        {
            var paramOrderId = new SqlParameter { ParameterName = "order_id", DbType = DbType.Int32, Value = objHistoryEntity.order_id };
            var paramnotify = new SqlParameter { ParameterName = "notify", DbType = DbType.Int32, Value = objHistoryEntity.notify };
            var paramcomment = new SqlParameter { ParameterName = "comment", DbType = DbType.String, Value = objHistoryEntity.comment };
            var paramorderStatusId = new SqlParameter { ParameterName = "order_status_id", DbType = DbType.Int32, Value = objHistoryEntity.order_status_id };
            int result = objGenericRepository.ExecuteSQL<int>("InsertOrderHistory", paramOrderId, paramnotify, paramcomment, paramorderStatusId).FirstOrDefault();
            return result;
        }

        public IEnumerable<OrderEntity> GetOrderList(string sortColumn, TableParameter<OrderEntity> filter, string PageFrom = "")
        {
            try
            {
                var paramOrderBy = new SqlParameter { ParameterName = "OrderBy", DbType = DbType.String, Value = sortColumn };
                var paramPageSize = new SqlParameter { ParameterName = "PageSize", DbType = DbType.Int32, Value = filter != null ? filter.iDisplayLength : 10 };
                var paramPageIndex = new SqlParameter { ParameterName = "PageIndex", DbType = DbType.Int32, Value = filter != null ? filter.PageIndex : 1 };
                var paramPageFrom = new SqlParameter { ParameterName = "PageFrom", DbType = DbType.String, Value = PageFrom };
                var query = objGenericRepository.ExecuteSQL<OrderEntity>("GetOrderList", paramOrderBy, paramPageSize, paramPageIndex, paramPageFrom).AsQueryable();
                return query;
            }
            catch (Exception)
            {
                throw;
            }

            //return result;
        }

        public List<CustomerGroupEntity> GetCustomerGroupList()
        {
            try
            {
                var listCustomerGroup = objGenericRepository.ExecuteSQL<CustomerGroupEntity>("GetCustomerGroupList").ToList();
                return listCustomerGroup;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<CurrencyEntity> GetCurrencyList()
        {
            try
            {
                var listCustomerGroup = objGenericRepository.ExecuteSQL<CurrencyEntity>("GetCurrencyList").ToList();
                return listCustomerGroup;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ZoneEntity> GetZoneListByCountry(int countryId)
        {
            try
            {
                var paramCountryid = new SqlParameter { ParameterName = "country_id", DbType = DbType.Int32, Value = countryId };
                var listZones = objGenericRepository.ExecuteSQL<ZoneEntity>("GetZoneListByCountry", paramCountryid).ToList();
                return listZones;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<AddressEntity> GetCustomerAddress(int orderId)
        {
            try
            {
                var paramOrderId = new SqlParameter { ParameterName = "orderId", DbType = DbType.Int32, Value = orderId };
                var listZones = objGenericRepository.ExecuteSQL<AddressEntity>("GetCustomerAddress", paramOrderId).ToList();
                return listZones;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public int UpdateOrder_CutomerDetails(OrderEntity objOrderEntity)
        {
            try
            {
                var paramstore_id = new SqlParameter { ParameterName = "store_id ", DbType = DbType.Int32, Value = objOrderEntity.store_id };
                var paramcurrency_id = new SqlParameter { ParameterName = "currency_id", DbType = DbType.Int32, Value = objOrderEntity.currency_id };
                var paramcustomer_id = new SqlParameter { ParameterName = "customer_id", DbType = DbType.Int32, Value = objOrderEntity.customer_id };
                var paramcustomer_group_id = new SqlParameter { ParameterName = "customer_group_id", DbType = DbType.Int32, Value = objOrderEntity.customer_group_id };
                var paramfirstname = new SqlParameter { ParameterName = "firstname", DbType = DbType.String, Value = objOrderEntity.firstname };
                var paramlastname = new SqlParameter { ParameterName = "lastname", DbType = DbType.String, Value = objOrderEntity.lastname };
                var paramemail = new SqlParameter { ParameterName = "email", DbType = DbType.String, Value = objOrderEntity.email };
                var paramtelephone = new SqlParameter { ParameterName = "telephone", DbType = DbType.String, Value = objOrderEntity.telephone };
                var paramfax = new SqlParameter { ParameterName = "fax", DbType = DbType.String, Value = objOrderEntity.fax };
                var paramorder_id = new SqlParameter { ParameterName = "order_id", DbType = DbType.Int32, Value = objOrderEntity.order_id };
                var list = objGenericRepository.ExecuteSQL<int>("UpdateOrder_CutomerDetails", paramstore_id, paramcurrency_id, paramcustomer_id, paramcustomer_group_id,
                    paramfirstname, paramlastname, paramemail,
                    paramtelephone, paramfax, paramorder_id).FirstOrDefault();
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int UpdateOrder_PaymentDetails(OrderEntity objOrderEntity)
        {
            try
            {
                var paramorder_id = new SqlParameter { ParameterName = "order_id", DbType = DbType.Int32, Value = objOrderEntity.order_id };
                var paramaddress_id = new SqlParameter { ParameterName = "address_id", DbType = DbType.Int32, Value = objOrderEntity.address_id };
                var parampayment_firstname = new SqlParameter { ParameterName = "payment_firstname", DbType = DbType.String, Value = objOrderEntity.payment_firstname };
                var parampayment_lastname = new SqlParameter { ParameterName = "payment_lastname", DbType = DbType.String, Value = objOrderEntity.payment_lastname };
                var parampayment_company = new SqlParameter { ParameterName = "payment_company", DbType = DbType.String, Value = objOrderEntity.payment_company };
                var parampayment_address_1 = new SqlParameter { ParameterName = "payment_address_1", DbType = DbType.String, Value = objOrderEntity.payment_address_1 };
                var parampayment_address_2 = new SqlParameter { ParameterName = "payment_address_2", DbType = DbType.String, Value = objOrderEntity.payment_address_2 };
                var parampayment_city = new SqlParameter { ParameterName = "payment_city", DbType = DbType.String, Value = objOrderEntity.payment_city };
                var parampayment_postcode = new SqlParameter { ParameterName = "payment_postcode", DbType = DbType.String, Value = objOrderEntity.payment_postcode };
                var parampayment_country = new SqlParameter { ParameterName = "payment_country", DbType = DbType.String, Value = objOrderEntity.payment_country };
                var parampayment_country_id = new SqlParameter { ParameterName = "payment_country_id", DbType = DbType.Int32, Value = objOrderEntity.payment_country_id };
                var parampayment_zone_id = new SqlParameter { ParameterName = "payment_zone_id", DbType = DbType.Int32, Value = objOrderEntity.payment_zone_id };
                var parampayment_zone = new SqlParameter { ParameterName = "payment_zone", DbType = DbType.String, Value = objOrderEntity.payment_zone ?? (object)DBNull.Value };

                var list = objGenericRepository.ExecuteSQL<int>("UpdateOrder_PaymentDetails", paramorder_id, paramaddress_id, parampayment_firstname, parampayment_lastname, parampayment_company, parampayment_address_1, parampayment_address_2, parampayment_city, parampayment_postcode, parampayment_country, parampayment_country_id, parampayment_zone_id, parampayment_zone).FirstOrDefault();
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int UpdateOrder_ShippingDetails(OrderEntity objOrderEntity)
        {
            try
            {
                var paramorder_id = new SqlParameter { ParameterName = "order_id ", DbType = DbType.Int32, Value = objOrderEntity.order_id };
                var paramaddress_id = new SqlParameter { ParameterName = "address_id", DbType = DbType.Int32, Value = objOrderEntity.address_id };
                var parampayment_firstname = new SqlParameter { ParameterName = "shipping_firstname", DbType = DbType.String, Value = objOrderEntity.shipping_firstname };
                var parampayment_lastname = new SqlParameter { ParameterName = "shipping_lastname", DbType = DbType.String, Value = objOrderEntity.shipping_lastname };
                var parampayment_company = new SqlParameter { ParameterName = "shipping_company", DbType = DbType.String, Value = objOrderEntity.shipping_company };
                var parampayment_address_1 = new SqlParameter { ParameterName = "shipping_address_1", DbType = DbType.String, Value = objOrderEntity.shipping_address_1 };
                var parampayment_address_2 = new SqlParameter { ParameterName = "shipping_address_2", DbType = DbType.String, Value = objOrderEntity.shipping_address_2 };
                var parampayment_city = new SqlParameter { ParameterName = "shipping_city", DbType = DbType.String, Value = objOrderEntity.shipping_city };
                var parampayment_postcode = new SqlParameter { ParameterName = "shipping_postcode", DbType = DbType.String, Value = objOrderEntity.shipping_postcode };
                var parampayment_country = new SqlParameter { ParameterName = "shipping_country", DbType = DbType.String, Value = objOrderEntity.shipping_country };
                var parampayment_country_id = new SqlParameter { ParameterName = "shipping_country_id", DbType = DbType.Int32, Value = objOrderEntity.shipping_country_id };

                var list = objGenericRepository.ExecuteSQL<int>("UpdateOrder_ShippingDetails", paramorder_id, paramaddress_id,
                    parampayment_firstname, parampayment_lastname, parampayment_company, parampayment_address_1, parampayment_address_2,
                    parampayment_city, parampayment_postcode, parampayment_country, parampayment_country_id).FirstOrDefault();
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<CustomerEntity> GetCustomersByStore(int storeId)
        {
            try
            {
                var paramStore_id = new SqlParameter { ParameterName = "store_id", DbType = DbType.Int32, Value = storeId };
                var list = objGenericRepository.ExecuteSQL<CustomerEntity>("GetCustomersByStore", paramStore_id).ToList();
                return list;
            }
            catch
            {
                throw;
            }
        }

        public int UpdateOrder_TotalDetails(int order_id, int order_status_id, string comment)
        {
            var paramOrderStatusId = new SqlParameter { ParameterName = "order_status_id", DbType = DbType.Int32, Value = order_status_id };
            var paramorderId = new SqlParameter { ParameterName = "order_id", DbType = DbType.Int32, Value = order_id };
            var paramComment = new SqlParameter { ParameterName = "comment", DbType = DbType.String, Value = (comment == null || comment == "") ? " " : comment };
            var result = objGenericRepository.ExecuteSQL<int>("UpdateOrder_TotalDetails", paramOrderStatusId, paramorderId, paramComment).FirstOrDefault();
            return result;
        }
    }
}
