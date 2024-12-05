using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupsPresenter : MonoBehaviour
{
    [SerializeField]
    private PopupsModel model;

    [SerializeField]
    private PopupsView view;

    [field: Header("Popups")]
    [field: SerializeField]
    public DAOInfoPopupPresenter DAOInfoPopupPresenter { get; private set; } 
    
    [field: SerializeField]
    public InviteFriendPopupPresenter InviteFriendPopupPresenter { get; private set; }

    [field: SerializeField]
    public TaskPopupPresenter TaskPopupPresenter { get; private set; }

    [field: SerializeField]
    public FarmPopupPresenter FarmPopupPresenter { get; private set; }

    [field: SerializeField]
    public MergeProbabilityPopupPresenter MergeProbabilityPopupPresenter { get; private set; }

    [field: SerializeField]
    public NewFarmPopupPresenter NewFarmPopupPresenter { get; private set; }

    [field: SerializeField]
    public MetaBoxPopupPresenter MetaBoxPopupPresenter { get; private set; }

    [field: SerializeField]
    public AdsPopupPresenter AdsPopupPresenter { get; private set; }

    [field: SerializeField]
    public NewMetaBoxPopupPresenter NewMetaBoxPopupPresenter { get; private set; }

    [field: SerializeField]
    public WalletPopupPresenter WalletPopupPresenter { get; private set; }

    private void Awake()
    {
        //model.ActivePopup.Subscribe(popup => view.ShowPopup(popup));
        HidePopups();
    }

    public void ShowPopup(PopupsModel.Popups popup)
    {
        view.ShowPopup(popup);
        //model.ActivePopup.SetValueWithoutNotify(popup);
        //model.ActivePopup.Notify();
    }

    public void HidePopups()
    {
        view.HidePopups();
        //view.ShowPopup(PopupsModel.Popups.None);
        //model.ActivePopup.Value = PopupsModel.Popups.None;
    }
}
