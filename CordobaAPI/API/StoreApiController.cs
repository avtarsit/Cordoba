using CordobaModels.Entities;
using CordobaServices.Interfaces;
using CordobaServices.Services;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Spire.Presentation;
using Spire.Presentation.Charts;
using Spire.Presentation.Drawing;
//using Spire.Presentation;
//using Spire.Presentation.Charts;
//using Spire.Presentation.Drawing;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
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
            //Spire Presentation DLL version 3.6.9
            //try
            //{
            //    var result = _StoreServices.GetStoreHTMLCharts(StoreID, Month, Year);

            //    Presentation ppt = new Presentation();
            //    ISlide slide = ppt.Slides[0];

            //    //First slide
            //    SizeF pptSize = ppt.SlideSize.Size;

            //    IAutoShape shape = (IAutoShape)ppt.Slides[0].Shapes.AppendShape(ShapeType.Rectangle, new RectangleF(10, 10, 700, 50));
            //    shape.Fill.FillType = FillFormatType.Solid;
            //    shape.Fill.SolidColor.Color = Color.Red;
            //    shape.ShapeStyle.LineColor.Color = Color.Red;

            //    //ppt.Slides[0].Shapes.ZOrder(1, shape);

            //    ////insert image to PPT
            //    //string ImageFile2 = HttpContext.Current.Server.MapPath("~/Content//images//logo//") + "red.png";
            //    //RectangleF fisrtsliderect = new RectangleF(10, 10, 600, 100);
            //    //ppt.Slides[0].Shapes.AppendEmbedImage(ShapeType.Rectangle, ImageFile2, fisrtsliderect);
            //    //ppt.Slides[0].Shapes[0].Line.FillFormat.SolidFillColor.Color = Color.FloralWhite;



            //    string logoFile = HttpContext.Current.Server.MapPath("~/Content//images//logo//") + "cordoba-logo.png";//"logo.png";
            //    //RectangleF logoRect = new RectangleF(pptSize.Width / 2 - 40, 60, 80, 80);
            //    RectangleF logoRect = new RectangleF(10, 70, 200, 78);
            //    IEmbedImage image = slide.Shapes.AppendEmbedImage(ShapeType.Rectangle, logoFile, logoRect);
            //    image.Line.FillType = FillFormatType.None;

            //    string logoFile1 = HttpContext.Current.Server.MapPath("~/Content//images//Storelogo//") + StoreID + ".png";//"logo.png";
            //    //RectangleF logoRect = new RectangleF(pptSize.Width / 2 - 40, 60, 80, 80);
            //    RectangleF logoRect1 = new RectangleF(500, 70, 200, 78);
            //    IEmbedImage image1 = slide.Shapes.AppendEmbedImage(ShapeType.Rectangle, logoFile1, logoRect1);
            //    image1.Line.FillType = FillFormatType.None;

            //    ////Add content
            //    //RectangleF textRect = new RectangleF(10, 200, 200, 130);
            //    //IAutoShape textShape = slide.Shapes.AppendShape(ShapeType.Rectangle, textRect);
            //    ////Content format
            //    //string text = "Review Meeting";
            //    //textShape.AppendTextFrame(text);
            //    RectangleF titleRect = new RectangleF(10, 150, 200, 130);
            //    IAutoShape titleShape = slide.Shapes.AppendShape(ShapeType.Rectangle, titleRect);
            //    titleShape.Fill.FillType = FillFormatType.None;
            //    titleShape.ShapeStyle.LineColor.Color = Color.Empty;
            //    TextParagraph titlePara = titleShape.TextFrame.Paragraphs[0];
            //    titlePara.Text = "Review Meeting";
            //    titlePara.FirstTextRange.FontHeight = 20;
            //    titlePara.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            //    titlePara.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            //    titlePara.Alignment = TextAlignmentType.Center;

            //    RectangleF titleRect1 = new RectangleF(10, 170, 200, 130);
            //    IAutoShape titleShape1 = slide.Shapes.AppendShape(ShapeType.Rectangle, titleRect1);
            //    titleShape1.Fill.FillType = FillFormatType.None;
            //    titleShape1.ShapeStyle.LineColor.Color = Color.Empty;
            //    TextParagraph titlePara1 = titleShape1.TextFrame.Paragraphs[0];
            //    string monthvalue = string.Empty;
            //    switch (Month)
            //    {
            //        case 1:
            //            monthvalue = "January";
            //            break;
            //        case 2:
            //            monthvalue = "February";
            //            break;
            //        case 3:
            //            monthvalue = "March";
            //            break;
            //        case 4:
            //            monthvalue = "April";
            //            break;
            //        case 5:
            //            monthvalue = "May";
            //            break;
            //        case 6:
            //            monthvalue = "June";
            //            break;
            //        case 7:
            //            monthvalue = "July";
            //            break;
            //        case 8:
            //            monthvalue = "August";
            //            break;
            //        case 9:
            //            monthvalue = "September";
            //            break;
            //        case 10:
            //            monthvalue = "October";
            //            break;
            //        case 11:
            //            monthvalue = "November";
            //            break;
            //        default:
            //            monthvalue = "December";
            //            break;

            //    }
            //    titlePara1.Text = string.Format("{0} {1}", monthvalue, "2018");
            //    titlePara1.FirstTextRange.FontHeight = 20;
            //    titlePara1.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            //    titlePara1.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            //    titlePara1.Alignment = TextAlignmentType.Center;

            //    string logoFile2 = HttpContext.Current.Server.MapPath("~/Content//images//logo//") + "homepdfimage.png";//"logo.png";
            //    //RectangleF logoRect = new RectangleF(pptSize.Width / 2 - 40, 60, 80, 80);
            //    RectangleF logoRect2 = new RectangleF(10, 310, 700, 200);
            //    IEmbedImage image2 = slide.Shapes.AppendEmbedImage(ShapeType.Rectangle, logoFile2, logoRect2);
            //    image1.Line.FillType = FillFormatType.None;

            //    //Set footer
            //    ppt.SetDateTimeVisible(false);
            //    ppt.SetSlideNoVisible(true);

            //    ////Set transition of slide
            //    //slide.SlideShowTransition.Type = TransitionType.Cover;
            //    //ISlide slide1 = ppt.Slides[0];
            //    //ppt.Slides.Append(slide);
            //    ppt.Slides.Append();

            //    //Second slide
            //    //RectangleF storerect = new RectangleF(10, 100, 550, 320);
            //    //IChart storechart = ppt.Slides[1].Shapes.AppendChart(ChartType.ColumnClustered, storerect);

            //    IAutoShape storeshape = (IAutoShape)ppt.Slides[1].Shapes.AppendShape(ShapeType.Rectangle, new RectangleF(10, 10, 700, 50));
            //    storeshape.Fill.FillType = FillFormatType.Solid;
            //    storeshape.Fill.SolidColor.Color = Color.Red;
            //    storeshape.ShapeStyle.LineColor.Color = Color.White;

            //    string storeFile = HttpContext.Current.Server.MapPath("~/Content//images//Storescreen//") + StoreID + ".png";//"logo.png";
            //    //RectangleF logoRect = new RectangleF(pptSize.Width / 2 - 40, 60, 80, 80);
            //    RectangleF storeRect = new RectangleF(10, 60, 600, 400);
            //    IEmbedImage storeimage = ppt.Slides[1].Shapes.AppendEmbedImage(ShapeType.Rectangle, storeFile, storeRect);
            //    storeimage.Line.FillType = FillFormatType.None;

            //    //Third slide
            //    ppt.Slides.Append();

            //    RectangleF dougnutrect = new RectangleF(10, 60, 300, 300);
            //    IChart chart = ppt.Slides[2].Shapes.AppendChart(ChartType.Doughnut, dougnutrect, false);
            //    chart.ChartTitle.TextProperties.Text = "Store Summary";
            //    chart.ChartTitle.TextProperties.IsCentered = true;
            //    chart.ChartTitle.Height = 30;

            //    RectangleF storesummarytitleRect = new RectangleF(10, 500, 200, 20);
            //    IAutoShape storesummarytitleShape = ppt.Slides[2].Shapes.AppendShape(ShapeType.Rectangle, titleRect);
            //    storesummarytitleShape.Fill.FillType = FillFormatType.None;
            //    storesummarytitleShape.ShapeStyle.LineColor.Color = Color.Empty;
            //    TextParagraph storesummarytitlePara = storesummarytitleShape.TextFrame.Paragraphs[0];
            //    storesummarytitlePara.Text = "Participant: (" + Convert.ToInt32(result.storeSummary.ToList()[0].Count + result.storeSummary.ToList()[1].Count) + ")";
            //    storesummarytitlePara.FirstTextRange.FontHeight = 20;
            //    storesummarytitlePara.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            //    storesummarytitlePara.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            //    storesummarytitlePara.Alignment = TextAlignmentType.Center;

            //    IAutoShape storesummaryshape = (IAutoShape)ppt.Slides[2].Shapes.AppendShape(ShapeType.Rectangle, new RectangleF(10, 10, 700, 50));
            //    storesummaryshape.Fill.FillType = FillFormatType.Solid;
            //    storesummaryshape.Fill.SolidColor.Color = Color.Red;
            //    storesummaryshape.ShapeStyle.LineColor.Color = Color.White;

            //    string[] countries = new string[] { "Active", "Not Active" };
            //    int[] sales = new int[] { result.storeSummary[0].Count, result.storeSummary[1].Count };
            //    chart.ChartData[0, 0].Text = "Countries";
            //    chart.ChartData[0, 1].Text = "Sales";
            //    for (int i = 0; i < countries.Length; ++i)
            //    {
            //        chart.ChartData[i + 1, 0].Value = countries[i];
            //        chart.ChartData[i + 1, 1].Value = sales[i];
            //    }
            //    chart.Series.SeriesLabel = chart.ChartData["B1", "B1"];
            //    chart.Categories.CategoryLabels = chart.ChartData["A2", "A5"];
            //    chart.Series[0].Values = chart.ChartData["B2", "B3"];
            //    //for (int i = 0; i < chart.Series[0].Values.Count; i++)
            //    for (int i = 0; i < 2; i++)
            //    {
            //        ChartDataPoint cdp = new ChartDataPoint(chart.Series[0]);
            //        cdp.Index = i;
            //        chart.Series[0].DataPoints.Add(cdp);
            //    }
            //    chart.Series[0].DataPoints[0].Fill.FillType = FillFormatType.Solid;
            //    chart.Series[0].DataPoints[0].Fill.SolidColor.Color = Color.LightBlue;
            //    chart.Series[0].DataPoints[1].Fill.FillType = FillFormatType.Solid;
            //    chart.Series[0].DataPoints[1].Fill.SolidColor.Color = Color.MediumPurple;
            //    chart.Series[0].DataLabels.LabelValueVisible = true;
            //    //chart.Series[0].DataLabels.PercentValueVisible = true;
            //    chart.Series[0].DoughnutHoleSize = 60;

            //    //Point Summary

            //    RectangleF pointsummaryrect = new RectangleF(400, 60, 300, 300);
            //    IChart pointsummarychart = ppt.Slides[2].Shapes.AppendChart(ChartType.Doughnut, pointsummaryrect, false);
            //    pointsummarychart.ChartTitle.TextProperties.Text = "Points Remaining";
            //    pointsummarychart.ChartTitle.TextProperties.IsCentered = true;
            //    pointsummarychart.ChartTitle.Height = 30;
            //    string[] pointsummarycountries = new string[] { "Activated accounts" };
            //    int[] pointsummarysales = new int[] { result.pointsRemaining[0].Count };
            //    pointsummarychart.ChartData[0, 0].Text = "Countries";
            //    pointsummarychart.ChartData[0, 1].Text = "Sales";
            //    for (int i = 0; i < pointsummarycountries.Length; ++i)
            //    {
            //        pointsummarychart.ChartData[i + 1, 0].Value = pointsummarycountries[i];
            //        pointsummarychart.ChartData[i + 1, 1].Value = pointsummarysales[i];
            //    }
                
            //    pointsummarychart.Series.SeriesLabel = pointsummarychart.ChartData["B1", "B1"];
            //    pointsummarychart.Categories.CategoryLabels = pointsummarychart.ChartData["A2", "A5"];
            //    pointsummarychart.Series[0].Values = pointsummarychart.ChartData["B2", "B2"];
            //    //for (int i = 0; i < chart.Series[0].Values.Count; i++)
            //    for (int i = 0; i < 2; i++)
            //    {
            //        ChartDataPoint cdp = new ChartDataPoint(chart.Series[0]);
            //        cdp.Index = i;
            //        pointsummarychart.Series[0].DataPoints.Add(cdp);
            //    }
            //    pointsummarychart.Series[0].DataPoints[0].Fill.FillType = FillFormatType.Solid;
            //    pointsummarychart.Series[0].DataPoints[0].Fill.SolidColor.Color = Color.LightBlue;
            //    pointsummarychart.Series[0].DataPoints[1].Fill.FillType = FillFormatType.Solid;
            //    pointsummarychart.Series[0].DataPoints[1].Fill.SolidColor.Color = Color.MediumPurple;
            //    pointsummarychart.Series[0].DataLabels.LabelValueVisible = true;
               
            //    pointsummarychart.Series[0].DoughnutHoleSize = 60;

            //    //Fourth slide
            //    ppt.Slides.Append();

            //    //insert a column chart  
            //    //insert a column participantloadedbymonthchart  
            //    RectangleF rect = new RectangleF(10, 100, 700, 420);
            //    IChart participantloadedbymonthchart = ppt.Slides[3].Shapes.AppendChart(ChartType.ColumnClustered, rect);

            //    IAutoShape participantloadedshape = (IAutoShape)ppt.Slides[3].Shapes.AppendShape(ShapeType.Rectangle, new RectangleF(10, 10, 700, 50));
            //    participantloadedshape.Fill.FillType = FillFormatType.Solid;
            //    participantloadedshape.Fill.SolidColor.Color = Color.Red;
            //    participantloadedshape.ShapeStyle.LineColor.Color = Color.White;

            //    //set chart title  
            //    participantloadedbymonthchart.ChartTitle.TextProperties.Text = "Participants loaded by month";
            //    participantloadedbymonthchart.ChartTitle.TextProperties.IsCentered = true;
            //    participantloadedbymonthchart.ChartTitle.Height = 30;
            //    participantloadedbymonthchart.HasTitle = true;

            //    //create a datatable  
            //    System.Data.DataTable dataTable1 = new DataTable();
            //    System.Data.DataTable dataTablenew = ToDataTable(result.participantsLoadedByMonth.ToList());
            //    //dataTable1 = dataTablenew.Copy();
            //    dataTable1.Columns.Add(new DataColumn("Month", Type.GetType("System.String")));
            //    dataTable1.Columns.Add(new DataColumn("CustomerCount", Type.GetType("System.Int32")));
            //    dataTable1.Columns.Add(new DataColumn("Chart", Type.GetType("System.Decimal")));

            //    //dataTable1.Rows.Clear();
            //    for (int i = 0; i < dataTablenew.Rows.Count; i++)
            //    {
            //        //dataTable1.Rows.Add("Customer" + i.ToString(), 0);
            //        dataTable1.ImportRow(dataTablenew.Rows[i]);
            //    }

            //    //import data from datatable to chart data  
            //    for (int c = 0; c < dataTable1.Columns.Count; c++)
            //    {
            //        participantloadedbymonthchart.ChartData[0, c].Text = dataTable1.Columns[c].Caption;
            //    }
            //    for (int r = 0; r < dataTable1.Rows.Count; r++)
            //    {
            //        object[] datas = dataTable1.Rows[r].ItemArray;
            //        for (int c = 0; c < datas.Length; c++)
            //        {
            //            participantloadedbymonthchart.ChartData[r + 1, c].Value = datas[c];

            //        }
            //    }

            //    //set series labels  
            //    participantloadedbymonthchart.Series.SeriesLabel = participantloadedbymonthchart.ChartData["B1", "C1"];

            //    int totalRows = dataTable1.Rows.Count + 1;

            //    //set categories labels      
            //    participantloadedbymonthchart.Categories.CategoryLabels = participantloadedbymonthchart.ChartData["A2", "A" + totalRows.ToString()];

            //    //assign data to series values  
            //    participantloadedbymonthchart.Series[0].Values = participantloadedbymonthchart.ChartData["B2", "B" + totalRows.ToString()];
            //    participantloadedbymonthchart.Series[1].Values = participantloadedbymonthchart.ChartData["C2", "C" + totalRows.ToString()];

            //    //change the chart type of series 2 to line chart with markers  
            //    participantloadedbymonthchart.Series[1].Type = ChartType.LineMarkers;

            //    ////plot data of series 2 on the secondary axis  
            //    //participantloadedbymonthchart.Series[1].UseSecondAxis = true;

            //    ////set the number format as percentage   
            //    //participantloadedbymonthchart.SecondaryValueAxis.NumberFormat = "0%";

            //    //hide grid lines of secondary axis  
            //    participantloadedbymonthchart.SecondaryValueAxis.MajorGridTextLines.FillType = FillFormatType.None;

            //    //set overlap  
            //    participantloadedbymonthchart.OverLap = -50;

            //    //set gap width  
            //    participantloadedbymonthchart.GapWidth = 200;

            //    //Fifth slide
            //    ppt.Slides.Append();

            //    RectangleF OrderPlacedByTypeByMonthrect = new RectangleF(10, 100, 700, 420);
            //    IAutoShape orderplacedbytypemonthshape = (IAutoShape)ppt.Slides[4].Shapes.AppendShape(ShapeType.Rectangle, new RectangleF(10, 10, 700, 50));
            //    orderplacedbytypemonthshape.Fill.FillType = FillFormatType.Solid;
            //    orderplacedbytypemonthshape.Fill.SolidColor.Color = Color.Red;
            //    orderplacedbytypemonthshape.ShapeStyle.LineColor.Color = Color.White;




            //    //create a datatable  
            //    System.Data.DataTable dtMakeOrderPlacedByTypeByMonth = new DataTable();
            //    System.Data.DataTable dataTableMakeOrderPlacedByTypeDynamic = ToDataTable(result.orderPlacedByType.ToList());

            //    if (dataTableMakeOrderPlacedByTypeDynamic != null && dataTableMakeOrderPlacedByTypeDynamic.Rows.Count > 0)
            //    {
            //        IChart OrderPlacedByTypeByMonthchart = ppt.Slides[4].Shapes.AppendChart(ChartType.ColumnClustered, OrderPlacedByTypeByMonthrect);


            //        //set chart title  
            //        OrderPlacedByTypeByMonthchart.ChartTitle.TextProperties.Text = "Order Placed By Type By Loaded By Month";
            //        OrderPlacedByTypeByMonthchart.ChartTitle.TextProperties.IsCentered = true;
            //        OrderPlacedByTypeByMonthchart.ChartTitle.Height = 30;
            //        OrderPlacedByTypeByMonthchart.HasTitle = true;

            //        dtMakeOrderPlacedByTypeByMonth.Columns.Add(new DataColumn("Name", Type.GetType("System.String")));
            //        dtMakeOrderPlacedByTypeByMonth.Columns.Add(new DataColumn("OrderCount", Type.GetType("System.Int32")));
            //        dtMakeOrderPlacedByTypeByMonth.Columns.Add(new DataColumn("Chart", Type.GetType("System.Decimal")));

            //        //dataTable1.Rows.Clear();
            //        for (int i = 0; i < dataTableMakeOrderPlacedByTypeDynamic.Rows.Count; i++)
            //        {
            //            //dataTable1.Rows.Add("Customer" + i.ToString(), 0);
            //            dtMakeOrderPlacedByTypeByMonth.ImportRow(dataTableMakeOrderPlacedByTypeDynamic.Rows[i]);
            //        }

            //        //import data from datatable to chart data  
            //        for (int c = 0; c < dtMakeOrderPlacedByTypeByMonth.Columns.Count; c++)
            //        {
            //            OrderPlacedByTypeByMonthchart.ChartData[0, c].Text = dtMakeOrderPlacedByTypeByMonth.Columns[c].Caption;
            //        }
            //        for (int r = 0; r < dtMakeOrderPlacedByTypeByMonth.Rows.Count; r++)
            //        {
            //            object[] datas = dtMakeOrderPlacedByTypeByMonth.Rows[r].ItemArray;
            //            for (int c = 0; c < datas.Length; c++)
            //            {
            //                OrderPlacedByTypeByMonthchart.ChartData[r + 1, c].Value = datas[c];

            //            }
            //        }

            //        //set series labels  
            //        OrderPlacedByTypeByMonthchart.Series.SeriesLabel = OrderPlacedByTypeByMonthchart.ChartData["B1", "C1"];

            //        int totalRowsOrderPlacedByTypeByMonthchart = dtMakeOrderPlacedByTypeByMonth.Rows.Count + 1;

            //        //set categories labels      
            //        OrderPlacedByTypeByMonthchart.Categories.CategoryLabels = OrderPlacedByTypeByMonthchart.ChartData["A2", "A" + totalRowsOrderPlacedByTypeByMonthchart.ToString()];

            //        //assign data to series values  
            //        OrderPlacedByTypeByMonthchart.Series[0].Values = OrderPlacedByTypeByMonthchart.ChartData["B2", "B" + totalRowsOrderPlacedByTypeByMonthchart.ToString()];
            //        OrderPlacedByTypeByMonthchart.Series[1].Values = OrderPlacedByTypeByMonthchart.ChartData["C2", "C" + totalRowsOrderPlacedByTypeByMonthchart.ToString()];

            //        //change the chart type of series 2 to line chart with markers  
            //        OrderPlacedByTypeByMonthchart.Series[1].Type = ChartType.LineMarkers;

            //        ////plot data of series 2 on the secondary axis  
            //        //OrderPlacedByTypeByMonthchart.Series[1].UseSecondAxis = true;

            //        ////set the number format as percentage   
            //        //OrderPlacedByTypeByMonthchart.SecondaryValueAxis.NumberFormat = "0%";

            //        //hide grid lines of secondary axis  
            //        OrderPlacedByTypeByMonthchart.SecondaryValueAxis.MajorGridTextLines.FillType = FillFormatType.None;

            //        //set overlap  
            //        OrderPlacedByTypeByMonthchart.OverLap = -50;

            //        //set gap width  
            //        OrderPlacedByTypeByMonthchart.GapWidth = 200;
            //    }
            //    else
            //    {
            //        RectangleF OrderPlacedByTypeByMonthtitleRect1 = new RectangleF(60, 10, 500, 130);
            //        IAutoShape OrderPlacedByTypeByMonthtitleShape1 = ppt.Slides[4].Shapes.AppendShape(ShapeType.Rectangle, OrderPlacedByTypeByMonthtitleRect1);
            //        OrderPlacedByTypeByMonthtitleShape1.Fill.FillType = FillFormatType.None;
            //        OrderPlacedByTypeByMonthtitleShape1.ShapeStyle.LineColor.Color = Color.Empty;
            //        TextParagraph OrderPlacedByTypeByMonthtitlePara1 = OrderPlacedByTypeByMonthtitleShape1.TextFrame.Paragraphs[0];
            //        OrderPlacedByTypeByMonthtitlePara1.Text = "Order Placed By Type By Loaded By Month";
            //        OrderPlacedByTypeByMonthtitlePara1.FirstTextRange.FontHeight = 20;
            //        OrderPlacedByTypeByMonthtitlePara1.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            //        OrderPlacedByTypeByMonthtitlePara1.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            //        OrderPlacedByTypeByMonthtitlePara1.Alignment = TextAlignmentType.Center;

            //        RectangleF OrderPlacedByTypeByMonthtitleRect = new RectangleF(60, 100, 500, 130);
            //        IAutoShape OrderPlacedByTypeByMonthtitleShape = ppt.Slides[4].Shapes.AppendShape(ShapeType.Rectangle, OrderPlacedByTypeByMonthtitleRect);
            //        OrderPlacedByTypeByMonthtitleShape.Fill.FillType = FillFormatType.None;
            //        OrderPlacedByTypeByMonthtitleShape.ShapeStyle.LineColor.Color = Color.Empty;
            //        TextParagraph OrderPlacedByTypeByMonthtitlePara = OrderPlacedByTypeByMonthtitleShape.TextFrame.Paragraphs[0];
            //        OrderPlacedByTypeByMonthtitlePara.Text = "No Orders Available.";
            //        OrderPlacedByTypeByMonthtitlePara.FirstTextRange.FontHeight = 20;
            //        OrderPlacedByTypeByMonthtitlePara.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            //        OrderPlacedByTypeByMonthtitlePara.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            //        OrderPlacedByTypeByMonthtitlePara.Alignment = TextAlignmentType.Center;
            //    }


            //    //Sixth slide
            //    ppt.Slides.Append();
            //    //TopPointsHoldersByStore[] pointhoders = result.topPointsHolders.ToList().ToArray();
            //    DataTable dtpointhoders = ToDataTable(result.topPointsHolders.ToList());

            //    IAutoShape pointhodersshape = (IAutoShape)ppt.Slides[5].Shapes.AppendShape(ShapeType.Rectangle, new RectangleF(10, 10, 700, 50));
            //    pointhodersshape.Fill.FillType = FillFormatType.Solid;
            //    pointhodersshape.Fill.SolidColor.Color = Color.Red;
            //    pointhodersshape.ShapeStyle.LineColor.Color = Color.White;

            //    Double[] widths = new double[] { 100, 100, 100, 100, 100, 100 };
            //    Double[] heights = new double[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
            //    ITable table = ppt.Slides[5].Shapes.AppendTable(ppt.SlideSize.Size.Width / 2 - 275, 80, widths, heights);
            //    //set the style of table
            //    table.StylePreset = TableStylePreset.LightStyle1Accent2;


            //    //    String[,] dataStr = new String[,]{
            //    //{"Name",    "Capital",  "Continent",    "Area", "Population"},
            //    //{"Venezuela",   "Caracas",  "South America",    "912047",   "19700000"},
            //    //{"Bolivia", "La Paz",   "South America",    "1098575",  "7300000"},
            //    //{"Brazil",  "Brasilia", "South America",    "8511196",  "150400000"},
            //    //{"Canada",  "Ottawa",   "North America",    "9976147",  "26500000"},
            //    //{"Chile",   "Santiago", "South America",    "756943",   "13200000"},
            //    //{"Colombia",    "Bagota",   "South America",    "1138907",  "33000000"},
            //    //{"Cuba",    "Havana",   "North America",    "114524",   "10600000"},
            //    //{"Ecuador", "Quito",    "South America",    "455502",   "10600000"},
            //    //{"Paraguay",    "Asuncion","South America", "406576",   "4660000"},
            //    //{"Peru",    "Lima", "South America",    "1285215",  "21600000"},
            //    //{"Jamaica", "Kingston", "North America",    "11424",    "2500000"},
            //    //{"Mexico",  "Mexico City",  "North America",    "1967180",  "88600000"}
            //    //};

            //    //for (int i = 0; i < 13; i++)
            //    //    for (int j = 0; j < 5; j++)
            //    //    {
            //    //        //fill the table with data
            //    //        table[j, i].TextFrame.Text = dataStr[i, j];
            //    //        //set the Font
            //    //        table[j, i].TextFrame.Paragraphs[0].TextRanges[0].LatinFont = new TextFont("Arial Narrow");
            //    //    }

            //    for (int i = 0; i < dtpointhoders.Columns.Count; i++)
            //    {
            //        table[i, 0].TextFrame.Text = dtpointhoders.Columns[i].ColumnName.ToString();// dataStr[i, j];
            //    }

            //    for (int i = 0; i < dtpointhoders.Rows.Count; i++)
            //        for (int j = 0; j < dtpointhoders.Columns.Count; j++)
            //        {
            //            //fill the table with data
            //            table[j, i + 1].TextFrame.Text = dtpointhoders.Rows[i][j].ToString();// dataStr[i, j];
            //            //set the Font
            //            table[j, i + 1].TextFrame.Paragraphs[0].TextRanges[0].LatinFont = new TextFont("Arial Narrow");
            //            table[j, i + 1].TextFrame.Paragraphs[0].TextRanges[0].FontHeight = 10;
            //        }

            //    //set the alignment of the first row to Center
            //    for (int i = 0; i < 6; i++)
            //    {
            //        table[i, 0].TextFrame.Paragraphs[0].Alignment = TextAlignmentType.Center;
            //    }

            //    //Seventh slide
            //    //Points Loaded By Month
            //    ppt.Slides.Append();



            //    ppt.SlideSize.Type = SlideSizeType.Screen16x9;
            //    SizeF slidesize = ppt.SlideSize.Size;

            //    IAutoShape pointsloadedbymonthshape = (IAutoShape)ppt.Slides[6].Shapes.AppendShape(ShapeType.Rectangle, new RectangleF(10, 10, 700, 50));
            //    pointsloadedbymonthshape.Fill.FillType = FillFormatType.Solid;
            //    pointsloadedbymonthshape.Fill.SolidColor.Color = Color.Red;
            //    pointsloadedbymonthshape.ShapeStyle.LineColor.Color = Color.White;

            //    var pointsloadedbymonthslide = ppt.Slides[6];

            //    //Add title
            //    RectangleF pointsloadedbymonthtitleRect = new RectangleF(10, 60, 250, 50);
            //    IAutoShape pointsloadedbymonthtitleShape = pointsloadedbymonthslide.Shapes.AppendShape(ShapeType.Rectangle, pointsloadedbymonthtitleRect);
            //    pointsloadedbymonthtitleShape.Fill.FillType = FillFormatType.None;
            //    pointsloadedbymonthtitleShape.ShapeStyle.LineColor.Color = Color.Empty;
            //    TextParagraph pointsloadedbymonthtitlePara = pointsloadedbymonthtitleShape.TextFrame.Paragraphs[0];
            //    pointsloadedbymonthtitlePara.Text = "Points Loaded By Month";
            //    pointsloadedbymonthtitlePara.FirstTextRange.FontHeight = 20;
            //    pointsloadedbymonthtitlePara.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            //    pointsloadedbymonthtitlePara.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            //    pointsloadedbymonthtitlePara.Alignment = TextAlignmentType.Center;

            //    RectangleF pointsloadedbymonthrect = new RectangleF(60, 100, slidesize.Width - 40, slidesize.Height - 100);
            //    IChart pointsloadedbymonthchart = pointsloadedbymonthslide.Shapes.AppendChart(Spire.Presentation.Charts.ChartType.BarStacked, pointsloadedbymonthrect);

            //    string[] columnlabels = { "Points Loaded By Month" };

            //    DataTable dtpointsloadedbymonth = ToDataTable(result.pointsLoadedByMonth.ToList());

            //    String[] cols = columnlabels.ToArray();
            //    for (Int32 c = 0; c < dtpointsloadedbymonth.Columns.Count; ++c)
            //        pointsloadedbymonthchart.ChartData[0, c + 1].Text = dtpointsloadedbymonth.Columns[c].ToString();

            //    string[] rowlabels = new string[dtpointsloadedbymonth.Rows.Count];

            //    for (int i = 0; i < dtpointsloadedbymonth.Rows.Count; i++)
            //    {
            //        rowlabels[i] = dtpointsloadedbymonth.Rows[i][0].ToString();
            //    }

            //    String[] rows = rowlabels.ToArray();
            //    for (Int32 r = 0; r < rows.Count(); ++r)
            //        pointsloadedbymonthchart.ChartData[r + 1, 0].Text = rows[r];

            //    string[,] values = new string[dtpointsloadedbymonth.Rows.Count, 1];

            //    for (int i = 0; i < dtpointsloadedbymonth.Rows.Count; i++)
            //    {
            //        values[i, 0] = dtpointsloadedbymonth.Rows[i][1].ToString();
            //    }

            //    double value = 0.0;
            //    for (Int32 r = 0; r < rows.Count(); ++r)
            //    {
            //        for (Int32 c = 0; c < cols.Count(); ++c)
            //        {
            //            value = Math.Round(Convert.ToDouble(values[r, c]), 2);
            //            pointsloadedbymonthchart.ChartData[r + 1, c + 1].Value = value;
            //        }
            //    }

            //    pointsloadedbymonthchart.Series.SeriesLabel = pointsloadedbymonthchart.ChartData[0, 1, 0, columnlabels.Count()];
            //    pointsloadedbymonthchart.Categories.CategoryLabels = pointsloadedbymonthchart.ChartData[1, 0, rowlabels.Count(), 0];

            //    pointsloadedbymonthchart.PrimaryCategoryAxis.Position = AxisPositionType.Left;
            //    pointsloadedbymonthchart.SecondaryCategoryAxis.Position = AxisPositionType.Left;
            //    pointsloadedbymonthchart.PrimaryCategoryAxis.TickLabelPosition = TickLabelPositionType.TickLabelPositionLow;

            //    for (Int32 c = 0; c < cols.Count(); ++c)
            //    {
            //        pointsloadedbymonthchart.Series[c].Values = pointsloadedbymonthchart.ChartData[1, c + 1, rowlabels.Count(), c + 1];
            //        pointsloadedbymonthchart.Series[c].Fill.FillType = FillFormatType.Solid;
            //        pointsloadedbymonthchart.Series[c].InvertIfNegative = true;
            //        pointsloadedbymonthchart.Series[c].Fill.SolidColor.Color = Color.LightBlue;

            //        for (Int32 r = 0; r < rows.Count(); ++r)
            //        {
            //            var label = pointsloadedbymonthchart.Series[c].DataLabels.Add();
            //            label.LabelValueVisible = true;
            //            label.Position = ChartDataLabelPosition.InsideEnd;
            //            pointsloadedbymonthchart.Series[c].DataLabels[r].HasDataSource = false;
            //            pointsloadedbymonthchart.Series[c].DataLabels.TextProperties.Paragraphs[0].DefaultCharacterProperties.FontHeight = 12;
            //        }
            //    }
            //    pointsloadedbymonthchart.Series[0].Fill.SolidColor.Color = Color.LightBlue;
            //    //chart.Series[1].Fill.SolidColor.Color = Color.Red;
            //    //chart.Series[2].Fill.SolidColor.Color = Color.Green;

            //    TextFont font = new TextFont("Tw Cen MT");

            //    for (int k = 0; k < pointsloadedbymonthchart.ChartLegend.EntryTextProperties.Length; k++)
            //    {
            //        pointsloadedbymonthchart.ChartLegend.EntryTextProperties[k].LatinFont = font;
            //        pointsloadedbymonthchart.ChartLegend.EntryTextProperties[k].FontHeight = 10;
            //    }




            //    //Eightth slide
            //    //Points Redeemed By Month
            //    ppt.Slides.Append();
            //    ppt.SlideSize.Type = SlideSizeType.Screen16x9;
            //    SizeF pointsredeemedslidesize = ppt.SlideSize.Size;

            //    var pointsredeemedslide = ppt.Slides[7];

            //    IAutoShape pointsredeemedshape = (IAutoShape)ppt.Slides[7].Shapes.AppendShape(ShapeType.Rectangle, new RectangleF(10, 10, 700, 50));
            //    pointsredeemedshape.Fill.FillType = FillFormatType.Solid;
            //    pointsredeemedshape.Fill.SolidColor.Color = Color.Red;
            //    pointsredeemedshape.ShapeStyle.LineColor.Color = Color.White;

            //    //Add title
            //    RectangleF pointsredeemedtitleRect = new RectangleF(10, 60, 250, 50);
            //    IAutoShape pointsredeemedtitleShape = pointsredeemedslide.Shapes.AppendShape(ShapeType.Rectangle, pointsredeemedtitleRect);
            //    pointsredeemedtitleShape.Fill.FillType = FillFormatType.None;
            //    pointsredeemedtitleShape.ShapeStyle.LineColor.Color = Color.Empty;
            //    TextParagraph pointsredeemedtitlePara = pointsredeemedtitleShape.TextFrame.Paragraphs[0];
            //    pointsredeemedtitlePara.Text = "Points Reedemed By Month";
            //    pointsredeemedtitlePara.FirstTextRange.FontHeight = 20;
            //    pointsredeemedtitlePara.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            //    pointsredeemedtitlePara.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            //    pointsredeemedtitlePara.Alignment = TextAlignmentType.Center;

            //    RectangleF pointsredeemedrect = new RectangleF(60, 100, pointsredeemedslidesize.Width - 40, pointsredeemedslidesize.Height - 100);
            //    IChart pointsredeemedchart = pointsredeemedslide.Shapes.AppendChart(Spire.Presentation.Charts.ChartType.BarStacked, pointsredeemedrect);

            //    string[] pointsredeemedcolumnlabels = { "Points Reedemed By Month" };

            //    DataTable dtpointsredeemed = ToDataTable(result.pointsRedeemedByMonth.ToList());

            //    String[] pointsredeemedcols = pointsredeemedcolumnlabels.ToArray();
            //    for (Int32 c = 0; c < dtpointsredeemed.Columns.Count; ++c)
            //        pointsredeemedchart.ChartData[0, c + 1].Text = dtpointsredeemed.Columns[c].ToString();

            //    string[] pointsredeemedrowlabels = new string[dtpointsredeemed.Rows.Count];

            //    for (int i = 0; i < dtpointsredeemed.Rows.Count; i++)
            //    {
            //        pointsredeemedrowlabels[i] = dtpointsredeemed.Rows[i][0].ToString();
            //    }

            //    String[] pointsredeemedrows = pointsredeemedrowlabels.ToArray();
            //    for (Int32 r = 0; r < pointsredeemedrows.Count(); ++r)
            //        pointsredeemedchart.ChartData[r + 1, 0].Text = pointsredeemedrows[r];

            //    string[,] pointsredeemedvalues = new string[dtpointsredeemed.Rows.Count, 1];

            //    for (int i = 0; i < dtpointsredeemed.Rows.Count; i++)
            //    {
            //        pointsredeemedvalues[i, 0] = dtpointsredeemed.Rows[i][1].ToString();
            //    }

            //    double pointsredeemedvalue = 0.0;
            //    for (Int32 r = 0; r < pointsredeemedrows.Count(); ++r)
            //    {
            //        for (Int32 c = 0; c < pointsredeemedcols.Count(); ++c)
            //        {
            //            pointsredeemedvalue = Math.Round(Convert.ToDouble(pointsredeemedvalues[r, c]), 2);
            //            pointsredeemedchart.ChartData[r + 1, c + 1].Value = pointsredeemedvalue;
            //        }
            //    }

            //    pointsredeemedchart.Series.SeriesLabel = pointsredeemedchart.ChartData[0, 1, 0, columnlabels.Count()];
            //    pointsredeemedchart.Categories.CategoryLabels = pointsredeemedchart.ChartData[1, 0, rowlabels.Count(), 0];

            //    pointsredeemedchart.PrimaryCategoryAxis.Position = AxisPositionType.Left;
            //    pointsredeemedchart.SecondaryCategoryAxis.Position = AxisPositionType.Left;
            //    pointsredeemedchart.PrimaryCategoryAxis.TickLabelPosition = TickLabelPositionType.TickLabelPositionLow;

            //    for (Int32 c = 0; c < pointsredeemedcols.Count(); ++c)
            //    {
            //        pointsredeemedchart.Series[c].Values = pointsredeemedchart.ChartData[1, c + 1, rowlabels.Count(), c + 1];
            //        pointsredeemedchart.Series[c].Fill.FillType = FillFormatType.Solid;
            //        pointsredeemedchart.Series[c].InvertIfNegative = true;
            //        pointsredeemedchart.Series[c].Fill.SolidColor.Color = Color.LightBlue;

            //        for (Int32 r = 0; r < pointsredeemedrows.Count(); ++r)
            //        {
            //            var pointsredeemedlabel = pointsredeemedchart.Series[c].DataLabels.Add();
            //            pointsredeemedlabel.LabelValueVisible = true;
            //            pointsredeemedlabel.Position = ChartDataLabelPosition.InsideEnd;
            //            pointsredeemedchart.Series[c].DataLabels[r].HasDataSource = false;
            //            pointsredeemedchart.Series[c].DataLabels.TextProperties.Paragraphs[0].DefaultCharacterProperties.FontHeight = 12;
            //        }
            //    }
            //    pointsredeemedchart.Series[0].Fill.SolidColor.Color = Color.LightBlue;
            //    //chart.Series[1].Fill.SolidColor.Color = Color.Red;
            //    //chart.Series[2].Fill.SolidColor.Color = Color.Green;

            //    TextFont pointsredeemedfont = new TextFont("Tw Cen MT");

            //    for (int k = 0; k < pointsredeemedchart.ChartLegend.EntryTextProperties.Length; k++)
            //    {
            //        pointsredeemedchart.ChartLegend.EntryTextProperties[k].LatinFont = font;
            //        pointsredeemedchart.ChartLegend.EntryTextProperties[k].FontHeight = 10;
            //    }


            //    //save to file  
            //    ppt.SaveToFile(HttpContext.Current.Server.MapPath("~/Files//") + "Monthly Report.pptx", FileFormat.Pptx2010);
            //    //presentation.SaveToFile(HttpContext.Current.Server.MapPath("~/Files//") + "CombinationChart.pptx", FileFormat.Pptx2010);

            //    HttpResponseMessage response = Request.CreateResponse();

            //    string filePath = HttpContext.Current.Server.MapPath("~/Files//") + "Monthly Report.pptx";
            //    FileInfo fileInfo = new FileInfo(filePath);

            //    HttpContext.Current.Response.Clear();
            //    HttpContext.Current.Response.ContentType = GetMimeType("Monthly Report.pptx");
            //    HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=Monthly Report.pptx");
            //    HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //    HttpContext.Current.Response.WriteFile(HttpContext.Current.Server.MapPath("~/Files//") + "Monthly Report.pptx");

            //    System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
            //    if (result != null)
            //    {
            //        return Request.CreateResponse(HttpStatusCode.OK, result);
            //    }
            //    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong? Please try again later.");
            //}

            //Spire Presentation DLL version 3.3.0
            try
            {
                var result = _StoreServices.GetStoreHTMLCharts(StoreID, Month, Year);

                Presentation ppt = new Presentation();
                ISlide slide = ppt.Slides[0];

                //First slide
                SizeF pptSize = ppt.SlideSize.Size;

                //IAutoShape shape = (IAutoShape)ppt.Slides[0].Shapes.AppendShape(ShapeType.Rectangle, new RectangleF(10, 10, 700, 50));
                //shape.Fill.FillType = FillFormatType.Solid;
                //shape.Fill.SolidColor.Color = Color.Red;
                //shape.ShapeStyle.LineColor.Color = Color.Red;

                //ppt.Slides[0].Shapes.ZOrder(1, shape);

                ////insert image to PPT
                //string ImageFile2 = HttpContext.Current.Server.MapPath("~/Content//images//logo//") + "red.png";
                //RectangleF fisrtsliderect = new RectangleF(10, 10, 600, 100);
                //ppt.Slides[0].Shapes.AppendEmbedImage(ShapeType.Rectangle, ImageFile2, fisrtsliderect);
                //ppt.Slides[0].Shapes[0].Line.FillFormat.SolidFillColor.Color = Color.FloralWhite;



                string logoFile = HttpContext.Current.Server.MapPath("~/Content//images//logo//") + "cordoba-logo.png";//"logo.png";
                //RectangleF logoRect = new RectangleF(pptSize.Width / 2 - 40, 60, 80, 80);
                RectangleF logoRect = new RectangleF(10, 20, 200, 78);
                IEmbedImage image = slide.Shapes.AppendEmbedImage(ShapeType.Rectangle, logoFile, logoRect);
                image.Line.FillType = FillFormatType.None;

                string logoFile1 = HttpContext.Current.Server.MapPath("~/Content//images//Storelogo//") + StoreID + ".png";//"logo.png";
                //RectangleF logoRect = new RectangleF(pptSize.Width / 2 - 40, 60, 80, 80);
                RectangleF logoRect1 = new RectangleF(500, 20, 200, 78);
                IEmbedImage image1 = slide.Shapes.AppendEmbedImage(ShapeType.Rectangle, logoFile1, logoRect1);
                image1.Line.FillType = FillFormatType.None;

                ////Add content
                //RectangleF textRect = new RectangleF(10, 200, 200, 130);
                //IAutoShape textShape = slide.Shapes.AppendShape(ShapeType.Rectangle, textRect);
                ////Content format
                //string text = "Review Meeting";
                //textShape.AppendTextFrame(text);
                RectangleF titleRect = new RectangleF(10, 100, 200, 60);
                IAutoShape titleShape = slide.Shapes.AppendShape(ShapeType.Rectangle, titleRect);
                titleShape.Fill.FillType = FillFormatType.None;
                titleShape.ShapeStyle.LineColor.Color = Color.Empty;
                TextParagraph titlePara = titleShape.TextFrame.Paragraphs[0];
                titlePara.Text = "Review Meeting";
                titlePara.FirstTextRange.FontHeight = 20;
                titlePara.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                titlePara.FirstTextRange.Fill.SolidColor.Color = Color.Black;
                titlePara.Alignment = TextAlignmentType.Center;

                RectangleF titleRect1 = new RectangleF(10, 120, 200, 60);
                IAutoShape titleShape1 = slide.Shapes.AppendShape(ShapeType.Rectangle, titleRect1);
                titleShape1.Fill.FillType = FillFormatType.None;
                titleShape1.ShapeStyle.LineColor.Color = Color.Empty;
                TextParagraph titlePara1 = titleShape1.TextFrame.Paragraphs[0];
                string monthvalue = string.Empty;
                switch (Month)
                {
                    case 1:
                        monthvalue = "January";
                        break;
                    case 2:
                        monthvalue = "February";
                        break;
                    case 3:
                        monthvalue = "March";
                        break;
                    case 4:
                        monthvalue = "April";
                        break;
                    case 5:
                        monthvalue = "May";
                        break;
                    case 6:
                        monthvalue = "June";
                        break;
                    case 7:
                        monthvalue = "July";
                        break;
                    case 8:
                        monthvalue = "August";
                        break;
                    case 9:
                        monthvalue = "September";
                        break;
                    case 10:
                        monthvalue = "October";
                        break;
                    case 11:
                        monthvalue = "November";
                        break;
                    default:
                        monthvalue = "December";
                        break;

                }
                titlePara1.Text = string.Format("{0} {1}", monthvalue, "2018");
                titlePara1.FirstTextRange.FontHeight = 20;
                titlePara1.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                titlePara1.FirstTextRange.Fill.SolidColor.Color = Color.Black;
                titlePara1.Alignment = TextAlignmentType.Center;

                string logoFile2 = HttpContext.Current.Server.MapPath("~/Content//images//logo//") + "homepdfimage.png";//"logo.png";
                //RectangleF logoRect = new RectangleF(pptSize.Width / 2 - 40, 60, 80, 80);
                RectangleF logoRect2 = new RectangleF(10, 170, 700, 160);
                IEmbedImage image2 = slide.Shapes.AppendEmbedImage(ShapeType.Rectangle, logoFile2, logoRect2);
                image1.Line.FillType = FillFormatType.None;

                //Set footer
                ppt.SetDateTimeVisible(false);
                ppt.SetSlideNoVisible(true);

                ////Set transition of slide
                //slide.SlideShowTransition.Type = TransitionType.Cover;
                //ISlide slide1 = ppt.Slides[0];
                //ppt.Slides.Append(slide);
                ppt.Slides.Append();

                //Second slide
                //RectangleF storerect = new RectangleF(10, 100, 550, 320);
                //IChart storechart = ppt.Slides[1].Shapes.AppendChart(ChartType.ColumnClustered, storerect);

                //IAutoShape storeshape = (IAutoShape)ppt.Slides[1].Shapes.AppendShape(ShapeType.Rectangle, new RectangleF(10, 10, 700, 50));
                //storeshape.Fill.FillType = FillFormatType.Solid;
                //storeshape.Fill.SolidColor.Color = Color.Red;
                //storeshape.ShapeStyle.LineColor.Color = Color.White;

                string storeFile = HttpContext.Current.Server.MapPath("~/Content//images//Storescreen//") + StoreID + ".png";//"logo.png";
                //RectangleF logoRect = new RectangleF(pptSize.Width / 2 - 40, 60, 80, 80);
                RectangleF storeRect = new RectangleF(10, 20, 600, 380);
                IEmbedImage storeimage = ppt.Slides[1].Shapes.AppendEmbedImage(ShapeType.Rectangle, storeFile, storeRect);
                storeimage.Line.FillType = FillFormatType.None;

                //Third slide
                ppt.Slides.Append();

                RectangleF dougnutrect = new RectangleF(10, 20, 300, 300);
                IChart chart = ppt.Slides[2].Shapes.AppendChart(ChartType.Doughnut, dougnutrect, false);
                chart.ChartTitle.TextProperties.Text = "Store Summary";
                chart.ChartTitle.TextProperties.IsCentered = true;
                chart.ChartTitle.Height = 30;

                RectangleF storesummarytitleRect = new RectangleF(10, 380, 200, 20);
                IAutoShape storesummarytitleShape = ppt.Slides[2].Shapes.AppendShape(ShapeType.Rectangle, titleRect);
                storesummarytitleShape.Fill.FillType = FillFormatType.None;
                storesummarytitleShape.ShapeStyle.LineColor.Color = Color.Empty;
                TextParagraph storesummarytitlePara = storesummarytitleShape.TextFrame.Paragraphs[0];
                storesummarytitlePara.Text = "Participant: (" + Convert.ToInt32(result.storeSummary.ToList()[0].Count + result.storeSummary.ToList()[1].Count) + ")";
                storesummarytitlePara.FirstTextRange.FontHeight = 20;
                storesummarytitlePara.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                storesummarytitlePara.FirstTextRange.Fill.SolidColor.Color = Color.Black;
                storesummarytitlePara.Alignment = TextAlignmentType.Center;

                //IAutoShape storesummaryshape = (IAutoShape)ppt.Slides[2].Shapes.AppendShape(ShapeType.Rectangle, new RectangleF(10, 10, 700, 50));
                //storesummaryshape.Fill.FillType = FillFormatType.Solid;
                //storesummaryshape.Fill.SolidColor.Color = Color.Red;
                //storesummaryshape.ShapeStyle.LineColor.Color = Color.White;

                string[] countries = new string[] { "Active", "Not Active" };
                int[] sales = new int[] { result.storeSummary[0].Count, result.storeSummary[1].Count };
                chart.ChartData[0, 0].Text = "Countries";
                chart.ChartData[0, 1].Text = "Sales";
                for (int i = 0; i < countries.Length; ++i)
                {
                    chart.ChartData[i + 1, 0].Value = countries[i];
                    chart.ChartData[i + 1, 1].Value = sales[i];
                }
                chart.Series.SeriesLabel = chart.ChartData["B1", "B1"];
                chart.Categories.CategoryLabels = chart.ChartData["A2", "A3"];
                chart.Series[0].Values = chart.ChartData["B2", "B3"];
                //for (int i = 0; i < chart.Series[0].Values.Count; i++)
                for (int i = 0; i < 2; i++)
                {
                    ChartDataPoint cdp = new ChartDataPoint(chart.Series[0]);
                    cdp.Index = i;
                    chart.Series[0].DataPoints.Add(cdp);
                }
                chart.Series[0].DataPoints[0].Fill.FillType = FillFormatType.Solid;
                chart.Series[0].DataPoints[0].Fill.SolidColor.Color = Color.LightBlue;
                chart.Series[0].DataPoints[1].Fill.FillType = FillFormatType.Solid;
                chart.Series[0].DataPoints[1].Fill.SolidColor.Color = Color.MediumPurple;
                chart.Series[0].DataLabels.LabelValueVisible = true;
                //chart.Series[0].DataLabels.PercentValueVisible = true;
                //chart.Series[0].DoughnutHoleSize = 60;

                //Point Summary

                RectangleF pointsummaryrect = new RectangleF(400, 20, 300, 300);
                IChart pointsummarychart = ppt.Slides[2].Shapes.AppendChart(ChartType.Doughnut, pointsummaryrect, false);
                pointsummarychart.ChartTitle.TextProperties.Text = "Points Remaining";
                pointsummarychart.ChartTitle.TextProperties.IsCentered = true;
                pointsummarychart.ChartTitle.Height = 30;
                string[] pointsummarycountries = new string[] { "Activated accounts" };
                int[] pointsummarysales = new int[] { result.pointsRemaining[0].Count };
                pointsummarychart.ChartData[0, 0].Text = "Countries";
                pointsummarychart.ChartData[0, 1].Text = "Sales";
                for (int i = 0; i < pointsummarycountries.Length; ++i)
                {
                    pointsummarychart.ChartData[i + 1, 0].Value = pointsummarycountries[i];
                    pointsummarychart.ChartData[i + 1, 1].Value = pointsummarysales[i];
                }

                pointsummarychart.Series.SeriesLabel = pointsummarychart.ChartData["B1", "B1"];
                pointsummarychart.Categories.CategoryLabels = pointsummarychart.ChartData["A2", "A2"];
                pointsummarychart.Series[0].Values = pointsummarychart.ChartData["B2", "B2"];
                //for (int i = 0; i < chart.Series[0].Values.Count; i++)
                for (int i = 0; i < 2; i++)
                {
                    ChartDataPoint cdp = new ChartDataPoint(chart.Series[0]);
                    cdp.Index = i;
                    pointsummarychart.Series[0].DataPoints.Add(cdp);
                }
                pointsummarychart.Series[0].DataPoints[0].Fill.FillType = FillFormatType.Solid;
                pointsummarychart.Series[0].DataPoints[0].Fill.SolidColor.Color = Color.LightBlue;
                pointsummarychart.Series[0].DataPoints[1].Fill.FillType = FillFormatType.Solid;
                pointsummarychart.Series[0].DataPoints[1].Fill.SolidColor.Color = Color.MediumPurple;
                pointsummarychart.Series[0].DataLabels.LabelValueVisible = true;

                // pointsummarychart.Series[0].DoughnutHoleSize = 60;

                //Fourth slide
                ppt.Slides.Append();

                //insert a column chart  
                //insert a column participantloadedbymonthchart  
                RectangleF rect = new RectangleF(10, 10, 700, 400);
                IChart participantloadedbymonthchart = ppt.Slides[3].Shapes.AppendChart(ChartType.ColumnClustered, rect);

                //IAutoShape participantloadedshape = (IAutoShape)ppt.Slides[3].Shapes.AppendShape(ShapeType.Rectangle, new RectangleF(10, 10, 700, 50));
                //participantloadedshape.Fill.FillType = FillFormatType.Solid;
                //participantloadedshape.Fill.SolidColor.Color = Color.Red;
                //participantloadedshape.ShapeStyle.LineColor.Color = Color.White;

                //set chart title  
                participantloadedbymonthchart.ChartTitle.TextProperties.Text = "Participants loaded by month";
                participantloadedbymonthchart.ChartTitle.TextProperties.IsCentered = true;
                participantloadedbymonthchart.ChartTitle.Height = 30;
                participantloadedbymonthchart.HasTitle = true;

                //create a datatable  
                System.Data.DataTable dataTable1 = new DataTable();
                System.Data.DataTable dataTablenew = ToDataTable(result.participantsLoadedByMonth.ToList());
                //dataTable1 = dataTablenew.Copy();
                dataTable1.Columns.Add(new DataColumn("Month", Type.GetType("System.String")));
                dataTable1.Columns.Add(new DataColumn("CustomerCount", Type.GetType("System.Int32")));
                dataTable1.Columns.Add(new DataColumn("Chart", Type.GetType("System.Decimal")));

                //dataTable1.Rows.Clear();
                for (int i = 0; i < dataTablenew.Rows.Count; i++)
                {
                    //dataTable1.Rows.Add("Customer" + i.ToString(), 0);
                    dataTable1.ImportRow(dataTablenew.Rows[i]);
                }

                //import data from datatable to chart data  
                for (int c = 0; c < dataTable1.Columns.Count; c++)
                {
                    participantloadedbymonthchart.ChartData[0, c].Text = dataTable1.Columns[c].Caption;
                }
                for (int r = 0; r < dataTable1.Rows.Count; r++)
                {
                    object[] datas = dataTable1.Rows[r].ItemArray;
                    for (int c = 0; c < datas.Length; c++)
                    {
                        participantloadedbymonthchart.ChartData[r + 1, c].Value = datas[c];

                    }
                }

                //set series labels  
                participantloadedbymonthchart.Series.SeriesLabel = participantloadedbymonthchart.ChartData["B1", "C1"];

                int totalRows = dataTable1.Rows.Count + 1;

                //set categories labels      
                participantloadedbymonthchart.Categories.CategoryLabels = participantloadedbymonthchart.ChartData["A2", "A" + totalRows.ToString()];

                //assign data to series values  
                participantloadedbymonthchart.Series[0].Values = participantloadedbymonthchart.ChartData["B2", "B" + totalRows.ToString()];
                participantloadedbymonthchart.Series[1].Values = participantloadedbymonthchart.ChartData["C2", "C" + totalRows.ToString()];

                //change the chart type of series 2 to line chart with markers  
                participantloadedbymonthchart.Series[1].Type = ChartType.LineMarkers;

                ////plot data of series 2 on the secondary axis  
                //participantloadedbymonthchart.Series[1].UseSecondAxis = true;

                ////set the number format as percentage   
                //participantloadedbymonthchart.SecondaryValueAxis.NumberFormat = "0%";

                //hide grid lines of secondary axis  
                participantloadedbymonthchart.SecondaryValueAxis.MajorGridTextLines.FillType = FillFormatType.None;

                //set overlap  
                //participantloadedbymonthchart.OverLap = -50;

                //set gap width  
                participantloadedbymonthchart.GapWidth = 200;

                //Fifth slide
                ppt.Slides.Append();

                RectangleF OrderPlacedByTypeByMonthrect = new RectangleF(10, 10, 700, 400);
                //IAutoShape orderplacedbytypemonthshape = (IAutoShape)ppt.Slides[4].Shapes.AppendShape(ShapeType.Rectangle, new RectangleF(10, 10, 700, 50));
                //orderplacedbytypemonthshape.Fill.FillType = FillFormatType.Solid;
                //orderplacedbytypemonthshape.Fill.SolidColor.Color = Color.Red;
                //orderplacedbytypemonthshape.ShapeStyle.LineColor.Color = Color.White;




                //create a datatable  
                System.Data.DataTable dtMakeOrderPlacedByTypeByMonth = new DataTable();
                System.Data.DataTable dataTableMakeOrderPlacedByTypeDynamic = ToDataTable(result.orderPlacedByType.ToList());

                if (dataTableMakeOrderPlacedByTypeDynamic != null && dataTableMakeOrderPlacedByTypeDynamic.Rows.Count > 0)
                {
                    IChart OrderPlacedByTypeByMonthchart = ppt.Slides[4].Shapes.AppendChart(ChartType.ColumnClustered, OrderPlacedByTypeByMonthrect);


                    //set chart title  
                    OrderPlacedByTypeByMonthchart.ChartTitle.TextProperties.Text = "Order Placed By Type By Loaded By Month";
                    OrderPlacedByTypeByMonthchart.ChartTitle.TextProperties.IsCentered = true;
                    OrderPlacedByTypeByMonthchart.ChartTitle.Height = 30;
                    OrderPlacedByTypeByMonthchart.HasTitle = true;

                    dtMakeOrderPlacedByTypeByMonth.Columns.Add(new DataColumn("Name", Type.GetType("System.String")));
                    dtMakeOrderPlacedByTypeByMonth.Columns.Add(new DataColumn("OrderCount", Type.GetType("System.Int32")));
                    dtMakeOrderPlacedByTypeByMonth.Columns.Add(new DataColumn("Chart", Type.GetType("System.Decimal")));

                    //dataTable1.Rows.Clear();
                    for (int i = 0; i < dataTableMakeOrderPlacedByTypeDynamic.Rows.Count; i++)
                    {
                        //dataTable1.Rows.Add("Customer" + i.ToString(), 0);
                        dtMakeOrderPlacedByTypeByMonth.ImportRow(dataTableMakeOrderPlacedByTypeDynamic.Rows[i]);
                    }

                    //import data from datatable to chart data  
                    for (int c = 0; c < dtMakeOrderPlacedByTypeByMonth.Columns.Count; c++)
                    {
                        OrderPlacedByTypeByMonthchart.ChartData[0, c].Text = dtMakeOrderPlacedByTypeByMonth.Columns[c].Caption;
                    }
                    for (int r = 0; r < dtMakeOrderPlacedByTypeByMonth.Rows.Count; r++)
                    {
                        object[] datas = dtMakeOrderPlacedByTypeByMonth.Rows[r].ItemArray;
                        for (int c = 0; c < datas.Length; c++)
                        {
                            OrderPlacedByTypeByMonthchart.ChartData[r + 1, c].Value = datas[c];

                        }
                    }

                    //set series labels  
                    OrderPlacedByTypeByMonthchart.Series.SeriesLabel = OrderPlacedByTypeByMonthchart.ChartData["B1", "C1"];

                    int totalRowsOrderPlacedByTypeByMonthchart = dtMakeOrderPlacedByTypeByMonth.Rows.Count + 1;

                    //set categories labels      
                    OrderPlacedByTypeByMonthchart.Categories.CategoryLabels = OrderPlacedByTypeByMonthchart.ChartData["A2", "A" + totalRowsOrderPlacedByTypeByMonthchart.ToString()];

                    //assign data to series values  
                    OrderPlacedByTypeByMonthchart.Series[0].Values = OrderPlacedByTypeByMonthchart.ChartData["B2", "B" + totalRowsOrderPlacedByTypeByMonthchart.ToString()];
                    OrderPlacedByTypeByMonthchart.Series[1].Values = OrderPlacedByTypeByMonthchart.ChartData["C2", "C" + totalRowsOrderPlacedByTypeByMonthchart.ToString()];

                    //change the chart type of series 2 to line chart with markers  
                    OrderPlacedByTypeByMonthchart.Series[1].Type = ChartType.LineMarkers;

                    ////plot data of series 2 on the secondary axis  
                    //OrderPlacedByTypeByMonthchart.Series[1].UseSecondAxis = true;

                    ////set the number format as percentage   
                    //OrderPlacedByTypeByMonthchart.SecondaryValueAxis.NumberFormat = "0%";

                    //hide grid lines of secondary axis  
                    OrderPlacedByTypeByMonthchart.SecondaryValueAxis.MajorGridTextLines.FillType = FillFormatType.None;

                    //set overlap  
                    //OrderPlacedByTypeByMonthchart.OverLap = -50;

                    //set gap width  
                    OrderPlacedByTypeByMonthchart.GapWidth = 200;
                }
                else
                {
                    RectangleF OrderPlacedByTypeByMonthtitleRect1 = new RectangleF(60, 10, 500, 130);
                    IAutoShape OrderPlacedByTypeByMonthtitleShape1 = ppt.Slides[4].Shapes.AppendShape(ShapeType.Rectangle, OrderPlacedByTypeByMonthtitleRect1);
                    OrderPlacedByTypeByMonthtitleShape1.Fill.FillType = FillFormatType.None;
                    OrderPlacedByTypeByMonthtitleShape1.ShapeStyle.LineColor.Color = Color.Empty;
                    TextParagraph OrderPlacedByTypeByMonthtitlePara1 = OrderPlacedByTypeByMonthtitleShape1.TextFrame.Paragraphs[0];
                    OrderPlacedByTypeByMonthtitlePara1.Text = "Order Placed By Type By Loaded By Month";
                    OrderPlacedByTypeByMonthtitlePara1.FirstTextRange.FontHeight = 20;
                    OrderPlacedByTypeByMonthtitlePara1.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                    OrderPlacedByTypeByMonthtitlePara1.FirstTextRange.Fill.SolidColor.Color = Color.Black;
                    OrderPlacedByTypeByMonthtitlePara1.Alignment = TextAlignmentType.Center;

                    RectangleF OrderPlacedByTypeByMonthtitleRect = new RectangleF(60, 100, 500, 130);
                    IAutoShape OrderPlacedByTypeByMonthtitleShape = ppt.Slides[4].Shapes.AppendShape(ShapeType.Rectangle, OrderPlacedByTypeByMonthtitleRect);
                    OrderPlacedByTypeByMonthtitleShape.Fill.FillType = FillFormatType.None;
                    OrderPlacedByTypeByMonthtitleShape.ShapeStyle.LineColor.Color = Color.Empty;
                    TextParagraph OrderPlacedByTypeByMonthtitlePara = OrderPlacedByTypeByMonthtitleShape.TextFrame.Paragraphs[0];
                    OrderPlacedByTypeByMonthtitlePara.Text = "No Orders Available.";
                    OrderPlacedByTypeByMonthtitlePara.FirstTextRange.FontHeight = 20;
                    OrderPlacedByTypeByMonthtitlePara.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                    OrderPlacedByTypeByMonthtitlePara.FirstTextRange.Fill.SolidColor.Color = Color.Black;
                    OrderPlacedByTypeByMonthtitlePara.Alignment = TextAlignmentType.Center;
                }


                //Sixth slide
                ppt.Slides.Append();
                //TopPointsHoldersByStore[] pointhoders = result.topPointsHolders.ToList().ToArray();
                DataTable dtpointhoders = ToDataTable(result.topPointsHolders.ToList());

                //IAutoShape pointhodersshape = (IAutoShape)ppt.Slides[5].Shapes.AppendShape(ShapeType.Rectangle, new RectangleF(10, 10, 700, 50));
                //pointhodersshape.Fill.FillType = FillFormatType.Solid;
                //pointhodersshape.Fill.SolidColor.Color = Color.Red;
                //pointhodersshape.ShapeStyle.LineColor.Color = Color.White;
                RectangleF pointhoderstitleRect = new RectangleF(10, 10, 200, 30);
                IAutoShape pointhoderstitleShape = ppt.Slides[5].Shapes.AppendShape(ShapeType.Rectangle, pointhoderstitleRect);
                pointhoderstitleShape.Fill.FillType = FillFormatType.None;
                pointhoderstitleShape.ShapeStyle.LineColor.Color = Color.Empty;
                TextParagraph pointhoderstitlePara = pointhoderstitleShape.TextFrame.Paragraphs[0];
                pointhoderstitlePara.Text = "Top Points Holders";
                pointhoderstitlePara.FirstTextRange.FontHeight = 20;
                pointhoderstitlePara.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                pointhoderstitlePara.FirstTextRange.Fill.SolidColor.Color = Color.Black;
                pointhoderstitlePara.Alignment = TextAlignmentType.Center;

                Double[] widths = new double[] { 100, 100, 100, 100, 100, 100 };
                Double[] heights = new double[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
                ITable table = ppt.Slides[5].Shapes.AppendTable(ppt.SlideSize.Size.Width / 2 - 275, 40, widths, heights);
                //set the style of table
                table.StylePreset = TableStylePreset.LightStyle1Accent2;


                //    String[,] dataStr = new String[,]{
                //{"Name",    "Capital",  "Continent",    "Area", "Population"},
                //{"Venezuela",   "Caracas",  "South America",    "912047",   "19700000"},
                //{"Bolivia", "La Paz",   "South America",    "1098575",  "7300000"},
                //{"Brazil",  "Brasilia", "South America",    "8511196",  "150400000"},
                //{"Canada",  "Ottawa",   "North America",    "9976147",  "26500000"},
                //{"Chile",   "Santiago", "South America",    "756943",   "13200000"},
                //{"Colombia",    "Bagota",   "South America",    "1138907",  "33000000"},
                //{"Cuba",    "Havana",   "North America",    "114524",   "10600000"},
                //{"Ecuador", "Quito",    "South America",    "455502",   "10600000"},
                //{"Paraguay",    "Asuncion","South America", "406576",   "4660000"},
                //{"Peru",    "Lima", "South America",    "1285215",  "21600000"},
                //{"Jamaica", "Kingston", "North America",    "11424",    "2500000"},
                //{"Mexico",  "Mexico City",  "North America",    "1967180",  "88600000"}
                //};

                //for (int i = 0; i < 13; i++)
                //    for (int j = 0; j < 5; j++)
                //    {
                //        //fill the table with data
                //        table[j, i].TextFrame.Text = dataStr[i, j];
                //        //set the Font
                //        table[j, i].TextFrame.Paragraphs[0].TextRanges[0].LatinFont = new TextFont("Arial Narrow");
                //    }

                for (int i = 0; i < dtpointhoders.Columns.Count; i++)
                {
                    table[i, 0].TextFrame.Text = dtpointhoders.Columns[i].ColumnName.ToString();// dataStr[i, j];
                }

                for (int i = 0; i < dtpointhoders.Rows.Count; i++)
                    for (int j = 0; j < dtpointhoders.Columns.Count; j++)
                    {
                        //fill the table with data
                        table[j, i + 1].TextFrame.Text = dtpointhoders.Rows[i][j].ToString();// dataStr[i, j];
                        //set the Font
                        table[j, i + 1].TextFrame.Paragraphs[0].TextRanges[0].LatinFont = new TextFont("Arial Narrow");
                        table[j, i + 1].TextFrame.Paragraphs[0].TextRanges[0].FontHeight = 10;
                    }

                //set the alignment of the first row to Center
                for (int i = 0; i < 6; i++)
                {
                    table[i, 0].TextFrame.Paragraphs[0].Alignment = TextAlignmentType.Center;
                }

                //Seventh slide
                //Points Loaded By Month
                ppt.Slides.Append();



                ppt.SlideSize.Type = SlideSizeType.Screen16x9;
                SizeF slidesize = ppt.SlideSize.Size;

                //IAutoShape pointsloadedbymonthshape = (IAutoShape)ppt.Slides[6].Shapes.AppendShape(ShapeType.Rectangle, new RectangleF(10, 10, 700, 50));
                //pointsloadedbymonthshape.Fill.FillType = FillFormatType.Solid;
                //pointsloadedbymonthshape.Fill.SolidColor.Color = Color.Red;
                //pointsloadedbymonthshape.ShapeStyle.LineColor.Color = Color.White;

                var pointsloadedbymonthslide = ppt.Slides[6];

                //Add title
                RectangleF pointsloadedbymonthtitleRect = new RectangleF(10, 10, 250, 50);
                IAutoShape pointsloadedbymonthtitleShape = pointsloadedbymonthslide.Shapes.AppendShape(ShapeType.Rectangle, pointsloadedbymonthtitleRect);
                pointsloadedbymonthtitleShape.Fill.FillType = FillFormatType.None;
                pointsloadedbymonthtitleShape.ShapeStyle.LineColor.Color = Color.Empty;
                TextParagraph pointsloadedbymonthtitlePara = pointsloadedbymonthtitleShape.TextFrame.Paragraphs[0];
                pointsloadedbymonthtitlePara.Text = "Points Loaded By Month";
                pointsloadedbymonthtitlePara.FirstTextRange.FontHeight = 20;
                pointsloadedbymonthtitlePara.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                pointsloadedbymonthtitlePara.FirstTextRange.Fill.SolidColor.Color = Color.Black;
                pointsloadedbymonthtitlePara.Alignment = TextAlignmentType.Center;

                RectangleF pointsloadedbymonthrect = new RectangleF(60, 20, slidesize.Width - 40, slidesize.Height - 10);
                IChart pointsloadedbymonthchart = pointsloadedbymonthslide.Shapes.AppendChart(Spire.Presentation.Charts.ChartType.BarStacked, pointsloadedbymonthrect);

                string[] columnlabels = { "Points Loaded By Month" };

                DataTable dtpointsloadedbymonth = ToDataTable(result.pointsLoadedByMonth.ToList());

                String[] cols = columnlabels.ToArray();
                for (Int32 c = 0; c < dtpointsloadedbymonth.Columns.Count; ++c)
                    pointsloadedbymonthchart.ChartData[0, c + 1].Text = dtpointsloadedbymonth.Columns[c].ToString();

                string[] rowlabels = new string[dtpointsloadedbymonth.Rows.Count];

                for (int i = 0; i < dtpointsloadedbymonth.Rows.Count; i++)
                {
                    rowlabels[i] = dtpointsloadedbymonth.Rows[i][0].ToString();
                }

                String[] rows = rowlabels.ToArray();
                for (Int32 r = 0; r < rows.Count(); ++r)
                    pointsloadedbymonthchart.ChartData[r + 1, 0].Text = rows[r];

                string[,] values = new string[dtpointsloadedbymonth.Rows.Count, 1];

                for (int i = 0; i < dtpointsloadedbymonth.Rows.Count; i++)
                {
                    values[i, 0] = dtpointsloadedbymonth.Rows[i][1].ToString();
                }

                double value = 0.0;
                for (Int32 r = 0; r < rows.Count(); ++r)
                {
                    for (Int32 c = 0; c < cols.Count(); ++c)
                    {
                        value = Math.Round(Convert.ToDouble(values[r, c]), 2);
                        pointsloadedbymonthchart.ChartData[r + 1, c + 1].Value = value;
                    }
                }

                pointsloadedbymonthchart.Series.SeriesLabel = pointsloadedbymonthchart.ChartData[0, 1, 0, columnlabels.Count()];
                pointsloadedbymonthchart.Categories.CategoryLabels = pointsloadedbymonthchart.ChartData[1, 0, rowlabels.Count(), 0];

                pointsloadedbymonthchart.PrimaryCategoryAxis.Position = AxisPositionType.Left;
                pointsloadedbymonthchart.SecondaryCategoryAxis.Position = AxisPositionType.Left;
                pointsloadedbymonthchart.PrimaryCategoryAxis.TickLabelPosition = TickLabelPositionType.TickLabelPositionLow;

                for (Int32 c = 0; c < cols.Count(); ++c)
                {
                    pointsloadedbymonthchart.Series[c].Values = pointsloadedbymonthchart.ChartData[1, c + 1, rowlabels.Count(), c + 1];
                    pointsloadedbymonthchart.Series[c].Fill.FillType = FillFormatType.Solid;
                    pointsloadedbymonthchart.Series[c].InvertIfNegative = true;
                    pointsloadedbymonthchart.Series[c].Fill.SolidColor.Color = Color.LightBlue;

                    for (Int32 r = 0; r < rows.Count(); ++r)
                    {
                        var label = pointsloadedbymonthchart.Series[c].DataLabels.Add();
                        label.LabelValueVisible = true;
                        //label.Position = ChartDataLabelPosition.InsideEnd;
                        pointsloadedbymonthchart.Series[c].DataLabels[r].HasDataSource = false;
                        pointsloadedbymonthchart.Series[c].DataLabels.TextProperties.Paragraphs[0].DefaultCharacterProperties.FontHeight = 6;
                    }
                }
                pointsloadedbymonthchart.Series[0].Fill.SolidColor.Color = Color.LightBlue;
                //chart.Series[1].Fill.SolidColor.Color = Color.Red;
                //chart.Series[2].Fill.SolidColor.Color = Color.Green;

                TextFont font = new TextFont("Tw Cen MT");

                for (int k = 0; k < pointsloadedbymonthchart.ChartLegend.EntryTextProperties.Length; k++)
                {
                    pointsloadedbymonthchart.ChartLegend.EntryTextProperties[k].LatinFont = font;
                    pointsloadedbymonthchart.ChartLegend.EntryTextProperties[k].FontHeight = 10;
                }




                //Eightth slide
                //Points Redeemed By Month
                ppt.Slides.Append();
                ppt.SlideSize.Type = SlideSizeType.Screen16x9;
                SizeF pointsredeemedslidesize = ppt.SlideSize.Size;

                var pointsredeemedslide = ppt.Slides[7];

                //IAutoShape pointsredeemedshape = (IAutoShape)ppt.Slides[7].Shapes.AppendShape(ShapeType.Rectangle, new RectangleF(10, 10, 700, 50));
                //pointsredeemedshape.Fill.FillType = FillFormatType.Solid;
                //pointsredeemedshape.Fill.SolidColor.Color = Color.Red;
                //pointsredeemedshape.ShapeStyle.LineColor.Color = Color.White;

                //Add title
                RectangleF pointsredeemedtitleRect = new RectangleF(10, 10, 250, 50);
                IAutoShape pointsredeemedtitleShape = pointsredeemedslide.Shapes.AppendShape(ShapeType.Rectangle, pointsredeemedtitleRect);
                pointsredeemedtitleShape.Fill.FillType = FillFormatType.None;
                pointsredeemedtitleShape.ShapeStyle.LineColor.Color = Color.Empty;
                TextParagraph pointsredeemedtitlePara = pointsredeemedtitleShape.TextFrame.Paragraphs[0];
                pointsredeemedtitlePara.Text = "Points Reedemed By Month";
                pointsredeemedtitlePara.FirstTextRange.FontHeight = 20;
                pointsredeemedtitlePara.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                pointsredeemedtitlePara.FirstTextRange.Fill.SolidColor.Color = Color.Black;
                pointsredeemedtitlePara.Alignment = TextAlignmentType.Center;

                RectangleF pointsredeemedrect = new RectangleF(60, 20, pointsredeemedslidesize.Width - 40, pointsredeemedslidesize.Height - 10);
                IChart pointsredeemedchart = pointsredeemedslide.Shapes.AppendChart(Spire.Presentation.Charts.ChartType.BarStacked, pointsredeemedrect);

                string[] pointsredeemedcolumnlabels = { "Points Reedemed By Month" };

                DataTable dtpointsredeemed = ToDataTable(result.pointsRedeemedByMonth.ToList());

                String[] pointsredeemedcols = pointsredeemedcolumnlabels.ToArray();
                for (Int32 c = 0; c < dtpointsredeemed.Columns.Count; ++c)
                    pointsredeemedchart.ChartData[0, c + 1].Text = dtpointsredeemed.Columns[c].ToString();

                string[] pointsredeemedrowlabels = new string[dtpointsredeemed.Rows.Count];

                for (int i = 0; i < dtpointsredeemed.Rows.Count; i++)
                {
                    pointsredeemedrowlabels[i] = dtpointsredeemed.Rows[i][0].ToString();
                }

                String[] pointsredeemedrows = pointsredeemedrowlabels.ToArray();
                for (Int32 r = 0; r < pointsredeemedrows.Count(); ++r)
                    pointsredeemedchart.ChartData[r + 1, 0].Text = pointsredeemedrows[r];

                string[,] pointsredeemedvalues = new string[dtpointsredeemed.Rows.Count, 1];

                for (int i = 0; i < dtpointsredeemed.Rows.Count; i++)
                {
                    pointsredeemedvalues[i, 0] = dtpointsredeemed.Rows[i][1].ToString();
                }

                double pointsredeemedvalue = 0.0;
                for (Int32 r = 0; r < pointsredeemedrows.Count(); ++r)
                {
                    for (Int32 c = 0; c < pointsredeemedcols.Count(); ++c)
                    {
                        pointsredeemedvalue = Math.Round(Convert.ToDouble(pointsredeemedvalues[r, c]), 2);
                        pointsredeemedchart.ChartData[r + 1, c + 1].Value = pointsredeemedvalue;
                    }
                }

                pointsredeemedchart.Series.SeriesLabel = pointsredeemedchart.ChartData[0, 1, 0, columnlabels.Count()];
                pointsredeemedchart.Categories.CategoryLabels = pointsredeemedchart.ChartData[1, 0, rowlabels.Count(), 0];

                pointsredeemedchart.PrimaryCategoryAxis.Position = AxisPositionType.Left;
                pointsredeemedchart.SecondaryCategoryAxis.Position = AxisPositionType.Left;
                pointsredeemedchart.PrimaryCategoryAxis.TickLabelPosition = TickLabelPositionType.TickLabelPositionLow;

                for (Int32 c = 0; c < pointsredeemedcols.Count(); ++c)
                {
                    pointsredeemedchart.Series[c].Values = pointsredeemedchart.ChartData[1, c + 1, rowlabels.Count(), c + 1];
                    pointsredeemedchart.Series[c].Fill.FillType = FillFormatType.Solid;
                    pointsredeemedchart.Series[c].InvertIfNegative = true;
                    pointsredeemedchart.Series[c].Fill.SolidColor.Color = Color.LightBlue;

                    for (Int32 r = 0; r < pointsredeemedrows.Count(); ++r)
                    {
                        var pointsredeemedlabel = pointsredeemedchart.Series[c].DataLabels.Add();
                        pointsredeemedlabel.LabelValueVisible = true;
                        //pointsredeemedlabel.Position = ChartDataLabelPosition.InsideEnd;
                        pointsredeemedchart.Series[c].DataLabels[r].HasDataSource = false;
                        pointsredeemedchart.Series[c].DataLabels.TextProperties.Paragraphs[0].DefaultCharacterProperties.FontHeight = 6;
                    }
                }
                pointsredeemedchart.Series[0].Fill.SolidColor.Color = Color.LightBlue;
                //chart.Series[1].Fill.SolidColor.Color = Color.Red;
                //chart.Series[2].Fill.SolidColor.Color = Color.Green;

                TextFont pointsredeemedfont = new TextFont("Tw Cen MT");

                for (int k = 0; k < pointsredeemedchart.ChartLegend.EntryTextProperties.Length; k++)
                {
                    pointsredeemedchart.ChartLegend.EntryTextProperties[k].LatinFont = font;
                    pointsredeemedchart.ChartLegend.EntryTextProperties[k].FontHeight = 10;
                }


                //save to file  
                ppt.SaveToFile(HttpContext.Current.Server.MapPath("~/Files//") + "Monthly Report.pptx", FileFormat.Pptx2010);
                //presentation.SaveToFile(HttpContext.Current.Server.MapPath("~/Files//") + "CombinationChart.pptx", FileFormat.Pptx2010);

                HttpResponseMessage response = Request.CreateResponse();

                string filePath = HttpContext.Current.Server.MapPath("~/Files//") + "Monthly Report.pptx";
                FileInfo fileInfo = new FileInfo(filePath);

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = GetMimeType("Monthly Report.pptx");
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=Monthly Report.pptx");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.WriteFile(HttpContext.Current.Server.MapPath("~/Files//") + "Monthly Report.pptx");

                System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
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

        public static Stream ReadStream(FileInfo fileInfo)
        {
            int bufferSize = 1048575; // 1MB
            return new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize);
        }

        private static string GetMimeType(string fileName)
        {
            string mimeType = "application/unknown";
            string ext = System.IO.Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
                mimeType = regKey.GetValue("Content Type").ToString();
            return mimeType;
        }

        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties by using reflection   
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names  
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {

                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

        [HttpPost]
        public HttpResponseMessage ExportStoreHTMLPDF(StoreEntity storeentity)
        {


            // var htmlContent1 = storeentity.template;//String.Format("<body>Hello world: {0}</body>", ZTime.Now);
            // var htmlToPdf1 = new NReco.PdfGenerator.HtmlToPdfConverter();
            // var pdfBytes1 = htmlToPdf1.GeneratePdf(htmlContent1);
            // //var headingfile = File.Create(HttpContext.Current.Server.MapPath("~/Files//") + "Heading.pdf");
            // //headingfile.Write(pdfBytes1,0,pdfBytes1.Length);
            // //File.Open(HttpContext.Current.Server.MapPath("~/Files//") + "Heading.pdf", FileMode.Open);
            // File.WriteAllBytes(HttpContext.Current.Server.MapPath("~/Files//") + "Heading.pdf", pdfBytes1);

            // var htmlContent2 = storeentity.description;
            // var htmlToPdf2 = new NReco.PdfGenerator.HtmlToPdfConverter();
            // var pdfBytes2 = htmlToPdf2.GeneratePdf(htmlContent2);
            // File.WriteAllBytes(HttpContext.Current.Server.MapPath("~/Files//") + "StoreImage.pdf", pdfBytes2);

            // var htmlContent3 = storeentity.address;
            // var htmlToPdf3 = new NReco.PdfGenerator.HtmlToPdfConverter();
            // var pdfBytes3 = htmlToPdf3.GeneratePdf(htmlContent3);
            // File.WriteAllBytes(HttpContext.Current.Server.MapPath("~/Files//") + "StoreSummary.pdf", pdfBytes3);

            // var htmlContent4 = storeentity.county;
            // var htmlToPdf4 = new NReco.PdfGenerator.HtmlToPdfConverter();
            // var pdfBytes4 = htmlToPdf4.GeneratePdf(htmlContent4);
            // File.WriteAllBytes(HttpContext.Current.Server.MapPath("~/Files//") + "PointsRemaining.pdf", pdfBytes4);

            // var htmlContent5 = storeentity.telephone;
            // var htmlToPdf5 = new NReco.PdfGenerator.HtmlToPdfConverter();
            // var pdfBytes5 = htmlToPdf5.GeneratePdf(htmlContent5);
            // File.WriteAllBytes(HttpContext.Current.Server.MapPath("~/Files//") + "ParticipantsLoadedByMonth.pdf", pdfBytes5);


            // MergePDFs(HttpContext.Current.Server.MapPath("~/Files//") + "Heading.pdf",
            //     HttpContext.Current.Server.MapPath("~/Files//") + "StoreImage.pdf", 
            //     HttpContext.Current.Server.MapPath("~/Files//") + "StoreSummary.pdf",
            //     HttpContext.Current.Server.MapPath("~/Files//") + "PointsRemaining.pdf",
            //     HttpContext.Current.Server.MapPath("~/Files//") + "ParticipantsLoadedByMonth.pdf");

            //// headingfile.Close();

            HttpResponseMessage httpResponseMessage;
            httpResponseMessage = new HttpResponseMessage();
            ////HttpResponseMessage streamContent = new HttpResponseMessage(HttpStatusCode.OK);
            ////Stream @null = Stream.Null;

            ////streamContent.Content = new StreamContent(new MemoryStream(pdfBytes1));
            ////streamContent.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            ////streamContent.Content.Headers.Add("content-disposition", string.Concat("inline;  filename=\"", "Create.pdf", "\""));
            ////httpResponseMessage = streamContent;

            ////return httpResponseMessage;
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
