using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DAOScreenModel
{
    public ReactiveProperty<Sprite> DAOPicture = new ReactiveProperty<Sprite>();
    public ReactiveProperty<DAOState> CurrentDAO = new ReactiveProperty<DAOState>();
    public ReactiveProperty<bool> HasDAO = new ReactiveProperty<bool>();
}
