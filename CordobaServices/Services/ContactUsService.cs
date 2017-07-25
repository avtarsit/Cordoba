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

namespace CordobaServices.Services
{
    public class ContactUsService : IContactUsService
    {
        private GenericRepository<BannerEntity> contactUsGenericRepository = new GenericRepository<BannerEntity>();

        public int sendContactUsDetails(string firstname, string lastname, string email, string phone, string description, StoreEntity storeEntity)
        {
            try
            {
                return 0;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
