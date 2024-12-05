
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MergeProbabilityPopupModel
{
    public ReactiveProperty<List<CardState>> CardsToMerge = new ReactiveProperty<List<CardState>>();
    public ReactiveProperty<CardState> NewCard = new ReactiveProperty<CardState>();
    public ReactiveProperty<int> Probability = new ReactiveProperty<int>();
}
