using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DAOItemModel
{
    public ReactiveProperty<Sprite> DAOPicture = new ReactiveProperty<Sprite>();
    public ReactiveProperty<DAOState> DAO = new ReactiveProperty<DAOState>();
}
