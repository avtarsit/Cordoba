using CordobaModels;
using CordobaModels.Entities;
using CordobaServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Services
{
    public class BannerService : IBannerServices
    {
        private GenericRepository<BannerEntity> objGenericRepository = new GenericRepository<BannerEntity>();

        public List<BannerEntity> GetBannerList()
        {
            List<BannerEntity> Banners = new List<BannerEntity>();
            Banners = objGenericRepository.ExecuteSQL<BannerEntity>("GetBannerList").ToList();
            return Banners;
        }
        //public List<BannerEntity> GetBannerList()
        //{
        //    List<BannerEntity> Banner = new List<BannerEntity>();
        //    Banner.Add(new BannerEntity() { BannerId = 1, BannerName = "MAKE A DIFFERENCE THANK" ,StatusId = 1,StatusName ="Enabled"  });
        //    Banner.Add(new BannerEntity() { BannerId = 2, BannerName = "Annodata Rewards", StatusId = 1, StatusName = "Enabled" });
        //    Banner.Add(new BannerEntity() { BannerId = 3, BannerName = "arkle finance", StatusId = 1, StatusName = "Enabled" });
        //    Banner.Add(new BannerEntity() { BannerId = 4, BannerName = "Asset Advantage", StatusId = 1, StatusName = "Enabled" });
        //    Banner.Add(new BannerEntity() { BannerId = 5, BannerName = "blizzardrewards", StatusId = 1, StatusName = "Enabled" });
        //    Banner.Add(new BannerEntity() { BannerId = 6, BannerName = "BTLB Rewards", StatusId = 1, StatusName = "Enabled" });
        //    Banner.Add(new BannerEntity() { BannerId = 7, BannerName = "clearwinnersclub", StatusId = 1, StatusName = "Enabled" });
        //    Banner.Add(new BannerEntity() { BannerId = 8, BannerName = "Cordoba Slider", StatusId = 1, StatusName = "Enabled" });
        //    Banner.Add(new BannerEntity() { BannerId = 9, BannerName = "CVD Rewards" ,StatusId = 1, StatusName = "Enabled" });
        //    Banner.Add(new BannerEntity() { BannerId = 10, BannerName = "D and D Leasing Rewards",StatusId = 1, StatusName = "Enabled" });
        //    Banner.Add(new BannerEntity() { BannerId = 11, BannerName = "Default Banner" ,StatusId = 1, StatusName = "Enabled" });
        //    Banner.Add(new BannerEntity() { BannerId = 12, BannerName = "Econocom Rewards" ,StatusId = 1, StatusName = "Enabled" });
        //    Banner.Add(new BannerEntity() { BannerId = 13, BannerName = "fr.pbmakeadifferencethankyou",StatusId = 1, StatusName = "Enabled" });
        //    Banner.Add(new BannerEntity() { BannerId = 14, BannerName = "GO SKIPPY" ,StatusId = 1, StatusName = "Enabled" });
        //    Banner.Add(new BannerEntity() { BannerId = 15, BannerName = "Grenke Rewards" ,StatusId = 1, StatusName = "Enabled" });
        //    return Banner;
        //}

        //public BannerEntity GetBannerById(int BannerId, int StoreId, int LoggedInUserId)
        //{
        //    BannerEntity bannerEntity = new BannerEntity();
        //    if (BannerId > 0)
        //    {
        //        bannerEntity = (from t in GetBannerList()
        //                        where t.BannerId == BannerId
        //                        select t).FirstOrDefault();
        //    }
        //    else
        //    {
        //        bannerEntity = new BannerEntity();
        //        bannerEntity.StatusId = 1;
        //    }
        //    return bannerEntity;
        //}
    }
}
