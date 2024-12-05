using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AppModel
{
    public class Config
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int BoosterLevel { get; set; }
        public int Power { get; set; }
        public int Electricity { get; set; }
        public int BatteryLimit { get; set; }
        public int UpgradeCost { get; set; }
        public int PowerElectricityOutcome { get; set; }
    }

    public List<Config> Configs { get; set; } = new List<Config>();
}
