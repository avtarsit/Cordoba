using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace CordobaCatalogImageService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {   
            this.GetImportCatalogImageURLs();
        }

        protected override void OnStop()
        {
        }

        public List<ProductCatalogue> GetImportCatalogImageURLs()
        {  
            List<ProductCatalogue> objProductCatalogue = new List<ProductCatalogue>();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["CordobaEntities"].ConnectionString;

            con.Open();

            SqlCommand cmd = new SqlCommand("GetImportCatalogImageURLs", con);
            cmd.CommandType = CommandType.StoredProcedure;

            //cmd.Parameters.AddWithValue("@Supplier_Id", supplierId);
            //cmd.Parameters.AddWithValue("@Catalogue_Id", catalogueId);

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ProductCatalogue recordProductCatalog = new ProductCatalogue();
                    recordProductCatalog.code = Convert.ToString(dr["code"]);
                    recordProductCatalog.image_full = Convert.ToString(dr["image_full"]);
                    recordProductCatalog.image_full_url = Convert.ToString(dr["image_full_url"]);
                    recordProductCatalog.IsOperationCompleted = Convert.ToBoolean(dr["IsOperationCompleted"] == null ? false : true);
                    recordProductCatalog.product_id = Convert.ToInt32(dr["product_id"]);
                    recordProductCatalog.CatalogueName = Convert.ToString(dr["CatalogueName"]);
                    objProductCatalogue.Add(recordProductCatalog);
                }
            }
            DownloadCatalogueImages(objProductCatalogue);
            ProductActive_CatalogImport();
            return objProductCatalogue;
            //return lstOfImageUrls;
        }

        public void DownloadCatalogueImages(List<ProductCatalogue> productCatalogue)
        {
            try
            {
                for (int i = 0; i < productCatalogue.Count; i++)
                {
                    WebClient request = new WebClient();
                    if (productCatalogue[i].CatalogueName == null || productCatalogue[i].CatalogueName == "")
                    {
                        productCatalogue[i].CatalogueName = "Procurement Image Files";
                    }
                    string DirectoryPath = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["ImportProductImagePath"]) + "data//" + productCatalogue[i].CatalogueName;

                    if (!System.IO.Directory.Exists(DirectoryPath))
                    {
                        System.IO.Directory.CreateDirectory(DirectoryPath);
                    }

                    request.DownloadFile(new Uri(productCatalogue[i].image_full_url), DirectoryPath + "//" + productCatalogue[i].image_full);
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            

        }

        public void ProductActive_CatalogImport()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["CordobaEntities"].ConnectionString;
                con.Open();
                SqlCommand cmd = new SqlCommand("ProductActive_CatalogImport", con);
                cmd.CommandType = CommandType.StoredProcedure;
                int result = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                
                throw;
            }
           
        }
    }
}
