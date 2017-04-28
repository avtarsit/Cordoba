using CordobaModels.Entities;
using CordobaServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Services
{
    public class SupplierServices : ISupplierServices
    {
       public List<SupplierEntity> GetSupplierList(int? SupplierID)
       {
           List<SupplierEntity> SupplierList = new List<SupplierEntity>();
           SupplierList.Add(new SupplierEntity() { SupplierID = 1, SupplierName = "Procurement International Limited", SupplierAddress = "VICTORY HOUSE 17 & 19 MARINO WAY FINCHAMPSTEAD BERKSHIRE RG40 4RF" });
           SupplierList.Add(new SupplierEntity() { SupplierID = 2, SupplierName = "Apple", SupplierAddress = "VICTORY HOUSE 17 & 19 MARINO WAY FINCHAMPSTEAD BERKSHIRE RG40 4RF" });
           SupplierList.Add(new SupplierEntity() { SupplierID = 3, SupplierName = "John Lewis", SupplierAddress = "VICTORY HOUSE 17 & 19 MARINO WAY FINCHAMPSTEAD BERKSHIRE RG40 4RF" });
           SupplierList.Add(new SupplierEntity() { SupplierID = 4, SupplierName = "Drum Lessons R Us", SupplierAddress = "VICTORY HOUSE 17 & 19 MARINO WAY FINCHAMPSTEAD BERKSHIRE RG40 4RF" });
           SupplierList.Add(new SupplierEntity() { SupplierID = 5, SupplierName = "Red Letter Day", SupplierAddress = "VICTORY HOUSE 17 & 19 MARINO WAY FINCHAMPSTEAD BERKSHIRE RG40 4RF" });
           SupplierList.Add(new SupplierEntity() { SupplierID = 6, SupplierName = "Virgin Experience", SupplierAddress = "VICTORY HOUSE 17 & 19 MARINO WAY FINCHAMPSTEAD BERKSHIRE RG40 4RF" });
           SupplierList.Add(new SupplierEntity() { SupplierID = 7, SupplierName = "All Garden Fun", SupplierAddress = "VICTORY HOUSE 17 & 19 MARINO WAY FINCHAMPSTEAD BERKSHIRE RG40 4RF" });
           SupplierList.Add(new SupplierEntity() { SupplierID = 8, SupplierName = "Tesco Direct", SupplierAddress = "VICTORY HOUSE 17 & 19 MARINO WAY FINCHAMPSTEAD BERKSHIRE RG40 4RF" });
           SupplierList.Add(new SupplierEntity() { SupplierID = 9, SupplierName = " Mow Direct ", SupplierAddress = "VICTORY HOUSE 17 & 19 MARINO WAY FINCHAMPSTEAD BERKSHIRE RG40 4RF" });
           SupplierList.Add(new SupplierEntity() { SupplierID = 10, SupplierName = "Splash and Relax", SupplierAddress = "VICTORY HOUSE 17 & 19 MARINO WAY FINCHAMPSTEAD BERKSHIRE RG40 4RF" });
           SupplierList.Add(new SupplierEntity() { SupplierID = 11, SupplierName = "Home Leisure Direct", SupplierAddress = "VICTORY HOUSE 17 & 19 MARINO WAY FINCHAMPSTEAD BERKSHIRE RG40 4RF" });
           SupplierList.Add(new SupplierEntity() { SupplierID = 12, SupplierName = "PlayGamesUK", SupplierAddress = "VICTORY HOUSE 17 & 19 MARINO WAY FINCHAMPSTEAD BERKSHIRE RG40 4RF" });
           SupplierList.Add(new SupplierEntity() { SupplierID = 13, SupplierName = "Bose ", SupplierAddress = "VICTORY HOUSE 17 & 19 MARINO WAY FINCHAMPSTEAD BERKSHIRE RG40 4RF" });
           SupplierList.Add(new SupplierEntity() { SupplierID = 14, SupplierName = "Liberty Games ", SupplierAddress = "VICTORY HOUSE 17 & 19 MARINO WAY FINCHAMPSTEAD BERKSHIRE RG40 4RF" });
           SupplierList.Add(new SupplierEntity() { SupplierID = 15, SupplierName = "Simply Acer ", SupplierAddress = "VICTORY HOUSE 17 & 19 MARINO WAY FINCHAMPSTEAD BERKSHIRE RG40 4RF" });
           SupplierList.Add(new SupplierEntity() { SupplierID = 16, SupplierName = " Best Buy", SupplierAddress = "VICTORY HOUSE 17 & 19 MARINO WAY FINCHAMPSTEAD BERKSHIRE RG40 4RF" });
           SupplierList.Add(new SupplierEntity() { SupplierID = 17, SupplierName = "Edenred Travel ", SupplierAddress = "VICTORY HOUSE 17 & 19 MARINO WAY FINCHAMPSTEAD BERKSHIRE RG40 4RF" });
           SupplierList.Add(new SupplierEntity() { SupplierID = 18, SupplierName = "Currys ", SupplierAddress = "VICTORY HOUSE 17 & 19 MARINO WAY FINCHAMPSTEAD BERKSHIRE RG40 4RF" });
           SupplierList.Add(new SupplierEntity() { SupplierID = 19, SupplierName = "Waitrose ", SupplierAddress = "VICTORY HOUSE 17 & 19 MARINO WAY FINCHAMPSTEAD BERKSHIRE RG40 4RF" });
           SupplierList.Add(new SupplierEntity() { SupplierID = 20, SupplierName = " Whiskey Exchange", SupplierAddress = "VICTORY HOUSE 17 & 19 MARINO WAY FINCHAMPSTEAD BERKSHIRE RG40 4RF" });
           SupplierList.Add(new SupplierEntity() { SupplierID = 21, SupplierName = " Waud Wine Club", SupplierAddress = "VICTORY HOUSE 17 & 19 MARINO WAY FINCHAMPSTEAD BERKSHIRE RG40 4RF" });
           SupplierList.Add(new SupplierEntity() { SupplierID = 22, SupplierName = "Edenred Vouchers ", SupplierAddress = "VICTORY HOUSE 17 & 19 MARINO WAY FINCHAMPSTEAD BERKSHIRE RG40 4RF" });
           SupplierList.Add(new SupplierEntity() { SupplierID = 23, SupplierName = "Jessops ", SupplierAddress = "VICTORY HOUSE 17 & 19 MARINO WAY FINCHAMPSTEAD BERKSHIRE RG40 4RF" });
           return SupplierList;
       }


       public SupplierEntity GetSupplierDetail(int? SupplierID)
       {
           SupplierEntity Supplier = new SupplierEntity();
           if (SupplierID > 0)
           {
               Supplier = (from t in GetSupplierList(SupplierID)
                           where t.SupplierID == SupplierID
                               select t).FirstOrDefault();
           }
           else
           {
               Supplier = new SupplierEntity();
           }

           return Supplier;

       }
    }
}
