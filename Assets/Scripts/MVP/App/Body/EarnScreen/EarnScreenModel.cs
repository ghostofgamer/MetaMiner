using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EarnScreenModel
{
    public ReactiveProperty<int> FriendsCount = new ReactiveProperty<int>();
    public ReactiveProperty<int> EarnAmount = new ReactiveProperty<int>();
    public ReactiveProperty<string> InviteLink = new ReactiveProperty<string>();
}
