using MetaMiners.Network;
using Newtonsoft.Json;
using System;
using UnityEngine;

public class JSManager : Singleton<JSManager>
{
    public void AdComplete(string json)
    {
        try
        {
            var box = JsonConvert.DeserializeObject<Responses.BaseResponse<Responses.Box>>(json).Data;
            BoxState boxState = new BoxState(
            box.Id,
            box.Owner,
            box.Type,
            box.MintedIn,
            box.CommonChance,
            box.RareChance,
            box.EpicChance,
            box.LegendaryChance,
            box.PremiumPassChance,
            box.UpgradeCharacterChance,
            box.UsdtChance,
            box.CoinsMin,
            box.CoinsMax,
            box.UsdtMin,
            box.UsdtMax,
            box.UpgradePerkMin,
            box.UpgradePerkMax,
            box.NothingChance
            );

            AppPresenter.Instance.PopupsPresenter.NewMetaBoxPopupPresenter.SetBox(boxState);
            AppPresenter.Instance.PopupsPresenter.ShowPopup(PopupsModel.Popups.NewMetaBox);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }

    public void setLanguage(string lang)
    {
        Debug.Log($"Lang code: {lang}");
    }
}
