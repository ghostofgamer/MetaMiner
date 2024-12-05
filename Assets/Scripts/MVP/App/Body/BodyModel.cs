using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BodyModel
{
    [Serializable]
    public enum Screens
    {
        Mine,
        Upgrade,
        Inventory,
        Merge,
        Market,
        DAO,
        Earn,
        OpenMetaBox
    }

    [SerializeField]
    public ReactiveProperty<Screens> ActiveScreen = new ReactiveProperty<Screens>(Screens.Mine);
}
