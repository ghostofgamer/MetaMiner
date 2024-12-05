using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRendererPresenter : Singleton<CubeRendererPresenter>
{
    [SerializeField]
    private CubeRendererModel model;

    [SerializeField]
    private CubeRendererView view;

    private void Awake()
    {
        model.Rarity.Subscribe(() => view.ShowCube(model.Rarity, model.Level));
        model.Level.Subscribe(() => view.ShowCube(model.Rarity, model.Level));
    }

    public void SetRarity(string rarity)
    {
        model.Rarity.Value = rarity;
    }

    public void SetLevel(int level)
    {
        model.Level.Value = level;
    }

    public void SetToDefaultRotation()
    {
        view.SetToDefaultRotation();
    }
}
