using CordobaModels.Entities;
using CordobaServices.Interfaces;
using CordobaServices.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace CordobaAPI.API
{
    public class StoreApiController : ApiController
    {
        public IStoreServices _StoreServices;

        public StoreApiController()
        {
            _StoreServices = new StoreServices();
        }


        [HttpGet]
        public HttpResponseMessage GetStoreList(int? StoreID, int LoggedInUserId)
        {
            try
            {
                var result = _StoreServices.GetStoreList(StoreID, LoggedInUserId);
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
        public HttpResponseMessage GetStoreById(int store_id, int LoggedInUserId)
        {
            try
            {
                var result = _StoreServices.GetStoreById(store_id, LoggedInUserId);
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
        public HttpResponseMessage InsertUpdateStore(int LoggedInUserId, StoreEntity storeEntity)
        {
            try
            {
                var result = _StoreServices.InsertUpdateStore(storeEntity, LoggedInUserId);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public HttpResponseMessage DeleteStoreById_Admin(int store_id, int LoggedInUserId)
        {
            try
            {
                var result = _StoreServices.DeleteStoreById_Admin(store_id, LoggedInUserId);
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong? Please try again later.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: api/StoreApi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/StoreApi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/StoreApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/StoreApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/StoreApi/5
        public void Delete(int id)
        {
        }

        [HttpPost]
        public HttpResponseMessage UploadStoreImage(int Store_Id, int ImageKey, int layout, string Store_Name)
        {
            bool res = false;
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                // Get the uploaded image from the Files collection
                var httpPostedFile = HttpContext.Current.Request.Files[0];

                if (httpPostedFile != null)
                {
                    string folderPath = ConfigurationManager.AppSettings["FileUploadPath"].ToString() + "data//" + CordobaCommon.Enum.CommonEnums.FolderName.StoreImage.ToString();
                    if (!string.IsNullOrWhiteSpace(folderPath))
                    {
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        string childFolderPath = folderPath + "/" + Store_Id;
                        if (!Directory.Exists(childFolderPath))
                        {
                            Directory.CreateDirectory(childFolderPath);
                        }

                        childFolderPath += "/" + ImageKey;
                        if (!Directory.Exists(childFolderPath))
                        {
                            Directory.CreateDirectory(childFolderPath);
                        }

                        string fileName = Store_Id + "/" + ImageKey + "/" + Store_Name + "_Image.png"; //before httpPostedFile.FileName
                        res = _StoreServices.UploadStoreImage(Store_Id, "data/" + CordobaCommon.Enum.CommonEnums.FolderName.StoreImage.ToString() + "/" + fileName, ImageKey, layout);

                        if (res == true)
                        {
                            httpPostedFile.SaveAs(folderPath + "\\" + fileName);

                            var directoryFiles = Directory.GetFiles(childFolderPath);
                            foreach (var filepath in directoryFiles)
                            {
                                if (Path.GetFileName(filepath) != Store_Name + "_Image.png") //before httpPostedFile.FileName
                                {
                                    File.Delete(filepath);
                                }
                            }
                        }
                    }
                }
            }

            if (res == true)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { data = true });
            }

            return Request.CreateResponse(HttpStatusCode.NotImplemented, new { data = false });
        }

        [HttpPost]
        public HttpResponseMessage UploadStoreLogo(int store_id, string store_name)
        {
            bool res = false;
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                // Get the uploaded image from the Files collection
                var httpPostedFile = HttpContext.Current.Request.Files[0];

                if (httpPostedFile != null)
                {
                    string folderPath = ConfigurationManager.AppSettings["FileUploadPath"].ToString() + "data//" + CordobaCommon.Enum.CommonEnums.FolderName.store_logos.ToString();
                    if (!string.IsNullOrWhiteSpace(folderPath))
                    {
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }



                        string fileName = store_name + "-" + httpPostedFile.FileName;
                        res = _StoreServices.UploadStoreLogo(store_id, "data/" + CordobaCommon.Enum.CommonEnums.FolderName.store_logos.ToString() + "/" + fileName);

                        if (res == true)
                        {
                            httpPostedFile.SaveAs(folderPath + "\\" + fileName);

                            var directoryFiles = Directory.GetFiles(folderPath);
                            foreach (var filepath in directoryFiles)
                            {
                                if (Path.GetFileName(filepath) != httpPostedFile.FileName)
                                {
                                    //File.Delete(filepath);
                                }
                            }
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                return Request.CreateResponse(HttpStatusCode.OK);


            }
            if (res == true)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { data = true });
            }

            return Request.CreateResponse(HttpStatusCode.NotImplemented, new { data = false });
        }

        [HttpGet]
        public HttpResponseMessage GetAdvertisementImageList(int store_id)
        {
            try
            {
                var result = _StoreServices.GetAdvertisementImageList(store_id);
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong? Please try again later.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public HttpResponseMessage GetStoreHTMLCharts(int StoreID, int Month, int Year)
        {
            try
            {
                var result = _StoreServices.GetStoreHTMLCharts(StoreID, Month, Year);
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong? Please try again later.");
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public HttpResponseMessage ExportStoreHTMLPDF(StoreEntity storeentity)
        {
            var htmlContent = storeentity.template;//String.Format("<body>Hello world: {0}</body>", DateTime.Now);
            var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter();
            var pdfBytes = htmlToPdf.GeneratePdf(htmlContent);

            HttpResponseMessage httpResponseMessage;

            HttpResponseMessage streamContent = new HttpResponseMessage(HttpStatusCode.OK);
            Stream @null = Stream.Null;
            streamContent.Content = new StreamContent(new MemoryStream(pdfBytes));
            streamContent.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            streamContent.Content.Headers.Add("content-disposition", string.Concat("inline;  filename=\"", "AssociateObjective.pdf", "\""));
            httpResponseMessage = streamContent;
            return httpResponseMessage;

        }

    }
}
