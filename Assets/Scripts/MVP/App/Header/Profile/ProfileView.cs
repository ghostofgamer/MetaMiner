using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI nicknameText;

    [SerializeField]
    private TextMeshProUGUI statusText;

    [SerializeField]
    private Image profilePictureImage;

    [SerializeField]
    private AspectRatioFitter profilePictureFitter;

    public void ShowNickname(string nickname)
    {
        nicknameText.text = nickname;
    }

    public void ShowStatus(int status)
    {
        switch (status)
        {
            case 1:
                statusText.color = Color.white;
                statusText.text = "Status: Miner";
                break;
            case 2:
                Color32 color = new Color32(236, 148, 55, 255);
                statusText.color = color;
                statusText.text = "Status: VIP";
                break;
            default:
                statusText.color = Color.white;
                statusText.text = string.Empty;
                break;
        }
    }

    public void ShowProfilePicture(Sprite profilePicture)
    {
        if (profilePicture == null) return;

        profilePictureImage.sprite = profilePicture;

        profilePictureFitter.aspectMode = AspectRatioFitter.AspectMode.EnvelopeParent;
        profilePictureFitter.aspectRatio = profilePicture.rect.width / profilePicture.rect.height;
    }
}
