using CordobaServices.Interfaces;
using CordobaServices.Services;
using CordobaModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using CordobaModels.Entities;
using System.Runtime.InteropServices;
using System.Management;
using System.Diagnostics;
using System.ServiceProcess;

namespace CordobaAPI.API
{
    public class CatalogueApiController : ApiController
    {

        public ICatalogueServices _catalogueServices;
        public CatalogueApiController()
        {
            _catalogueServices = new CatalogueService();
        }
        [HttpGet]
        public HttpResponseMessage GetCatalogueList(int StoreId, int LoggedInUserId)
        {
            try
            {
                var result = _catalogueServices.GetCatalogueList(StoreId, LoggedInUserId);
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
        public HttpResponseMessage GetCatalogueById(int catalogue_id, int StoreId, int LoggedInUserId)
        {
            try
            {
                var result = _catalogueServices.GetCatalogueById(StoreId, LoggedInUserId, catalogue_id);
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
        public HttpResponseMessage InsertUpdateCatalogue(int StoreId, int LoggedInUserId, CatalogueEntity catalogueEntity)
        {
            try
            {
                var result = _catalogueServices.InsertUpdateCatalogue(StoreId, LoggedInUserId, catalogueEntity);
                if (result >= -1)
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
        public HttpResponseMessage DeleteCatalogue(int catalogue_id, int StoreId, int LoggedInUserId)
        {
            try
            {
                var result = _catalogueServices.DeleteCatalogue(catalogue_id, StoreId, LoggedInUserId);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public HttpResponseMessage ImportCatalogue(int StoreId, int LoggedInUserId, int supplier_id, int language_id, int catalogue_id, bool IsConfirmToIgnore)
        {
            try
            {
                var directoryPath = System.Web.HttpContext.Current.Server.MapPath("~/TempFiles");

                if (!System.IO.Directory.Exists(directoryPath))
                {
                    System.IO.Directory.CreateDirectory(directoryPath);
                }
                //string filePath = directoryPath + @"\" + Guid.NewGuid().ToString() + ".xlsx";
                string filePath = directoryPath + @"\" + Guid.NewGuid().ToString() + ".xlsx";
                if (HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    // Get the uploaded image from the Files collection
                    var httpPostedFile = HttpContext.Current.Request.Files[0];

                    if (httpPostedFile != null)
                    {
                        string fileName = httpPostedFile.FileName;
                        string contentType = httpPostedFile.ContentType;
                        string extension = contentType.Substring(contentType.IndexOf('/') + 1, contentType.Length - contentType.IndexOf('/') - 1);
                        int fileSize = httpPostedFile.ContentLength;
                        byte[] fileData = null;

                        using (var binaryReader = new BinaryReader(HttpContext.Current.Request.Files[0].InputStream))
                        {
                            fileData = binaryReader.ReadBytes(HttpContext.Current.Request.Files[0].ContentLength);

                            File.WriteAllBytes(filePath, fileData);
                        }
                    }
                }

                string excelfilepath = filePath;
                string strConnectionString = "";


                if (excelfilepath.ToLower().Trim().EndsWith(".xlsx") || excelfilepath.ToLower().Trim().EndsWith(".xls") || filePath.ToLower().Trim().EndsWith(".csv"))
                {
                    strConnectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", excelfilepath);
                }

                //if (filePath.ToLower().Trim().EndsWith(".xlsx") || filePath.ToLower().Trim().EndsWith(".csv"))
                //{
                //    strConnectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", filePath);
                //}


                OleDbConnection OleDbConn = new OleDbConnection(strConnectionString);

                OleDbConn.Open();
                DataTable dtSheets = OleDbConn.GetSchema("Tables");
                OleDbDataAdapter OleDbAdapter = new OleDbDataAdapter();

                string sql = "SELECT * FROM [" + dtSheets.Rows[0]["Table_name"] + "]";
                DataTable dtXLS = new DataTable(Convert.ToString(dtSheets.Rows[0]["Table_name"]).Replace("$", ""));
                dtXLS.TableName = "Sheet1";
                OleDbCommand oleDbcommand = new OleDbCommand(sql, OleDbConn);

                OleDbAdapter.SelectCommand = oleDbcommand;
                OleDbAdapter.Fill(dtXLS);
                OleDbConn.Close();

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                List<ImportProductCatalogueEntity> result = _catalogueServices.ImportDatatoCatalogue(StoreId, LoggedInUserId, supplier_id, language_id, catalogue_id, dtXLS, IsConfirmToIgnore);
                if (result.Count > 0)
                {
                }
                //else
                //{
                //    //using (var m = new ManagementObject(string.Format("Win32_Service.Name=\"{0}\"", "CordobaInstaller")))
                //    //{
                //    //    m.InvokeMethod("ChangeStartMode", new object[] { "Automatic" });
                //    //}
                //    var sc = new System.ServiceProcess.ServiceController();
                //    sc.ServiceName = "CordobaInstaller";                
                //    sc.Start();
                //}

                //ServiceController controller = new ServiceController();

                //controller.MachineName = ".";
                //controller.ServiceName = "CordobaInstaller";
                //string status = controller.Status.ToString();
                //controller.Stop();

                //controller.Start();

                //var sc = new System.ServiceProcess.ServiceController("CordobaInstaller");
                //sc.Start();
                //sc.WaitForStatus(System.ServiceProcess.ServiceControllerStatus.Running);
                //sc.Stop();
                //sc.WaitForStatus(System.ServiceProcess.ServiceControllerStatus.Stopped);

                //Process process = new Process();
                //process.Start("CordobaCatalogImageService.exe");


                //if (dtXLS != null && dtXLS.Rows.Count > 0)
                //{
                //    Dictionary<string, string> selectedColumnName = SetColumnConfiguration();
                //    selectedColumnName.ToList().ForEach(item =>
                //    {
                //        if (dtXLS.Columns[item.Value] != null)
                //        {
                //            dtXLS.Columns[item.Value].ColumnName = item.Key;
                //        }
                //    });

                //}

                //var result = true;

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}