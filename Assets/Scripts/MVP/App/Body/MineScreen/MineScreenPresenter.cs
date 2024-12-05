using MetaMiners.Network;
using System;
using System.Collections;
using UnityEngine;

public class MineScreenPresenter : MonoBehaviour
{
    [SerializeField]
    public MineScreenModel model;

    [SerializeField]
    private MineScreenView view;

    private Coroutine sendClicksCoroutine = null;
    private DateTime clickLastTime = DateTime.MinValue;

    private void Awake()
    {
        // Farm Level
        model.FarmLevel.Subscribe(() => view.ShowFarmLevel(model.FarmLevel, model.MaxFarmLevel));
        model.MaxFarmLevel.Subscribe(() => view.ShowFarmLevel(model.FarmLevel, model.MaxFarmLevel));

        // Upgrades
        model.Upgrades.Subscribe(() => view.ShowUpgrades(model.Upgrades, model.FarmLevel));
        model.FarmLevel.Subscribe(() => view.ShowUpgrades(model.Upgrades, model.FarmLevel));

        // MMCBalance
        model.MMCBalance.Subscribe(mmcBalance => view.ShowMMCBalance(mmcBalance));

        // Earnings
        model.Earnings.Subscribe(earnings => view.ShowEarnings(earnings));

        // Energy
        model.Energy.Subscribe(() => view.ShowEnergy(model.Energy, model.MaxEnergy));
        model.MaxEnergy.Subscribe(() => view.ShowEnergy(model.Energy, model.MaxEnergy));

        // Click
        model.ClickCountInTime.Subscribe(() => { clickLastTime = DateTime.Now; });
    }

    public void SetFarmLevel(int value)
    {
        model.FarmLevel.Value = value;
    }

    public void SetMaxFarmLevel(int value)
    {
        model.MaxFarmLevel.Value = value;
    }

    public void SetUpgrades(int value)
    {
        model.Upgrades.Value = value;
    }

    public void SetMMCBalance(int value)
    {
        model.MMCBalance.Value = value;
    }

    public void SetEarnings(int value)
    {
        model.Earnings.Value = value;
    }

    public void SetEnergy(int value)
    {
        model.Energy.Value = value;
    }

    public void SetMaxEnergy(int value)
    {
        model.MaxEnergy.Value = value;
    }

    public void SetPower(int value)
    {
        model.Power.Value = value;
    }

    public void SetPowerElectricityOutcome(int value)
    {
        model.PowerElectricityOutcome.Value = value;
    }

    public void SetElectricity(int value)
    {
        model.Electricity.Value = value;
    }

    public void SetAdsAvailable(int value)
    {
        model.AdsAvailable.Value = value;
    }

    public void SetFarmRarity(string value)
    {
        model.FarmRarity.Value = value;
    }

    public int GetAdsAvailable()
    {
        return model.AdsAvailable.Value;
    }

    public void CubeClicked()
    {
        if (model.Energy.Value - model.PowerElectricityOutcome <= 0)
        {
            return;
        }

        model.ClickCountInTime.Value += 1;
        view.SpawnClick(model.Power);
        clickLastTime = DateTime.Now;

        model.MMCBalance.Value += model.Power;
        model.Energy.Value -= model.PowerElectricityOutcome;

        if (sendClicksCoroutine == null)
        {
            sendClicksCoroutine = StartCoroutine(SendClicks());
        }
    }

    public void ShowAdsPopup()
    {
        AppPresenter.Instance.PopupsPresenter.ShowPopup(PopupsModel.Popups.Ads);
    }

    private IEnumerator SendClicks()
    {
        while ((DateTime.Now - clickLastTime).TotalSeconds < 1f)
        {
            yield return new WaitForSeconds(0.5f);
        }

        NetworkManager.Instance.PostClick(model.ClickCountInTime);
        model.ClickCountInTime.SetValueWithoutNotify(0);
        sendClicksCoroutine = null;
    }
}
