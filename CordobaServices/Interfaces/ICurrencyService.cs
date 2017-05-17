using CordobaModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Interfaces
{
    public interface ICurrencyService
    {
        IList<CurrencyEntity> GetCurrencyList();
        CurrencyEntity GetCurrencyDetail(int currencyID = 0);

        int CreateOrUpdateCurrency(CurrencyEntity currency);

        int DeleteCurrency(int CurrencyId);
    }
}
