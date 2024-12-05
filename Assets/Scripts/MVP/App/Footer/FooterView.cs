using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FooterView : MonoBehaviour
{
    [SerializeField]
    private FooterPresenter presenter;

    private void SetScreen(BodyModel.Screens screen) => presenter.SetScreen(screen);

    public void ShowMine() => SetScreen(BodyModel.Screens.Mine);

    public void ShowMerge() => SetScreen(BodyModel.Screens.Merge);

    public void ShowMarket() => SetScreen(BodyModel.Screens.Market);

    public void ShowDAO() => SetScreen(BodyModel.Screens.DAO);

    public void ShowEarn() => SetScreen(BodyModel.Screens.Earn);
}
