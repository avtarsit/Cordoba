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
        public List<BannerEntity> GetBannerList()
        {
            List<BannerEntity> Banner = new List<BannerEntity>();            
            return Banner;
        }

        public BannerEntity GetBannerById(int BannerId, int StoreId, int LoggedInUserId)
        {
            BannerEntity bannerEntity = new BannerEntity();
            if (BannerId > 0)
            {
                bannerEntity = (from t in GetBannerList()
                                where t.banner_id == BannerId
                                          select t).FirstOrDefault();
            }
            else
            {
                bannerEntity = new BannerEntity();
                bannerEntity.status = 1;
            }
            return bannerEntity;
        }
    }
}
