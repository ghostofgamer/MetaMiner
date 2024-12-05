using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;
using static MetaMiners.Network.Responses;
using static MetaMiners.Network.Requests;

namespace MetaMiners.Network
{
    public class NetworkManager : Singleton<NetworkManager>
    {
        private string authData = "query_id=AAFt4yIfAAAAAG3jIh9d9MS5&user=%7B%22id%22%3A522380141%2C%22first_name%22%3A%22Danil%22%2C%22last_name%22%3A%22%22%2C%22username%22%3A%22danil_kho%22%2C%22language_code%22%3A%22en%22%2C%22is_premium%22%3Atrue%2C%22allows_write_to_pm%22%3Atrue%7D&auth_date=1730405119&hash=7a731a410faec57e05236e5ac38737757375715095556f5c37fa0c1f86742b3b";

        [SerializeField]
        private MetaMinersBackendService backendService;

        private void Start()
        {
#if UNITY_EDITOR
            StartGame();
#endif
        }

        public void SetAuthData(string authData)
        {
            this.authData = authData;
            StartGame();
        }

        public void StartGame()
        {
            PostGetProfile();
            PostGetTopDao();
            PostGetStatistics();
            GetBoosterConfig();
        }

        private void OnError(Exception exception)
        {
            Debug.LogError($"<color=red>[Network: <b>Failure</b>]</color> {exception}");
        }

        private void ShowLogs<T>(BaseResponse<T> response)
        {
            if (response.Success)
            {
                Debug.Log($"<color=green>[Network: <b>Success</b>]</color> [{typeof(T).Name}]\n{JsonConvert.SerializeObject(response.Data)}");
            }
            else
            {
                Debug.LogError($"<color=red>[Network: <b>Failure</b>]</color> [{typeof(T).Name}]\n{response.ErrorDescription}");
            }
        }

        private void ShowLogs(BaseRequest request, string requestName = "")
        {
            Debug.Log($"<color=yellow>[Network: <b>Send</b>]</color> [{typeof(BaseRequest).Name}]{(string.IsNullOrWhiteSpace(requestName) ? "" : $" [{requestName}]")}\n{JsonConvert.SerializeObject(request)}");
        }

        public void PostGetProfile()
        {
            var body = new BaseRequest(authData: authData);
            ShowLogs(body, "/get_profile");
            backendService.PostGetProfile(body, callback: OnPostGetProfile, callbackError: OnError);
        }

        public void PostClick(int count)
        {
            var body = new ClickRequest(authData: authData, count);
            ShowLogs(body, "/click");
            backendService.PostClick(body, callback: OnPostClick, callbackError: OnError);
        }

        public void PostUpgrade(string boosterType)
        {
            var body = new UpgradeRequest(authData: authData, boosterType);
            ShowLogs(body, "/upgrade");
            backendService.PostUpgrade(body, callback: OnPostGetProfile, callbackError: OnError);
        }

        public void PostGetDao()
        {
            var body = new BaseRequest(authData: authData);
            ShowLogs(body, "/get_dao");
            backendService.PostGetDao(body, callback: OnPostGetDao, callbackError: OnError);
        }

        public void PostGetTopDao()
        {
            var body = new BaseRequest(authData: authData);
            ShowLogs(body, "/get_top_dao");
            backendService.PostGetTopDao(body, callback: OnPostGetTopDao, callbackError: OnError);
        }

        public void PostJoinToDao(int communityId)
        {
            var body = new JoinToDaoRequest(authData: authData, communityId: communityId);
            ShowLogs(body, "/join_to_dao");
            backendService.PostJoinToDao(body, callback: OnPostJoinToDao, callbackError: OnError);
        }

        public void PostLeaveDao(int communityId)
        {
            var body = new LeaveDaoRequest(authData: authData, communityId: communityId);
            ShowLogs(body, "/leave_dao");
            backendService.PostLeaveDao(body, callback: OnPostLeaveDao, callbackError: OnError);
        }

        public void PostGetStatistics()
        {
            var body = new BaseRequest(authData: authData);
            ShowLogs(body, "/get_statistics");
            backendService.PostGetStatistics(body, callback: OnPostGetStatistics, callbackError: OnError);
        }

