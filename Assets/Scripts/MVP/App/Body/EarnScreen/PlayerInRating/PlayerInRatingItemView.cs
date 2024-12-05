using System.Collections;
using System.Collections.Generic;
using ThisOtherThing.UI.Shapes;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class PlayerInRatingItemView : MonoBehaviour
{
    [SerializeField]
    private Sprite top1Gradient, top2Gradient, top3Gradient;

    [SerializeField]
    private Image profilePictureImage;

    [SerializeField]
    private AspectRatioFitter profilePictureFitter;

    [SerializeField]
    private Rectangle panelOutline, profilePictureOutline;

    [SerializeField]
    private TextMeshProUGUI topText, nicknameText, friendsInvitedText;

    public void ShowNickname(string nickname)
    {
        nicknameText.text = nickname;
    }

    public void ShowTopRating(int rating)
    {
        switch (rating)
        {
            case 1:
                topText.text = "TOP 1";
                panelOutline.Sprite = top1Gradient;
                profilePictureOutline.Sprite = top1Gradient;
                break;
            case 2:
                topText.text = "TOP 2";
                panelOutline.Sprite = top2Gradient;
                profilePictureOutline.Sprite = top2Gradient;
                break;
            case 3:
                topText.text = "TOP 3";
                panelOutline.Sprite = top3Gradient;
                profilePictureOutline.Sprite = top3Gradient;
                break;
            default:
                topText.gameObject.SetActive(false);
                panelOutline.Sprite = null;
                profilePictureOutline.Sprite = null;
                break;
        }
    }

    public void ShowFriendsInvitedCount(int friendsInvited)
    {
        string formattedEarnAmount = string.Format("{0:N0}", friendsInvited).Replace(",", " ");
        friendsInvitedText.text = $"{formattedEarnAmount} friends invited";
    }

    public void ShowProfilePicture(Sprite profilePicture)
    {
        if (profilePicture == null) return;

        profilePictureImage.sprite = profilePicture;
        profilePictureFitter.aspectMode = AspectRatioFitter.AspectMode.EnvelopeParent;
        profilePictureFitter.aspectRatio = profilePicture.rect.width / profilePicture.rect.height;
    }
}
