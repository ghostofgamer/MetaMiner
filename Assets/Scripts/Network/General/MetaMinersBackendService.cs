using MetaMiners.Network.Core;
using MetaMiners.Network.Core.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetaMiners.Network
{
    public class MetaMinersBackendService : BackendServiceProxy
    {
        protected override void Awake()
        {
            base.Awake();
            CreateOwnDataStorage("user-data");
        }

        [RequestEndpoint("/get_profile", RequestMethod.POST)]
        //[HeaderRequired("Authorization", canGetFromDataStorage: true)]
        public void PostGetProfile(Requests.BaseRequest body, Action<Responses.BaseResponse<Responses.GetProfileResponse>> callback, Dictionary<string, string> headers = null, Action<Exception> callbackError = null)
        {
            InvokeRequest(nameof(PostGetProfile), callback: callback, callbackError: callbackError, headers: headers, body: body);
        }

        [RequestEndpoint("/click", RequestMethod.POST)]
        public void PostClick(Requests.ClickRequest body, Action<Responses.BaseResponse<Responses.ClickResponse>> callback, Action<Exception> callbackError = null)
        {
            InvokeRequest(nameof(PostClick), callback: callback, callbackError: callbackError, body: body);
        }

        [RequestEndpoint("/upgrade", RequestMethod.POST)]
        public void PostUpgrade(Requests.UpgradeRequest body, Action<Responses.BaseResponse<Responses.GetProfileResponse>> callback, Action<Exception> callbackError = null)
        {
            InvokeRequest(nameof(PostUpgrade), callback: callback, callbackError: callbackError, body: body);
        }

        [RequestEndpoint("/get_top_dao", RequestMethod.POST)]
        public void PostGetTopDao(Requests.BaseRequest body, Action<Responses.BaseResponse<List<Responses.DAOResponse>>> callback, Action<Exception> callbackError = null)
        {
            InvokeRequest(nameof(PostGetTopDao), callback: callback, callbackError: callbackError, body: body);
        }

        [RequestEndpoint("/get_dao", RequestMethod.POST)]
        public void PostGetDao(Requests.BaseRequest body, Action<Responses.BaseResponse<Responses.DAOResponse>> callback, Action<Exception> callbackError = null)
        {
            InvokeRequest(nameof(PostGetDao), callback: callback, callbackError: callbackError, body: body);
        }

        [RequestEndpoint("/join_to_dao", RequestMethod.POST)]
        public void PostJoinToDao(Requests.JoinToDaoRequest body, Action<Responses.BaseResponse<object>> callback, Action<Exception> callbackError = null)
        {
            InvokeRequest(nameof(PostJoinToDao), callback: callback, callbackError: callbackError, body: body);
        }

        [RequestEndpoint("/leave_dao", RequestMethod.POST)]
        public void PostLeaveDao(Requests.LeaveDaoRequest body, Action<Responses.BaseResponse<object>> callback, Action<Exception> callbackError = null)
        {
            InvokeRequest(nameof(PostLeaveDao), callback: callback, callbackError: callbackError, body: body);
        }

        [RequestEndpoint("/get_statistics", RequestMethod.POST)]
        public void PostGetStatistics(Requests.BaseRequest body, Action<Responses.BaseResponse<Responses.GetStatisticsResponse>> callback, Action<Exception> callbackError = null)
        {
            InvokeRequest(nameof(PostGetStatistics), callback: callback, callbackError: callbackError, body: body);
        }


        [RequestEndpoint("/get_booster_config", RequestMethod.GET)]
        public void GetBoosterConfig(Requests.BaseRequest body, Action<Responses.BaseResponse<List<Responses.GetConfigResponse>>> callback, Action<Exception> callbackError = null)
        {
            InvokeRequest(nameof(GetBoosterConfig), callback: callback, callbackError: callbackError, body: body);
        }

        [RequestEndpoint("/set_active_card", RequestMethod.POST)]
        public void PostSetActiveCard(Requests.SetActiveCardRequest body, Action<Responses.BaseResponse<Responses.ActiveCardDataResponse>> callback, Action<Exception> callbackError = null)
        {
            InvokeRequest(nameof(PostSetActiveCard), callback: callback, callbackError: callbackError, body: body);
        }

        [RequestEndpoint("/merge_cards", RequestMethod.POST)]
        public void PostMergeCards(Requests.MergeCardsRequest body, Action<Responses.BaseResponse<Responses.MergeCardsResponse>> callback, Action<Exception> callbackError = null)
        {
            InvokeRequest(nameof(PostMergeCards), callback: callback, callbackError: callbackError, body: body);
        }

        [RequestEndpoint("/check_merge", RequestMethod.POST)]
        public void PostCheckMerge(Requests.MergeCardsRequest body, Action<Responses.BaseResponse<Responses.CheckMergeResponse>> callback, Action<Exception> callbackError = null)
        {
            InvokeRequest(nameof(PostCheckMerge), callback: callback, callbackError: callbackError, body: body);
        }

        [RequestEndpoint("/open_box", RequestMethod.POST)]
        public void PostOpenBox(Requests.OpenBoxRequest body, Action<Responses.BaseResponse<Responses.OpenBoxResponse>> callback, Action<Exception> callbackError = null)
        {
            InvokeRequest(nameof(PostOpenBox), callback: callback, callbackError: callbackError, body: body);
        }
    }
}