        public void GetBoosterConfig()
        {
            var body = new BaseRequest(authData: authData);
            ShowLogs(body, "/get_booster_config");
            backendService.GetBoosterConfig(body, callback: OnGetBoosterConfig, callbackError: OnError);
        }

        public void PostSetActiveCard(int cardId)
        {
            var body = new SetActiveCardRequest(authData: authData, cardId: cardId);
            ShowLogs(body, "/set_active_card");
            backendService.PostSetActiveCard(body, callback: OnPostSetActiveCard, callbackError: OnError);
        }

        public void PostMergeCards(List<int> cardIds)
        {
            var body = new MergeCardsRequest(authData: authData, cardIds: cardIds);
            ShowLogs(body, "/merge_cards");
            backendService.PostMergeCards(body, callback: OnPostMergeCards, callbackError: OnError);
        }

        public void PostCheckMerge(List<int> cardIds)
        {
            var body = new MergeCardsRequest(authData: authData, cardIds: cardIds);
            ShowLogs(body, "/check_merge");
            backendService.PostCheckMerge(body, callback: OnPostCheckMerge, callbackError: OnError);
        }

        public void PostOpenBox(int boxId)
        {
            var body = new OpenBoxRequest(authData: authData, boxId: boxId);
            ShowLogs(body, "/open_box");
            backendService.PostOpenBox(body, callback: OnPostOpenBox, callbackError: OnError);
        }

        public void OnPostOpenBox(BaseResponse<OpenBoxResponse> response)
        {
            ShowLogs(response);

            if (!response.Success) return;

            Debug.Log("SetReward in Network");
            AppPresenter.Instance.BodyPresenter.MetaBoxOpenScreenPresenter.SetReward(response.Data.Rewards);
            AppPresenter.Instance.BodyPresenter.SetScreen(BodyModel.Screens.OpenMetaBox);
            AppPresenter.Instance.BodyPresenter.MetaBoxOpenScreenPresenter.OpenMetaBox();

        }

        public void OnPostCheckMerge(BaseResponse<CheckMergeResponse> response)
        {
            ShowLogs(response);

            if (!response.Success) return;

            List<CardState> cards = new List<CardState>();

            foreach (var item in AppPresenter.Instance.BodyPresenter.MergeScreenPresenter.Items)
            {
                CardState card = item.GetCard();
                if (card == null) return;

                cards.Add(card);
            }

            CardState cardState = new CardState("", response.Data.NewType, response.Data.NewLevel, 0, 0, 0, 0, false);

            AppPresenter.Instance.PopupsPresenter.MergeProbabilityPopupPresenter.SetCardsToMerge(cards);
            AppPresenter.Instance.PopupsPresenter.MergeProbabilityPopupPresenter.SetNewCard(cardState);
            AppPresenter.Instance.PopupsPresenter.MergeProbabilityPopupPresenter.SetProbability(Mathf.RoundToInt(response.Data.SuccessChance));

            AppPresenter.Instance.PopupsPresenter.ShowPopup(PopupsModel.Popups.MergeProbability);
        }

        public void OnPostMergeCards(BaseResponse<MergeCardsResponse> response)
        {
            ShowLogs(response);

            if (!response.Success) return;

            CardState cardState = new CardState(response.Data.Id, response.Data.Type, response.Data.Level, response.Data.PowerLevel, response.Data.ElectricityLevel, response.Data.BatteryLevel, response.Data.EnergyAvailable, response.Data.HasAutoMining);

            AppPresenter.Instance.PopupsPresenter.NewFarmPopupPresenter.SetCard(cardState);

            AppPresenter.Instance.PopupsPresenter.ShowPopup(PopupsModel.Popups.NewFarm);
            AppPresenter.Instance.BodyPresenter.MergeScreenPresenter.ClearAll();
        }

        public void OnPostSetActiveCard(BaseResponse<ActiveCardDataResponse> response)
        {
            ShowLogs(response);

            if (!response.Success) return;

            OnActiveCard(response.Data);
        }

