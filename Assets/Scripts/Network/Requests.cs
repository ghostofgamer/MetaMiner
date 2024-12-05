using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;

namespace MetaMiners.Network
{
    public static class Requests
    {
        public class BaseRequest
        {
            public BaseRequest(string authData)
            {
                AuthData = authData;
            }

            [JsonProperty("authData")]
            public string AuthData { get; set; }
        }

        public class OpenBoxRequest : BaseRequest
        {
            public OpenBoxRequest(string authData, int boxId) : base(authData)
            {
                AuthData = authData;
                BoxId = boxId;
            }

            [JsonProperty("box_id")]
            public int BoxId { get; set; }
        }


        public class MergeCardsRequest : BaseRequest
        {
            public MergeCardsRequest(string authData, List<int> cardIds) : base(authData)
            {
                AuthData = authData;
                CardIds = cardIds;
            }

            [JsonProperty("card_ids")]
            public List<int> CardIds { get; set; }
        }

        public class SetActiveCardRequest : BaseRequest
        {
            public SetActiveCardRequest(string authData, int cardId) : base(authData)
            {
                AuthData = authData;
                CardId = cardId;
            }

            [JsonProperty("card_id")]
            public int CardId { get; set; }
        }

        public class ClickRequest : BaseRequest
        {
            public ClickRequest(string authData, int count) : base(authData)
            {
                AuthData = authData;
                Count = count;
            }

            [JsonProperty("count")]
            public int Count { get; set; }
        }

        public class UpgradeRequest : BaseRequest
        {
            public UpgradeRequest(string authData, string boosterType) : base(authData)
            {
                AuthData = authData;
                BoosterType = boosterType;
            }

            [JsonProperty("booster_type")]
            public string BoosterType { get; set; }
        }

        public class JoinToDaoRequest : BaseRequest
        {
            public JoinToDaoRequest(string authData, int communityId) : base(authData)
            {
                AuthData = authData;
                CommunityId = communityId;
            }

            [JsonProperty("community_id")]
            public int CommunityId { get; set; }
        }

        public class LeaveDaoRequest : BaseRequest
        {
            public LeaveDaoRequest(string authData, int communityId) : base(authData)
            {
                AuthData = authData;
                CommunityId = communityId;
            }

            [JsonProperty("community_id")]
            public int CommunityId { get; set; }
        }
    }
}