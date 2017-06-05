using CordobaModels;
using CordobaModels.Entities;
using CordobaServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Services
{
    public class CurrencyService : ICurrencyService
    {
        private GenericRepository<CurrencyEntity> CurrencyEntityGenericRepository = new GenericRepository<CurrencyEntity>();
        public IList<CurrencyEntity> GetCurrencyList()
        {
            try
            {
                var CurrencyList = CurrencyEntityGenericRepository.ExecuteSQL<CurrencyEntity>("EXEC GetCurrencyList").ToList<CurrencyEntity>().ToList();
                return CurrencyList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public CurrencyEntity GetCurrencyDetail(int currencyID = 0)
        {
            CurrencyEntity CurrencyDetail = new CurrencyEntity();
            if (currencyID > 0)
            {
                try
                {
                    SqlParameter[] param = new SqlParameter[1];
                    param[0] = new SqlParameter("currency_id", currencyID);
                    CurrencyDetail = CurrencyEntityGenericRepository.ExecuteSQL<CurrencyEntity>("EXEC GetCurrencyDetails", param).ToList<CurrencyEntity>().FirstOrDefault();

                }
                catch (Exception)
                {

                    throw;
                }

            }

            return CurrencyDetail;

        }

        public int CreateOrUpdateCurrency(CurrencyEntity currency)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("currency_id", currency.currency_id);
                param[1] = new SqlParameter("title", currency.title);
                param[2] = new SqlParameter("code", currency.code);
                param[3] = new SqlParameter("symbol_left", currency.symbol_left);
                param[4] = new SqlParameter("symbol_right", currency.symbol_right);
                param[5] = new SqlParameter("decimal_place", currency.decimal_place);
                param[6] = new SqlParameter("value", currency.value);
                param[7] = new SqlParameter("status", currency.status);
               
                var result = CurrencyEntityGenericRepository.ExecuteSQL<int>("EXEC InsertOrUpdateCurrency", param).ToList<int>().FirstOrDefault();

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public int DeleteCurrency(int CurrencyId)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("currency_id", CurrencyId);
                
                var Result = CurrencyEntityGenericRepository.ExecuteSQL<int>("DeleteCurrency", param).ToList<int>().FirstOrDefault();
                return Result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
