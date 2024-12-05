using System;
using UnityEngine;

[Serializable]
public class BoxState : IInventoryItem
{
    public BoxState(
        string id, string owner, string type, string mintedIn,
        string commonChance, string rareChance, string epicChance, string legendaryChance,
        string premiumPassChance, string upgradeCharacterChance, string usdtChance,
        int coinsMin, int coinsMax, string usdtMin, string usdtMax,
        int upgradePerkMin, int upgradePerkMax, string nothingChance)
    {
        Id = id;
        Owner = owner;
        Type = type;
        MintedIn = mintedIn;
        CommonChance = commonChance;
        RareChance = rareChance;
        EpicChance = epicChance;
        LegendaryChance = legendaryChance;
        PremiumPassChance = premiumPassChance;
        UpgradeCharacterChance = upgradeCharacterChance;
        UsdtChance = usdtChance;
        CoinsMin = coinsMin;
        CoinsMax = coinsMax;
        UsdtMin = usdtMin;
        UsdtMax = usdtMax;
        UpgradePerkMin = upgradePerkMin;
        UpgradePerkMax = upgradePerkMax;
        NothingChance = nothingChance;
    }

    public string ItemType { get => "box"; }

    [field: SerializeField]
    public string Id { get; set; }

    [field: SerializeField]
    public string Owner { get; set; }

    [field: SerializeField]
    public string Type { get; set; }

    [field: SerializeField]
    public string MintedIn { get; set; }

    [field: SerializeField]
    public string CommonChance { get; set; }

    [field: SerializeField]
    public string RareChance { get; set; }

    [field: SerializeField]
    public string EpicChance { get; set; }

    [field: SerializeField]
    public string LegendaryChance { get; set; }

    [field: SerializeField]
    public string PremiumPassChance { get; set; }

    [field: SerializeField]
    public string UpgradeCharacterChance { get; set; }

    [field: SerializeField]
    public string UsdtChance { get; set; }

    [field: SerializeField]
    public int CoinsMin { get; set; }

    [field: SerializeField]
    public int CoinsMax { get; set; }

    [field: SerializeField]
    public string UsdtMin { get; set; }

    [field: SerializeField]
    public string UsdtMax { get; set; }

    [field: SerializeField]
    public int UpgradePerkMin { get; set; }

    [field: SerializeField]
    public int UpgradePerkMax { get; set; }

    [field: SerializeField]
    public string NothingChance { get; set; }
}
