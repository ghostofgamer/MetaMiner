using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPresenter : MonoBehaviour
{
    [SerializeField]
    private BodyModel model;

    [SerializeField]
    private BodyView view;

    [field: Header("Screens")]
    [field: SerializeField]
    public MineScreenPresenter MineScreenPresenter { get; private set; }

    [field: SerializeField]
    public InventoryScreenPresenter InventoryScreenPresenter { get; private set; }

    [field: SerializeField]
    public UpgradeScreenPresenter UpgradeScreenPresenter { get; private set; }

    [field: SerializeField]
    public EarnScreenPresenter EarnScreenPresenter { get; private set; }

    [field: SerializeField]
    public DAOScreenPresenter DAOScreenPresenter { get; private set; }

    [field: SerializeField]
    public MergeScreenPresenter MergeScreenPresenter { get; private set; }

    [field: SerializeField]
    public MetaBoxOpenScreenPresenter MetaBoxOpenScreenPresenter { get; private set; }

    private void Awake()
    {
        model.ActiveScreen.Subscribe(screen => view.ShowScreen(screen));
    }

    private void Start()
    {
        StartCoroutine(RestoreEnergy());
    }

    public void SetScreen(BodyModel.Screens screen)
    {
        model.ActiveScreen.Value = screen;
    }

    public void OpenUpgrade()
    {
        model.ActiveScreen.Value = BodyModel.Screens.Upgrade;
    }

    public void OpenInventory()
    {
        model.ActiveScreen.Value = BodyModel.Screens.Inventory;
    }

    public IEnumerator RestoreEnergy()
    {
        while (true)
        {
            yield return new WaitForSeconds(1); // Задержка на 1 секунду

            if (MineScreenPresenter.model.Energy + MineScreenPresenter.model.Electricity < MineScreenPresenter.model.MaxEnergy)
            {
                MineScreenPresenter.model.Energy.Value += MineScreenPresenter.model.Electricity;
            }
            else
            {
                MineScreenPresenter.model.Energy.Value = MineScreenPresenter.model.MaxEnergy;
            }
        }
    }

  
}
