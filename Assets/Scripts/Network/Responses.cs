using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MetaMiners.Network
{
    public static class Responses
    {
        public class BaseResponse<T>
        {
            [JsonProperty("success")]
            public bool Success { get; set; }

            [JsonProperty("error_description")]
            public string ErrorDescription { get; set; }

            [JsonProperty("data")]
            public T Data { get; set; }
        }

        #region User Data Classes

        public class UserData
        {
            private string _id;

            [JsonProperty("id")]
            private string IdSetter
            {
                set { _id = value; }
            }

            [JsonProperty("user_id")]
            private string UserIdSetter
            {
                set { _id = value; }
            }

            public string Id => _id;

            [JsonProperty("username")]
            public string Username { get; set; }

            [JsonProperty("tg_id")]
            public string TgId { get; set; }

            [JsonProperty("balance")]
            public int Balance { get; set; }

            [JsonProperty("status")]
            public int Status { get; set; }

            [JsonProperty("refferer")]
            public object Refferer { get; set; }

            [JsonProperty("selected_card")]
            public int SelectedCard { get; set; }

            [JsonProperty("rating_score")]
            public int RatingScore { get; set; }

            [JsonProperty("rating_level")]
            public int RatingLevel { get; set; }

            [JsonProperty("rating_score_maximum")]
            public int RatingScoreMaximum { get; set; }

            [JsonProperty("referral_balance")]
            public int ReferralBalance { get; set; }

            [JsonProperty("has_community")]
            public bool HasCommunity { get; set; }

            [JsonProperty("usdt_balance")]
            public string UsdtBalance { get; set; }

            [JsonProperty("last_ads_view")]
            public DateTime LastAdsView { get; set; }

            [JsonProperty("ads_available")]
            public int AdsAvailable { get; set; }

            [JsonProperty("usdt_for_reward")]
            public double UsdtForReward { get; set; }
        }

        #endregion

        #region Card Classes

        public class Card
        {
            private string _id;

            [JsonProperty("id")]
            private string IdSetter
            {
                set { _id = value; }
            }

            [JsonProperty("card_id")]
            private string CardIdSetter
            {
                set { _id = value; }
            }

            public string Id => _id;

            private string _owner;

            [JsonProperty("owner")]
            private string OwnerSetter
            {
                set { _owner = value; }
            }

            [JsonProperty("card_owner")]
            private string CardOwnerSetter
            {
                set { _owner = value; }
            }

            public string Owner => _owner;

            private string _type;

            [JsonProperty("type")]
            private string TypeSetter
            {
                set { _type = value; }
            }

            [JsonProperty("card_type")]
            private string CardTypeSetter
            {
                set { _type = value; }
            }

            public string Type => _type;

            private int _level;

            [JsonProperty("level")]
            private int LevelSetter
            {
                set { _level = value; }
            }

            [JsonProperty("card_level")]
            private int CardLevelSetter
            {
                set { _level = value; }
            }

            public int Level => _level;

            [JsonProperty("power_level")]
            public int PowerLevel { get; set; }

            [JsonProperty("electricity_level")]
            public int ElectricityLevel { get; set; }

            [JsonProperty("battery_level")]
            public int BatteryLevel { get; set; }

            [JsonProperty("energy_available")]
            public int EnergyAvailable { get; set; }

            [JsonProperty("minted_in")]
            public string MintedIn { get; set; }

            [JsonProperty("has_auto_mining")]
            public bool HasAutoMining { get; set; }

            [JsonProperty("last_click_timestamp")]
            public DateTime? LastClickTimestamp { get; set; }

            [JsonProperty("last_auto_mining_timestamp")]
            public DateTime? LastAutoMiningTimestamp { get; set; }

            [JsonProperty("createdAt")]
            public DateTime? CreatedAt { get; set; }

            [JsonProperty("updatedAt")]
            public DateTime? UpdatedAt { get; set; }

            [JsonProperty("is_upgrading")]
            public bool IsUpgrading { get; set; }

            [JsonProperty("date_upgrade_start")]
            public DateTime? DateUpgradeStart { get; set; }

            [JsonProperty("date_upgrade_end")]
            public DateTime? DateUpgradeEnd { get; set; }
        }

        public class MergeCardsResponse : Card { }

        public class ActiveCardDataResponse : Card
        {
            [JsonProperty("card_config")]
            public CardConfig CardConfig { get; set; }

            [JsonProperty("card_config_next_level")]
            public CardConfig CardConfigNextLevel { get; set; }

            [JsonProperty("cardUpgradeConf")]
            public CardUpgradeConf CardUpgradeConf { get; set; }

            [JsonProperty("am_reward_all_time")]
            public int AmRewardAllTime { get; set; }

            [JsonProperty("is_am_claim_available")]
            public bool IsAmClaimAvailable { get; set; }

            [JsonProperty("auto_mining_reward_time")]
            public int AutoMiningRewardTime { get; set; }
        }

        public class CardUpgradeConf
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("card_level")]
            public int CardLevel { get; set; }

            [JsonProperty("card_type")]
            public string CardType { get; set; }

            [JsonProperty("usdt_price")]
            public string UsdtPrice { get; set; }

            [JsonProperty("time_in_seconds")]
            public int TimeInSeconds { get; set; }

            [JsonProperty("total_time_in_seconds")]
            public int TotalTimeInSeconds { get; set; }
        }

        public class CardConfig
        {
            [JsonProperty("power")]
            public int Power { get; set; }

            [JsonProperty("electricity")]
            public int Electricity { get; set; }

            [JsonProperty("battery_limit")]
            public int BatteryLimit { get; set; }

            [JsonProperty("upgrade_power_cost")]
            public int UpgradePowerCost { get; set; }

            [JsonProperty("upgrade_electricity_cost")]
            public int UpgradeElectricityCost { get; set; }

            [JsonProperty("upgrade_battery_cost")]
            public int UpgradeBatteryCost { get; set; }

            [JsonProperty("upgrade_auto_mining_cost")]
            public int UpgradeAutoMiningCost { get; set; }

            [JsonProperty("power_electricity_outcome")]
            public int PowerElectricityOutcome { get; set; }
        }

        #endregion

        #region Box Classes

        public class Box
        {
            private string _id;

            [JsonProperty("id")]
            private string IdSetter
            {
                set { _id = value; }
            }

            [JsonProperty("box_id")]
            private string BoxIdSetter
            {
                set { _id = value; }
            }

            public string Id => _id;

            [JsonProperty("owner")]
            public string Owner { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("minted_in")]
            public string MintedIn { get; set; }

            [JsonProperty("common_chance")]
            public string CommonChance { get; set; }

            [JsonProperty("rare_chance")]
            public string RareChance { get; set; }

            [JsonProperty("epic_chance")]
            public string EpicChance { get; set; }

            [JsonProperty("legendary_chance")]
            public string LegendaryChance { get; set; }

            [JsonProperty("premium_pass_chance")]
            public string PremiumPassChance { get; set; }

            [JsonProperty("upgrade_character_chance")]
            public string UpgradeCharacterChance { get; set; }

            [JsonProperty("usdt_chance")]
            public string UsdtChance { get; set; }

            [JsonProperty("coins_min")]
            public int CoinsMin { get; set; }

            [JsonProperty("coins_max")]
            public int CoinsMax { get; set; }

            [JsonProperty("usdt_min")]
            public string UsdtMin { get; set; }

            [JsonProperty("usdt_max")]
            public string UsdtMax { get; set; }

            [JsonProperty("upgrade_perk_min")]
            public int UpgradePerkMin { get; set; }

            [JsonProperty("upgrade_perk_max")]
            public int UpgradePerkMax { get; set; }

            [JsonProperty("nothing_chance")]
            public string NothingChance { get; set; }
        }

        #endregion

        #region Response Classes

        public class OpenBoxResponse
        {
            private string _boxId;

            [JsonProperty("box_id")]
            private string BoxIdSetter
            {
                set { _boxId = value; }
            }

            [JsonProperty("id")]
            private string IdSetter
            {
                set { _boxId = value; }
            }

            public string BoxId => _boxId;

            [JsonProperty("box_type")]
            public string BoxType { get; set; }

            [JsonProperty("reward")]
            public List<Reward> Rewards { get; set; }
        }

        public class Reward
        {
            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("value")]
            public float? Value { get; set; }

            [JsonProperty("card_params")]
            public Card CardParams { get; set; }
        }

        public class GetProfileResponse
        {
            [JsonProperty("user")]
            public UserData User { get; set; }

            [JsonProperty("active_card")]
            public ActiveCardDataResponse ActiveCard { get; set; }

            [JsonProperty("all_cards")]
            public List<Card> AllCards { get; set; }

            [JsonProperty("all_boxes")]
            public List<Box> AllBoxes { get; set; }

            [JsonProperty("missions_stats")]
            public MissionsStats MissionsStats { get; set; }
        }

        public class MergeCardsResponseWrapper : BaseResponse<MergeCardsResponse> { }

        public class CheckMergeResponse
        {
            [JsonProperty("newType")]
            public string NewType { get; set; }

            [JsonProperty("newLevel")]
            public int NewLevel { get; set; }

            [JsonProperty("mergeSuccess")]
            public bool MergeSuccess { get; set; }

            [JsonProperty("successChance")]
            public float SuccessChance { get; set; }
        }

        public class GetConfigResponse
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("booster_level")]
            public int BoosterLevel { get; set; }

            [JsonProperty("power")]
            public int Power { get; set; }

            [JsonProperty("electricity")]
            public int Electricity { get; set; }

            [JsonProperty("battery_limit")]
            public int BatteryLimit { get; set; }

            [JsonProperty("upgrade_cost")]
            public int UpgradeCost { get; set; }

            [JsonProperty("power_electricity_outcome")]
            public int PowerElectricityOutcome { get; set; }
        }

        public class GetStatisticsResponse
        {
            [JsonProperty("top_players")]
            public List<TopPlayer> TopPlayers { get; set; }
        }

        public class TopPlayer
        {
            [JsonProperty("username")]
            public string Username { get; set; }

            [JsonProperty("referral_count")]
            public int ReferralCount { get; set; }
        }

        public class ClickResponse
        {
            [JsonProperty("balance")]
            public int Balance { get; set; }

            [JsonProperty("energy_available")]
            public int EnergyAvailable { get; set; }

            [JsonProperty("current_rating_score")]
            public int CurrentRatingScore { get; set; }

            [JsonProperty("current_rating_level")]
            public int CurrentRatingLevel { get; set; }
        }

        public class DAOResponse
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("community_chat_link")]
            public string CommunityChatLink { get; set; }

            [JsonProperty("people_count")]
            public string PeopleCount { get; set; }

            [JsonProperty("summery_balance")]
            public string SummaryBalance { get; set; }

            [JsonProperty("percent_summery_balance")]
            public string PercentSummaryBalance { get; set; }
        }

        #endregion

        #region Missions Classes

        public class MissionsStats
        {
            [JsonProperty("invite_link")]
            public string InviteLink { get; set; }

            [JsonProperty("invited_people_count")]
            public int InvitedPeopleCount { get; set; }

            [JsonProperty("earned_from_people")]
            public int EarnedFromPeople { get; set; }

            [JsonProperty("missions")]
            public Missions Missions { get; set; }
        }

        public class Missions
        {
            [JsonProperty("Items")]
            public List<MissionItem> Items { get; set; }
        }

        public class MissionItem
        {
            [JsonProperty("completed")]
            public bool Completed { get; set; }

            [JsonProperty("caption")]
            public string Caption { get; set; }

            [JsonProperty("reward")]
            public string Reward { get; set; }

            [JsonProperty("icon")]
            public string Icon { get; set; }

            [JsonProperty("action")]
            public string Action { get; set; }

            [JsonProperty("link")]
            public string Link { get; set; }
        }

        #endregion
    }
}