        public void OnActiveCard(ActiveCardDataResponse activeCard)
        {
            if (activeCard == null) return;

            // Farm Level
            AppPresenter.Instance.BodyPresenter.MineScreenPresenter.SetFarmLevel(activeCard.Level);
            AppPresenter.Instance.BodyPresenter.MineScreenPresenter.SetMaxFarmLevel(5);
            AppPresenter.Instance.BodyPresenter.MineScreenPresenter.SetFarmRarity(activeCard.Type);

            // Upgrades
            AppPresenter.Instance.BodyPresenter.MineScreenPresenter.SetUpgrades(activeCard.BatteryLevel + activeCard.PowerLevel + activeCard.ElectricityLevel);

            // Power
            AppPresenter.Instance.BodyPresenter.MineScreenPresenter.SetPower(activeCard.CardConfig.Power);
            AppPresenter.Instance.BodyPresenter.MineScreenPresenter.SetPowerElectricityOutcome(activeCard.CardConfig.PowerElectricityOutcome);

            // Earnings
            AppPresenter.Instance.BodyPresenter.MineScreenPresenter.SetEarnings(activeCard.AmRewardAllTime / 12);

            // Energy
            AppPresenter.Instance.BodyPresenter.MineScreenPresenter.SetEnergy(activeCard.EnergyAvailable);
            AppPresenter.Instance.BodyPresenter.MineScreenPresenter.SetMaxEnergy(activeCard.CardConfig.BatteryLimit);

            // Electricity
            AppPresenter.Instance.BodyPresenter.MineScreenPresenter.SetElectricity(activeCard.CardConfig.Electricity);

            // Cube
            CubeRendererPresenter.Instance.SetRarity(activeCard.Type);
            CubeRendererPresenter.Instance.SetLevel(activeCard.Level);

            // UpgradeScreenPresenter
            AppPresenter.Instance.BodyPresenter.UpgradeScreenPresenter.SetFarmLevel(activeCard.Level);

            AppPresenter.Instance.BodyPresenter.UpgradeScreenPresenter.SetCurrentPowerTap(activeCard.CardConfig.Power);
            AppPresenter.Instance.BodyPresenter.UpgradeScreenPresenter.SetCurrentBattery(activeCard.CardConfig.BatteryLimit);
            AppPresenter.Instance.BodyPresenter.UpgradeScreenPresenter.SetCurrentRestoreEnergySpeed(activeCard.CardConfig.Electricity);

            AppPresenter.Instance.BodyPresenter.UpgradeScreenPresenter.SetPowerTapCost(activeCard.CardConfigNextLevel.UpgradePowerCost);
            AppPresenter.Instance.BodyPresenter.UpgradeScreenPresenter.SetBatteryCost(activeCard.CardConfigNextLevel.UpgradeBatteryCost);
            AppPresenter.Instance.BodyPresenter.UpgradeScreenPresenter.SetRestoreEnergySpeedCost(activeCard.CardConfigNextLevel.UpgradeElectricityCost);

            AppPresenter.Instance.BodyPresenter.UpgradeScreenPresenter.SetUpdatedPowerTap(activeCard.CardConfigNextLevel.Power);
            AppPresenter.Instance.BodyPresenter.UpgradeScreenPresenter.SetUpdatedBattery(activeCard.CardConfigNextLevel.BatteryLimit);
            AppPresenter.Instance.BodyPresenter.UpgradeScreenPresenter.SetUpdatedRestoreEnergySpeed(activeCard.CardConfigNextLevel.Electricity);

            AppPresenter.Instance.BodyPresenter.UpgradeScreenPresenter.SetAutoMiningCost(activeCard.CardConfigNextLevel.UpgradeAutoMiningCost);
            AppPresenter.Instance.BodyPresenter.UpgradeScreenPresenter.SetAutoMiningActive(activeCard.HasAutoMining);
            AppPresenter.Instance.BodyPresenter.UpgradeScreenPresenter.SetAutoMiningAvailable(activeCard.Level >= 2);
        }

        public void OnGetBoosterConfig(BaseResponse<List<GetConfigResponse>> response)
        {
            ShowLogs(response);

            if (response.Success)
                AppPresenter.Instance.SetConfigs(response.Data);
        }

        private void OnPostGetStatistics(BaseResponse<GetStatisticsResponse> response)
        {
            ShowLogs(response);

            if (!response.Success) return;

            AppPresenter.Instance.BodyPresenter.EarnScreenPresenter.ShowRating(response.Data.TopPlayers);
        }

