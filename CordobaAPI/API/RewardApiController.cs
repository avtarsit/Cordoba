using CordobaModels.Entities;
using CordobaServices.Interfaces;
using CordobaServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CordobaAPI.API
{
    public class RewardApiController : ApiController
    {
        public IRewardService _rewardService;
        public RewardApiController()
        {
            _rewardService = new RewardService();
        }

        [HttpGet]
        public HttpResponseMessage GetRewardList(int reward_id)
        {
            try
            {
                var result = _rewardService.GetRewardList(reward_id);
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
        public HttpResponseMessage GetRewardTypeList()
        {
            try
            {
                var result = _rewardService.GetRewardTypeList();
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
        public HttpResponseMessage InsertOrUpdateReward(int isAddMode, RewardEntity objRewardEntity)
        {
            try
            {
                var result = _rewardService.InsertOrUpdateReward(objRewardEntity, isAddMode);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public HttpResponseMessage DeleteReward(int reward_id)
        {
            try
            {
                int result = _rewardService.DeleteReward(reward_id);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
