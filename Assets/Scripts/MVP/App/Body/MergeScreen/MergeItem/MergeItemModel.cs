using System;
using UnityEngine;

[Serializable]
public class MergeItemModel
{
    public ReactiveProperty<CardState> Card = new ReactiveProperty<CardState>(null);
}
