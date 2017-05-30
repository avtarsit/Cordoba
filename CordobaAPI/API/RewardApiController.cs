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

        [HttpGet]
        public HttpResponseMessage ViewCustomerRewards(int reward_id)
        {
            try
            {
                var customerRewards = _rewardService.ViewCustomerRewards(reward_id);
                return Request.CreateResponse(HttpStatusCode.OK, customerRewards);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public HttpResponseMessage GetRewardCustomerDetails(int reward_user_id)
        {
            try
            {
                var customer_rewarddetails = _rewardService.GetRewardCustomerDetails(reward_user_id);
                return Request.CreateResponse(HttpStatusCode.OK, customer_rewarddetails);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public HttpResponseMessage DeleteRewardUser(int id, int reward_user_id)
        {
            try
            {
                var result = _rewardService.DeleteRewardUser(id, reward_user_id);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public HttpResponseMessage MyRewards(int id)
        {
            try
            {
                var myRewardslist = _rewardService.MyRewards(id);
                return Request.CreateResponse(HttpStatusCode.OK, myRewardslist);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
