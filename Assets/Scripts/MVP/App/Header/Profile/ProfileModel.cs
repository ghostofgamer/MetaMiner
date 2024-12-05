using System;
using UnityEngine;

[Serializable]
public class ProfileModel
{
    public ReactiveProperty<string> Nickname = new ReactiveProperty<string>();
    public ReactiveProperty<int> Status = new ReactiveProperty<int>();
    public ReactiveProperty<Sprite> ProfilePicture = new ReactiveProperty<Sprite>();
}
