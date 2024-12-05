using System;
using UnityEngine;

[Serializable]
public class NewMetaBoxPopupModel
{
    public ReactiveProperty<BoxState> Box { get; set; } = new ReactiveProperty<BoxState>();
}
