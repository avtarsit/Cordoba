using CordobaModels.Entities;
using CordobaServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Services
{
    public class ManufacturerServices : IManufacturerServices
    {
       public List<ManufacturersEntity> GetManufacturersList(int? ManufacturersID)
       {
           List<ManufacturersEntity> ManufacturersList = new List<ManufacturersEntity>();
           ManufacturersList.Add(new ManufacturersEntity() { ManufacturerID = 1, ManufacturerName="Apple" });
           ManufacturersList.Add(new ManufacturersEntity() { ManufacturerID = 2, ManufacturerName = "Canon" });
           ManufacturersList.Add(new ManufacturersEntity() { ManufacturerID = 3, ManufacturerName = "CVD" });
           ManufacturersList.Add(new ManufacturersEntity() { ManufacturerID = 4, ManufacturerName = "Hewlett-Packed" });
           ManufacturersList.Add(new ManufacturersEntity() { ManufacturerID = 5, ManufacturerName = "HTC" });
           ManufacturersList.Add(new ManufacturersEntity() { ManufacturerID = 6, ManufacturerName = "Palm" });
           ManufacturersList.Add(new ManufacturersEntity() { ManufacturerID = 7, ManufacturerName = "Sony" });          
           return ManufacturersList;
       }
    }
}
