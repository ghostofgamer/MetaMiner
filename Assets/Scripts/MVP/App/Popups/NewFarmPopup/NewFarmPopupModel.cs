using System;
using UnityEngine;

[Serializable]
public class NewFarmPopupModel 
{
    public ReactiveProperty<CardState> Card = new ReactiveProperty<CardState>();
}
