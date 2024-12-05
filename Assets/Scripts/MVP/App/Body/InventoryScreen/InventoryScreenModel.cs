using System;
using System.Collections.Generic;

[Serializable]
public class InventoryScreenModel
{
    public ReactiveProperty<List<BoxState>> Boxes = new ReactiveProperty<List<BoxState>>();
    public ReactiveProperty<List<CardState>> Cards = new ReactiveProperty<List<CardState>>();
    public ReactiveProperty<string> TypeSort = new ReactiveProperty<string>("all");
    public ReactiveProperty<string> RareSort = new ReactiveProperty<string>("all");
}
