using UnityEngine;

public class InviteFriendPopupPresenter : MonoBehaviour
{
    [SerializeField]
    private InviteFriendPopupModel model;

    [SerializeField]
    private InviteFriendPopupView view;

    private void Awake()
    {
        model.CommunityLink.Subscribe(link => view.ShowInviteLink(link));        
    }

    public void SetInviteLink(string inviteLink)
    {
        model.CommunityLink.Value = inviteLink;
    }

    public void SendLink()
    {
        Debug.Log("TODO: Send invite link");
    }

    public void CopyLink()
    {
        Debug.Log("TODO: Copy invite link");
    }
}
