using System;
using UnityEngine;

[Serializable]
public class FarmPopupModel 
{
    public ReactiveProperty<CardState> Card = new ReactiveProperty<CardState>();
}
