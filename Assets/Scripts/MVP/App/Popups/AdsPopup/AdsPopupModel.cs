using System;
using UnityEngine;

[Serializable]
public class AdsPopupModel 
{
    public ReactiveProperty<int> AdsRemains = new ReactiveProperty<int>();
    public ReactiveProperty<float> USDTBalance = new ReactiveProperty<float>();
}