        private void OnPostJoinToDao(BaseResponse<object> response)
        {
            ShowLogs(response);

            if (!response.Success) return;

            PostGetDao();
        }

        private void OnPostLeaveDao(BaseResponse<object> response)
        {
            ShowLogs(response);

            if (!response.Success) return;

            AppPresenter.Instance.BodyPresenter.DAOScreenPresenter.SetCurrentDAO(null);
        }

        private void OnPostClick(BaseResponse<ClickResponse> response)
        {
            ShowLogs(response);

            if (!response.Success) return;

            AppPresenter.Instance.BodyPresenter.MineScreenPresenter.SetMMCBalance(response.Data.Balance);
            AppPresenter.Instance.BodyPresenter.MineScreenPresenter.SetEnergy(response.Data.EnergyAvailable);

            // Upgrade Screen
            AppPresenter.Instance.BodyPresenter.UpgradeScreenPresenter.SetBalance(response.Data.Balance);
        }

        private void OnPostGetProfile(BaseResponse<GetProfileResponse> response)
        {
            ShowLogs(response);

            if (!response.Success) return;

            // Referral
            AppPresenter.Instance.PopupsPresenter.InviteFriendPopupPresenter.SetInviteLink(response.Data.MissionsStats.InviteLink);

            // Balance
            AppPresenter.Instance.BodyPresenter.MineScreenPresenter.SetMMCBalance(response.Data.User.Balance);
            AppPresenter.Instance.PopupsPresenter.AdsPopupPresenter.SetBalanceUSDT(float.Parse(response.Data.User.UsdtBalance));
            AppPresenter.Instance.PopupsPresenter.WalletPopupPresenter.SetUSDTBalance(float.Parse(response.Data.User.UsdtBalance));
            AppPresenter.Instance.PopupsPresenter.WalletPopupPresenter.SetMMCBalance(response.Data.User.Balance);

            // Profile
            AppPresenter.Instance.HeaderPresenter.ProfilePresenter.SetNickname(response.Data.User.Username);
            AppPresenter.Instance.HeaderPresenter.ProfilePresenter.SetStatus(response.Data.User.Status);
            AppPresenter.Instance.PopupsPresenter.AdsPopupPresenter.SetAdsRemains(response.Data.User.AdsAvailable);
            // HeaderPresenter.Instance.ProfilePresenter.ShowDAOPicture(/*activeCard.Data.User.*/);s

            // DAO
            if (response.Data.User.HasCommunity)
            {
                PostGetDao();
            }

            // Tasks
            AppPresenter.Instance.BodyPresenter.EarnScreenPresenter.ShowTasks(response.Data.MissionsStats.Missions.Items);
            AppPresenter.Instance.BodyPresenter.EarnScreenPresenter.SetEarnAmount(response.Data.MissionsStats.EarnedFromPeople);
            AppPresenter.Instance.BodyPresenter.EarnScreenPresenter.SetFriendCount(response.Data.MissionsStats.InvitedPeopleCount);

            // Upgrade
            AppPresenter.Instance.BodyPresenter.UpgradeScreenPresenter.SetBalance(response.Data.User.Balance);

            // Inventory
            AppPresenter.Instance.BodyPresenter.InventoryScreenPresenter.SetBoxes(response.Data.AllBoxes);
            AppPresenter.Instance.BodyPresenter.InventoryScreenPresenter.SetCards(response.Data.AllCards);

            // Ads
            AppPresenter.Instance.BodyPresenter.MineScreenPresenter.SetAdsAvailable(response.Data.User.AdsAvailable);

            OnActiveCard(response.Data.ActiveCard);
        }

        private void OnPostGetTopDao(BaseResponse<List<DAOResponse>> response)
        {
            ShowLogs(response);

            if (response.Success)
            {
                AppPresenter.Instance.BodyPresenter.DAOScreenPresenter.ShowTopDAO(response.Data);
            }
        }

        private void OnPostGetDao(BaseResponse<DAOResponse> response)
        {
            ShowLogs(response);

            if (response.Data != null)
            {
                AppPresenter.Instance.BodyPresenter.DAOScreenPresenter.SetCurrentDAO(response.Data);
            }
        }
    }
}