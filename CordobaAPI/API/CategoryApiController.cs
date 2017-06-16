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
using System.Web;
using System.Web.Http;

namespace CordobaAPI.API
{
    public class CategoryApiController : ApiController
    {
        public ICategoryServices _categoryServices;
        public CategoryApiController()
        {
            _categoryServices = new CategoryService();
        }
        [HttpGet]
        public HttpResponseMessage GetCategoryList(int Category_Id = 0)
        {
            try
            {
                var result = _categoryServices.GetCategoryList(Category_Id);
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
        public HttpResponseMessage GetCategoryById(int Category_Id)
        {
            try
            {
                var result = _categoryServices.GetCategoryById(Category_Id);
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



        //Popular Category
        [HttpGet]
        public HttpResponseMessage GetCategoryListByStoreIdPopular(int storeID = 0)
        {
            try
            {
                var result = _categoryServices.GetCategoryListByStoreIdPopular(storeID);
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong! Please try again later.");
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet]
        public HttpResponseMessage GetStoreNameList()
        {
            try
            {
                var result = _categoryServices.GetStoreNameList();
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong! Please try again later.");
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost]
        public HttpResponseMessage InsertOrUpdateCategoryAsPopular(CategoryPopularEntity CategoryPopularEntity)
        {
            try
            {
                var result = _categoryServices.InsertOrUpdateCategoryAsPopular(CategoryPopularEntity);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {

                throw;
            }

        }



        [HttpGet]
        public HttpResponseMessage GetLanguageList()
        {
            try
            {
                var result = _categoryServices.GetLanguageList();
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
        public HttpResponseMessage GetParentCategoryList()
        {
            try
            {
                var result = _categoryServices.GetParentCategoryList();
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


        //Insert Or Update Category
        [HttpPost]
        public HttpResponseMessage InsertOrUpdateCategory(CategoryEntity categoryEntity)
        {
            try
            {
                var result = _categoryServices.InsertOrUpdateCategory(categoryEntity);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                throw;
            }
        }


        //Delete Category
        [HttpPost]
        public HttpResponseMessage DeleteCategory(int Category_Id)
        {
            try
            {
                var result = _categoryServices.DeleteCategory(Category_Id);
                if (result > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong! Please try again later.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public HttpResponseMessage UploadCategoryImage(int Category_Id)
        {
            bool res = false;
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                // Get the uploaded image from the Files collection
                var httpPostedFile = HttpContext.Current.Request.Files[0];

                if (httpPostedFile != null)
                {
                    string folderPath = ConfigurationManager.AppSettings["FileUploadPath"].ToString() + "data//" + CordobaCommon.Enum.CommonEnums.FolderName.Category.ToString();
                    if (!string.IsNullOrWhiteSpace(folderPath))
                    {
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        string childFolderPath = folderPath + "/" + Category_Id;
                        if (!Directory.Exists(childFolderPath))
                        {
                            Directory.CreateDirectory(childFolderPath);
                        }

                        string fileName = Category_Id + "/" + httpPostedFile.FileName;
                        res = _categoryServices.UpdateCategoryImage(Category_Id, "data/" + CordobaCommon.Enum.CommonEnums.FolderName.Category.ToString() + "/" + fileName);

                        if (res == true)
                        {
                            httpPostedFile.SaveAs(folderPath + "\\" + fileName);

                            var directoryFiles = Directory.GetFiles(childFolderPath);
                            foreach (var filepath in directoryFiles)
                            {
                                if (Path.GetFileName(filepath) != httpPostedFile.FileName)
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
    }
}