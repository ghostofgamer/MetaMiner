using MetaMiners.Network;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DAOScreenPresenter : MonoBehaviour
{
    [SerializeField]
    private DAOScreenModel model;

    [SerializeField]
    private DAOScreenView view;

    private void Awake()
    {
        model.CurrentDAO.Subscribe(dao => view.ShowCurrentDAO(dao));
        model.HasDAO.Subscribe(hasDAO => view.ChangeState(hasDAO));
    }

    public void SetCurrentDAO(Responses.DAOResponse daoResponse)
    {
        if (daoResponse == null)
        {
            model.HasDAO.Value = false;
            return;
        }

        DAOState dao = new DAOState(daoResponse.Id, daoResponse.Name, daoResponse.CommunityChatLink, daoResponse.PeopleCount, daoResponse.SummaryBalance, daoResponse.PercentSummaryBalance);

        model.HasDAO.Value = !string.IsNullOrWhiteSpace(dao.Id);

        model.CurrentDAO.Value = dao;
    }

    public void ShowTopDAO(List<Responses.DAOResponse> daoTopResponse)
    {
        var topList = daoTopResponse.Select(dao => new DAOState(dao.Id, dao.Name, dao.CommunityChatLink, dao.PeopleCount, dao.SummaryBalance, dao.PercentSummaryBalance)).ToList();

        view.ShowTopDAO(topList);
    }

    public void SetCurrentDAOPicture(Sprite picture)
    {
        model.DAOPicture.Value = picture;
    }

    public void LeaveDAO()
    {
        NetworkManager.Instance.PostLeaveDao(int.Parse(model.CurrentDAO.Value.Id));
    }

    public void OpenCommunityChat()
    {
        Application.OpenURL(model.CurrentDAO.Value.CommunityChatLink);
    }

    public void OpenCreateOwnDao()
    {
        Application.OpenURL("tg://resolve?domain=cb_world_bot&start=create_community");
    }

    public void InviteFriend()
    {
        //AppPresenter.Instance.PopupsPresenter.InviteFriendPopupPresenter.SetInviteLink("Let's help Dora find a referral link!");
        AppPresenter.Instance.PopupsPresenter.ShowPopup(PopupsModel.Popups.InviteFriend);
    }
}