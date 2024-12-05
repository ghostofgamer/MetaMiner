
using System;

[Serializable]
public class TaskPopupModel
{
    public ReactiveProperty<string> Caption = new ReactiveProperty<string>();
    public ReactiveProperty<string> IconCode = new ReactiveProperty<string>();
    public ReactiveProperty<string> Link = new ReactiveProperty<string>();
}
