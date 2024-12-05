using System;
using UnityEngine;

[Serializable]
public class UpgradeScreenModel
{
    public ReactiveProperty<int> Balance = new ReactiveProperty<int>();
    public ReactiveProperty<int> FarmLevel = new ReactiveProperty<int>();
    public ReactiveProperty<int> CurrentPowerTap = new ReactiveProperty<int>();
    public ReactiveProperty<int> CurrentBattery = new ReactiveProperty<int>();
    public ReactiveProperty<int> CurrentRestoreEnergySpeed = new ReactiveProperty<int>();

    public ReactiveProperty<int> PowerTapCost = new ReactiveProperty<int>();
    public ReactiveProperty<int> BatteryCost = new ReactiveProperty<int>();
    public ReactiveProperty<int> RestoreEnergySpeedCost = new ReactiveProperty<int>();

    public ReactiveProperty<int> UpdatedPowerTap = new ReactiveProperty<int>();
    public ReactiveProperty<int> UpdatedBattery = new ReactiveProperty<int>();
    public ReactiveProperty<int> UpdatedRestoreEnergySpeed = new ReactiveProperty<int>();

    public ReactiveProperty<int> AutoMiningCost = new ReactiveProperty<int>();
    public ReactiveProperty<bool> IsAutoMiningAvailable = new ReactiveProperty<bool>();
    public ReactiveProperty<bool> IsAutoMiningActive = new ReactiveProperty<bool>();
}
