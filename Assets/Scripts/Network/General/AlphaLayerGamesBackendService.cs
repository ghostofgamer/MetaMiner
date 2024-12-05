using System.Collections.Generic;
using System;
using MetaMiners.Network.Core;
using MetaMiners.Network.Core.Attributes;

namespace MetaMiners.Network
{
    public class AlphaLayerGamesBackendService : BackendServiceProxy
    {
        protected override void Awake()
        {
            base.Awake();
            CreateOwnDataStorage("user-data");
        }

        [RequestEndpoint("/authorization", RequestMethod.POST)]
        public void Authorization(Action<AuthResponse> callback, Action<Exception> callbackError = null)
        {
            // Do something with the data if necessary...

            // Invoke request
            InvokeRequest(nameof(Authorization), callback: callback, callbackError: callbackError);
        }

        [RequestEndpoint("/get_profile", RequestMethod.GET)]
        [HeaderRequired("Authorization", canGetFromDataStorage: true)]
        public void GetProfile(Action<UserProfileResponse> callback, Dictionary<string, string> headers = null, Action<Exception> callbackError = null)
        {
            InvokeRequest(nameof(GetProfile), callback: callback, callbackError: callbackError, headers: headers);
        }

        [RequestEndpoint("/reward", RequestMethod.POST)]
        [HeaderRequired("Authorization", canGetFromDataStorage: true)]
        public void PostReward(RewardData body, Dictionary<string, string> headers = null, Action<string> callback = null, Action<Exception> callbackError = null)
        {
            InvokeRequest<string>(nameof(PostReward), callback: callback, callbackError: callbackError, headers: headers, body: body);
        }

        [RequestEndpoint("/spend", RequestMethod.POST)]
        [HeaderRequired("Authorization", canGetFromDataStorage: true)]
        public void PostSpend(SpendData body, Dictionary<string, string> headers = null, Action<string> callback = null, Action<Exception> callbackError = null)
        {
            InvokeRequest(nameof(PostSpend), callback: callback, callbackError: callbackError, headers: headers, body: body);
        }

        [Serializable]
        public struct AuthResponse
        {
            public string token;
        }

        [Serializable]
        public struct UserProfileResponse
        {
            public string username;
            public int balance;
            public int score;
            public int last_score;
            public int max_reward;
            public DateTime last_reward_timestamp;
        }

        [Serializable]
        public struct RewardData
        {
            public int reward;
            public int score;
            public DateTime reward_timestamp;
        }

        [Serializable]
        public struct SpendData
        {
            public int amount;
        }
    }
}