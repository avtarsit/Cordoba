using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CordobaServices.Interfaces;
using CordobaServices.Services;

namespace CordobaAPI.API
{
    public class LanguageApiController: ApiController
    {
        public ILanguageService _LanguageServices;

        public LanguageApiController()
        {
            _LanguageServices =new LanguageService();
        }


        [HttpGet]
        public HttpResponseMessage GetLanguageList(int? languageId)
        {
            try
            {
                var result = _LanguageServices.GetLanguageList(languageId);
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