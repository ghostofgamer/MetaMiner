using System;
using UnityEngine;

[Serializable]
public class MergeScreenModel
{
    public ReactiveProperty<int> SelectedItem = new ReactiveProperty<int>();
}
