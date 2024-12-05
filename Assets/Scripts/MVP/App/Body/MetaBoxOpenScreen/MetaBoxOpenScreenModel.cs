using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MetaBoxOpenScreenModel
{
    public class RewardState
    {
        public RewardState(string type, float? value, CardState cardParams)
        {
            Type = type;
            Value = value;
            CardParams = cardParams;
        }

        public string Type { get; set; }

        public float? Value { get; set; }

        public CardState CardParams { get; set; }
    }

    public ReactiveProperty<List<RewardState>> Rewards = new ReactiveProperty<List<RewardState>>();
    public ReactiveProperty<int> CurrentCard = new ReactiveProperty<int>();
}
