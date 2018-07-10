using CordobaModels.Entities;
using CordobaServices.Interfaces;
using CordobaServices.Services;
using iTextSharp.text;
using iTextSharp.text.pdf;
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

            var htmlContent1 = storeentity.template;//String.Format("<body>Hello world: {0}</body>", DateTime.Now);
            var htmlToPdf1 = new NReco.PdfGenerator.HtmlToPdfConverter();
            var pdfBytes1 = htmlToPdf1.GeneratePdf(htmlContent1);
            //var headingfile = File.Create(HttpContext.Current.Server.MapPath("~/Files//") + "Heading.pdf");
            //headingfile.Write(pdfBytes1,0,pdfBytes1.Length);
            //File.Open(HttpContext.Current.Server.MapPath("~/Files//") + "Heading.pdf", FileMode.Open);
            File.WriteAllBytes(HttpContext.Current.Server.MapPath("~/Files//") + "Heading.pdf", pdfBytes1);

            var htmlContent2 = storeentity.description;
            var htmlToPdf2 = new NReco.PdfGenerator.HtmlToPdfConverter();
            var pdfBytes2 = htmlToPdf2.GeneratePdf(htmlContent2);
            File.WriteAllBytes(HttpContext.Current.Server.MapPath("~/Files//") + "StoreImage.pdf", pdfBytes2);

            var htmlContent3 = storeentity.address;
            var htmlToPdf3 = new NReco.PdfGenerator.HtmlToPdfConverter();
            var pdfBytes3 = htmlToPdf3.GeneratePdf(htmlContent3);
            File.WriteAllBytes(HttpContext.Current.Server.MapPath("~/Files//") + "StoreSummary.pdf", pdfBytes3);

            var htmlContent4 = storeentity.county;
            var htmlToPdf4 = new NReco.PdfGenerator.HtmlToPdfConverter();
            var pdfBytes4 = htmlToPdf4.GeneratePdf(htmlContent4);
            File.WriteAllBytes(HttpContext.Current.Server.MapPath("~/Files//") + "PointsRemaining.pdf", pdfBytes4);

            var htmlContent5 = storeentity.telephone;
            var htmlToPdf5 = new NReco.PdfGenerator.HtmlToPdfConverter();
            var pdfBytes5 = htmlToPdf5.GeneratePdf(htmlContent5);
            File.WriteAllBytes(HttpContext.Current.Server.MapPath("~/Files//") + "ParticipantsLoadedByMonth.pdf", pdfBytes5);

            
            MergePDFs(HttpContext.Current.Server.MapPath("~/Files//") + "Heading.pdf",
                HttpContext.Current.Server.MapPath("~/Files//") + "StoreImage.pdf", 
                HttpContext.Current.Server.MapPath("~/Files//") + "StoreSummary.pdf",
                HttpContext.Current.Server.MapPath("~/Files//") + "PointsRemaining.pdf",
                HttpContext.Current.Server.MapPath("~/Files//") + "ParticipantsLoadedByMonth.pdf");

           // headingfile.Close();

            HttpResponseMessage httpResponseMessage;
            httpResponseMessage = new HttpResponseMessage();
            //HttpResponseMessage streamContent = new HttpResponseMessage(HttpStatusCode.OK);
            //Stream @null = Stream.Null;

            //streamContent.Content = new StreamContent(new MemoryStream(pdfBytes1));
            //streamContent.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            //streamContent.Content.Headers.Add("content-disposition", string.Concat("inline;  filename=\"", "Create.pdf", "\""));
            //httpResponseMessage = streamContent;

            //return httpResponseMessage;
            return httpResponseMessage;
        }

        private void MergePDFs(params string[] filesPath)
        {
            List<PdfReader> readerList = new List<PdfReader>();
            foreach (string filePath in filesPath)
            {
                PdfReader pdfReader = new PdfReader(filePath);
                readerList.Add(pdfReader);
            }

            //Define a new output document and its size, type
            Document document = new Document(PageSize.A4, 0, 0, 0, 0);
            //Create blank output pdf file and get the stream to write on it.
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(HttpContext.Current.Server.MapPath("~/Files//") + "Merge.pdf", FileMode.Create));
            document.Open();
            
            foreach (PdfReader reader in readerList)
            {
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    PdfImportedPage page = writer.GetImportedPage(reader, i);
                    document.Add(iTextSharp.text.Image.GetInstance(page));
                }
            }
            document.Close();
            
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/pdf";
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=Store PDF.pdf");
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.WriteFile(HttpContext.Current.Server.MapPath("~/Files//") + "Merge.pdf");

            System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
            //if (File.Exists(HttpContext.Current.Server.MapPath("~/Files//Heading.pdf")))
            //{
                
            //    File.Delete(HttpContext.Current.Server.MapPath("~/Files//Heading.pdf"));
            //}
            //if (File.Exists(HttpContext.Current.Server.MapPath("~/Files//StoreSummary.pdf")))
            //{
            //    File.Delete(HttpContext.Current.Server.MapPath("~/Files//StoreSummary.pdf"));
            //}
            //if (File.Exists(HttpContext.Current.Server.MapPath("~/Files//Merge.pdf")))
            //{
            //    File.Delete(HttpContext.Current.Server.MapPath("~/Files//Merge.pdf"));
            //}
        }

    }
}
