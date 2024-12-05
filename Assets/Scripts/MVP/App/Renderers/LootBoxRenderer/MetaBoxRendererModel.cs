using System;
using UnityEngine;

[Serializable]
public class MetaBoxRendererModel
{
   public ReactiveProperty<bool> Open = new ReactiveProperty<bool>();   
   public ReactiveProperty<int> CardsCount = new ReactiveProperty<int>();   
}
