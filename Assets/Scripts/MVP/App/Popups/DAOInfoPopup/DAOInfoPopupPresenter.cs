using MetaMiners.Network;
using UnityEngine;

public class DAOInfoPopupPresenter : MonoBehaviour 
{
    [SerializeField]
    private DAOInfoPopupModel model;

    [SerializeField]
    private DAOInfoPopupView view;

    private void Awake()
    {
        model.DAO.Subscribe(dao => view.ShowDAO(dao));
        model.DAOPicture.Subscribe(picture => view.ShowDAOPicture(picture));
    }

    public void SetDAO(DAOState state)
    {
        model.DAO.Value = state;
    }

    public void SetDAOPicture(Sprite picture)
    {
        model.DAOPicture.Value = picture;
    }

    public void OpenDAOChatLink()
    {
        Application.OpenURL(model.DAO.Value.CommunityChatLink);
    }

    public void JoinToCommunity()
    {
        NetworkManager.Instance.PostJoinToDao(int.Parse(model.DAO.Value.Id));
    }
}
