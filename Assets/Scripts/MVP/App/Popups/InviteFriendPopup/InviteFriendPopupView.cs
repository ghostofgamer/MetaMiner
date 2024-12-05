using TMPro;
using UnityEngine;

public class InviteFriendPopupView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI inviteLinkText;

    public void ShowInviteLink(string inviteLink)
    {
        inviteLinkText.text = inviteLink;
    }
}
