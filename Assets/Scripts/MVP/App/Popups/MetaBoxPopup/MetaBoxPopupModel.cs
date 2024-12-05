using System;

[Serializable]
public class MetaBoxPopupModel
{
    public ReactiveProperty<BoxState> Box { get; set; } = new ReactiveProperty<BoxState>();
}
