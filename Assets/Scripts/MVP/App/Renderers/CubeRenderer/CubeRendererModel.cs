using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class CubeRendererModel
{
    public ReactiveProperty<string> Rarity = new ReactiveProperty<string>();
    public ReactiveProperty<int> Level = new ReactiveProperty<int>();
}
