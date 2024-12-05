using System;
using UnityEngine;

[Serializable]
public class BoxInventoryModel
{
    public ReactiveProperty<BoxState> Box = new ReactiveProperty<BoxState>();
}
