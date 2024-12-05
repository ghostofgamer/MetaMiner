using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DAOItemPresenter : MonoBehaviour
{
    [SerializeField]
    private DAOItemModel model;

    [SerializeField]
    private DAOItemView view;

    private void Awake()
    {
        model.DAO.Subscribe(dao => view.ShowDAO(dao));
    }

    public void SetDAO(DAOState state)
    {
        model.DAO.Value = state;
    }

    public void SetDAOPicture(Sprite picture)
    {
        model.DAOPicture.Value = picture;
    }

    public void OpenDAOInfoPopup()
    {
        AppPresenter.Instance.PopupsPresenter.DAOInfoPopupPresenter.SetDAO(model.DAO);
        AppPresenter.Instance.PopupsPresenter.DAOInfoPopupPresenter.SetDAOPicture(model.DAOPicture);
        AppPresenter.Instance.PopupsPresenter.ShowPopup(PopupsModel.Popups.DAOInfo);
    }
}
