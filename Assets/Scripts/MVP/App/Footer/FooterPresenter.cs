using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FooterPresenter : MonoBehaviour
{
    [SerializeField]
    private FooterModel model;

    [SerializeField]
    private FooterView view;

    [SerializeField]
    private BodyPresenter bodyPresenter;

    public void SetScreen(BodyModel.Screens screen) => bodyPresenter.SetScreen(screen);
}
