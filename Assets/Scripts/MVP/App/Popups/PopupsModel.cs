using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PopupsModel 
{
    public enum Popups
    {
        None,
        DAOInfo,
        InviteFriend,
        Task,
        Farm,
        NewFarm,
        MergeProbability,
        MetaBox,
        Ads,
        NewMetaBox,
        Wallet,
    }

    //[SerializeField]
    //public ReactiveProperty<Popups> ActivePopup = new ReactiveProperty<Popups>(Popups.None);
}
