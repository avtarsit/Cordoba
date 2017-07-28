using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            //int supplierId = 27;
            //int catalogueId = 62;
            this.GetImportCatalogImageURLs();
        }

        protected override void OnStop()
        {
        }

        public List<ProductCatalogue> GetImportCatalogImageURLs()
        {
            //List<string> lstOfImageUrls = new List<string>();
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
            for (int i = 0; i < productCatalogue.Count; i++)
            {
                WebClient request = new WebClient();
                request.DownloadFile(new Uri(productCatalogue[i].image_full_url), @"D:\Projects\DUMMY\" + productCatalogue[i].image_full);
                //request.DownloadFile(new Uri(productCatalogue[i].image_full_url), @"D:\Project\CordobaGIT\CordobaWeb\image\data\ProductCatalogueImages\" + productCatalogue[i].image_full);
            }

        }

        public void ProductActive_CatalogImport()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["CordobaEntities"].ConnectionString;

            con.Open();

            SqlCommand cmd = new SqlCommand("ProductActive_CatalogImport", con);
            cmd.CommandType = CommandType.StoredProcedure;

            int result = cmd.ExecuteNonQuery();
        }
    }
}
