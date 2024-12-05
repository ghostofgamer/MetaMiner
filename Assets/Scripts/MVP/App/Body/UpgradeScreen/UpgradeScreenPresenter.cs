using MetaMiners.Network;
using UnityEngine;

public class UpgradeScreenPresenter : MonoBehaviour
{
    [SerializeField]
    private UpgradeScreenModel model;

    [SerializeField]
    private UpgradeScreenView view;

    private void Awake()
    {
        model.Balance.Subscribe(balance => view.ShowBalance(balance));
        model.FarmLevel.Subscribe(level => view.ShowFarmLevel(level));
        model.CurrentPowerTap.Subscribe(powerTap => view.ShowCurrentPowerTap(powerTap));
        model.CurrentBattery.Subscribe(battery => view.ShowCurrentBattery(battery));
        model.CurrentRestoreEnergySpeed.Subscribe(speed => view.ShowCurrentRestoreEnergySpeed(speed));
        model.PowerTapCost.Subscribe(cost => view.ShowPowerTapCost(cost));
        model.BatteryCost.Subscribe(cost => view.ShowBatteryCost(cost));
        model.RestoreEnergySpeedCost.Subscribe(cost => view.ShowRestoreEnergySpeedCost(cost));
        model.UpdatedPowerTap.Subscribe(powerTap => view.ShowUpdatedPowerTap(powerTap));
        model.UpdatedBattery.Subscribe(battery => view.ShowUpdatedBattery(battery));
        model.UpdatedRestoreEnergySpeed.Subscribe(speed => view.ShowUpdatedRestoreEnergySpeed(speed));
        model.AutoMiningCost.Subscribe(cost => view.ShowAutoMiningCost(cost));
        model.IsAutoMiningAvailable.Subscribe(isAvailable => view.ShowAutoMiningState(isAvailable, model.IsAutoMiningActive));
        model.IsAutoMiningActive.Subscribe(isActive => view.ShowAutoMiningState(model.IsAutoMiningAvailable, isActive));
    }

    public void SetBalance(int balance) => model.Balance.Value = balance;
    public void SetFarmLevel(int level) => model.FarmLevel.Value = level;
    public void SetCurrentPowerTap(int powerTap) => model.CurrentPowerTap.Value = powerTap;
    public void SetCurrentBattery(int battery) => model.CurrentBattery.Value = battery;
    public void SetCurrentRestoreEnergySpeed(int speed) => model.CurrentRestoreEnergySpeed.Value = speed;
    public void SetPowerTapCost(int cost) => model.PowerTapCost.Value = cost;
    public void SetBatteryCost(int cost) => model.BatteryCost.Value = cost;
    public void SetRestoreEnergySpeedCost(int cost) => model.RestoreEnergySpeedCost.Value = cost;
    public void SetUpdatedPowerTap(int powerTap) => model.UpdatedPowerTap.Value = powerTap;
    public void SetUpdatedBattery(int battery) => model.UpdatedBattery.Value = battery;
    public void SetUpdatedRestoreEnergySpeed(int speed) => model.UpdatedRestoreEnergySpeed.Value = speed;
    public void SetAutoMiningCost(int cost) => model.AutoMiningCost.Value = cost;
    public void SetAutoMiningAvailable(bool isAvailable) => model.IsAutoMiningAvailable.Value = isAvailable;
    public void SetAutoMiningActive(bool isActive) => model.IsAutoMiningActive.Value = isActive;

    public void BuyAutoMining()
    {
        NetworkManager.Instance.PostUpgrade("auto_mining");
        //auto_mining
    }

    public void BuyBattery()
    {
        NetworkManager.Instance.PostUpgrade("battery_limit");

        // battery
    }

    public void BuyRestoreEnergy()
    {
        NetworkManager.Instance.PostUpgrade("electricity");

        // energy
    }

    public void BuyPowerTap()
    {
        NetworkManager.Instance.PostUpgrade("power");

        // power
    }
}
