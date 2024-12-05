using MetaMiners.Network;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

public class AppPresenter : Singleton<AppPresenter>
{
    [SerializeField]
    private AppModel model;

    [SerializeField]
    private AppView view;

    [field: Header("References")]
    [field: SerializeField]
    public NetworkManager NetworkManager { get; private set; }

    [field: Header("Presenters")]

    [field: SerializeField]
    public HeaderPresenter HeaderPresenter { get; private set; }

    [field: SerializeField]
    public BodyPresenter BodyPresenter { get; private set; }

    [field: SerializeField]
    public FooterPresenter FooterPresenter { get; private set; }

    [field: SerializeField]
    public PopupsPresenter PopupsPresenter { get; private set; }

    private void Awake()
    {
        Debug.Log($"This is MetaMiners version {Application.version} with Unity version {Application.unityVersion}!");
    }

    public void SetConfigs(List<Responses.GetConfigResponse> responses)
    {
        var configList = responses.Select(config => new AppModel.Config() { 
            Id = config.Id, 
            Type = config.Type, 
            BoosterLevel = config.BoosterLevel, 
            Power = config.Power, 
            Electricity = config.Electricity, 
            BatteryLimit = config.BatteryLimit, 
            UpgradeCost = config.UpgradeCost, 
            PowerElectricityOutcome = config.PowerElectricityOutcome }).ToList();

        model.Configs = configList;
    }
    
    public AppModel.Config GetConfig(int boosterLevel, string type)
    {
        //Debug.Log($"Search for BoosterLevel = {boosterLevel} && Type == {type}");
        var config = model.Configs.Find(x => x.BoosterLevel == boosterLevel && x.Type == type);
        //Debug.Log($"Find for BoosterLevel = {boosterLevel} && Type == {type}: {JsonUtility.ToJson(config)}");

        return config;
    }
}
