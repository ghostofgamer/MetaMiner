using System;
using UnityEngine;

[Serializable]
public class DAOInfoPopupModel
{
    public ReactiveProperty<Sprite> DAOPicture = new ReactiveProperty<Sprite>();
    public ReactiveProperty<DAOState> DAO = new ReactiveProperty<DAOState>();
}
