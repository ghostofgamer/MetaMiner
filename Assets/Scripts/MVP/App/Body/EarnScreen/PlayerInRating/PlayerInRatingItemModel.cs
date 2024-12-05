using System;
using UnityEngine;

[Serializable]
public class PlayerInRatingItemModel
{
    public class PlayerInRatingState
    {
        public PlayerInRatingState(string nickname, int friendsInvited)
        {
            Nickname = nickname;
            FriendsInvited = friendsInvited;
        }

        public string Nickname { get; private set; }
        public int FriendsInvited { get; private set; }
        public Sprite ProfilePicture { get; private set; }

    }

    public ReactiveProperty<int> Rating = new ReactiveProperty<int>();
    public ReactiveProperty<string> Nickname = new ReactiveProperty<string>();
    public ReactiveProperty<Sprite> ProfilePicture = new ReactiveProperty<Sprite>();
    public ReactiveProperty<int> FriendsInvited = new ReactiveProperty<int>();
}
