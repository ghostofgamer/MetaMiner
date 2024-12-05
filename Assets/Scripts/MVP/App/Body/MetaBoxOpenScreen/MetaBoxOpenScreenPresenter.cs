using MetaMiners.Network;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MetaBoxOpenScreenPresenter : MonoBehaviour
{
    [SerializeField]
    private MetaBoxOpenScreenModel model;

    [SerializeField]
    private MetaBoxOpenScreenView view;

    [SerializeField]
    private MetaBoxRendererPresenter metaBoxRendererPresenter;

    private void Awake()
    {
        model.CurrentCard.Subscribe(() => metaBoxRendererPresenter.ShowNextCard(InvokeCardViewAnimation));
        model.Rewards.Subscribe(() => metaBoxRendererPresenter.SetCardsCount(model.Rewards.Value.Count));
    }

    public void SetReward(List<Responses.Reward> rewards)
    {
        var rewardList = rewards.Select(reward => new MetaBoxOpenScreenModel.RewardState(reward.Type, reward.Value, reward.CardParams != null ? new CardState(reward.CardParams.Id, reward.CardParams.Type, reward.CardParams.Level, reward.CardParams.PowerLevel, reward.CardParams.ElectricityLevel, reward.CardParams.BatteryLevel, reward.CardParams.EnergyAvailable, reward.CardParams.HasAutoMining) : null)).ToList();

        Debug.Log("Set rewards");
        model.Rewards.SetValueWithoutNotify(rewardList);
        model.Rewards.Notify();
        model.CurrentCard.SetValueWithoutNotify(-1);
    }

    public void OpenMetaBox()
    {
        StartCoroutine(OpenMetaBoxCoroutine());
    }

    private IEnumerator OpenMetaBoxCoroutine()
    {
        metaBoxRendererPresenter.ShowOpenAnimation();
        yield return new WaitForSeconds(4f);
        ShowNextCardOrHide();
    }

    public void ShowNextCardOrHide()
    {
        if (model.CurrentCard.Value + 1 < model.Rewards.Value.Count)
        {
            view.InvokeCardHideAnimation(model.CurrentCard.Value, NextCard);
        }
        else
        {
            metaBoxRendererPresenter.ShowIdleAnimation();
            CubeRendererPresenter.Instance.SetLevel(AppPresenter.Instance.BodyPresenter.MineScreenPresenter.model.FarmLevel);
            CubeRendererPresenter.Instance.SetRarity(AppPresenter.Instance.BodyPresenter.MineScreenPresenter.model.FarmRarity);
            AppPresenter.Instance.BodyPresenter.SetScreen(BodyModel.Screens.Mine);
            //NetworkManager.Instance.PostGetProfile();
        }
    }

    private void NextCard()
    {
        model.CurrentCard.Value++;

        var reward = model.Rewards.Value[model.CurrentCard.Value];

        switch (reward.Type)
        {
            case "coins":
                view.ShowMMC(reward.Value.HasValue ? reward.Value.Value : 0f);
                break;
            case "usdt":
                view.ShowUSDT(reward.Value.HasValue ? reward.Value.Value : 0f);
                break;
            case "farm":
                view.ShowCard(reward.CardParams);
                CubeRendererPresenter.Instance.SetLevel(reward.CardParams.Level);
                CubeRendererPresenter.Instance.SetRarity(reward.CardParams.Type);
                CubeRendererPresenter.Instance.SetToDefaultRotation();
                break;
        }
    }

    private void InvokeCardViewAnimation()
    {
        if (model.Rewards.Value.Count < 0) return;

        view.InvokeCardShowAnimation();
    }
}
