using System;
using UnityEngine;

[Serializable]
public class WalletPopupModel
{
    public ReactiveProperty<float> USDTBalance = new ReactiveProperty<float>();
    public ReactiveProperty<int> MMCBalance = new ReactiveProperty<int>();
}
