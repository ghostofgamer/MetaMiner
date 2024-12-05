using System;
using UnityEngine;


[Serializable]
public class CardState : IInventoryItem
{
    public CardState(string id, string type, int level, int powerLevel, int electricityLevel, int batteryLevel, int energyAvailable, bool hasAutoMining)
    {
        Id = id;
        Type = type;
        Level = level;
        PowerLevel = powerLevel;
        ElectricityLevel = electricityLevel;
        BatteryLevel = batteryLevel;
        EnergyAvailable = energyAvailable;
        HasAutoMining = hasAutoMining;
    }
    public string ItemType { get => "farm"; }

    [field: SerializeField]
    public string Id { get; set; }

    [field: SerializeField]
    public string Type { get; set; }

    [field: SerializeField]
    public int Level { get; private set; }

    [field: SerializeField]
    public int PowerLevel { get; private set; }

    [field: SerializeField]
    public int ElectricityLevel { get; private set; }

    [field: SerializeField]
    public int BatteryLevel { get; private set; }

    [field: SerializeField]
    public int EnergyAvailable { get; private set; }

    [field: SerializeField]
    public bool HasAutoMining { get; private set; }
}
