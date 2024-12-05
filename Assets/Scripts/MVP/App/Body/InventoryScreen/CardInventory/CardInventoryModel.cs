using System;
using UnityEngine;

[Serializable]
public class CardInventoryModel
{
    public ReactiveProperty<CardState> Card = new ReactiveProperty<CardState>();
}
