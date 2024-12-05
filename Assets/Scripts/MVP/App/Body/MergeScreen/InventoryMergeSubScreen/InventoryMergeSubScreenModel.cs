using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryMergeSubScreenModel
{
    public ReactiveProperty<List<CardState>> Cards = new ReactiveProperty<List<CardState>>();
}
