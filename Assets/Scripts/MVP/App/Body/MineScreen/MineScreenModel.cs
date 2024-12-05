using System;

[Serializable]
public class MineScreenModel
{
    public ReactiveProperty<int> FarmLevel = new ReactiveProperty<int>(0);
    public ReactiveProperty<string> FarmRarity = new ReactiveProperty<string>();
    public ReactiveProperty<int> MaxFarmLevel = new ReactiveProperty<int>(0);
    public ReactiveProperty<int> Upgrades = new ReactiveProperty<int>(0);
    public ReactiveProperty<int> MMCBalance = new ReactiveProperty<int>(0);
    public ReactiveProperty<int> Earnings = new ReactiveProperty<int>(0);
    public ReactiveProperty<int> Energy = new ReactiveProperty<int>(0);
    public ReactiveProperty<int> MaxEnergy = new ReactiveProperty<int>(0);
    public ReactiveProperty<int> ClickCountInTime = new ReactiveProperty<int>(0);
    public ReactiveProperty<int> Power = new ReactiveProperty<int>(0);
    public ReactiveProperty<int> PowerElectricityOutcome = new ReactiveProperty<int>(0);
    public ReactiveProperty<int> Electricity = new ReactiveProperty<int>(0);
    public ReactiveProperty<int> AdsAvailable = new ReactiveProperty<int>(0);
}
