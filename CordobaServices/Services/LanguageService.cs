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
    public class LanguageService : ILanguageService
    {
        private GenericRepository<LanguageEntity> objGenericRepository = new GenericRepository<LanguageEntity>();

        public List<LanguageEntity> GetLanguageList(int? languageId)
        {
            List<LanguageEntity> languages = new List<LanguageEntity>();
            var paramLanguageId = new SqlParameter { ParameterName = "countryId", DbType = DbType.Int32, Value = languageId };
            languages = objGenericRepository.ExecuteSQL<LanguageEntity>("GetLanguageList", paramLanguageId).ToList();
            return languages;
        }
    }
}
