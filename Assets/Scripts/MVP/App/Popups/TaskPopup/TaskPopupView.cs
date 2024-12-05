using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskPopupView : MonoBehaviour
{
    [SerializeField]
    private Sprite telegramSprite, youTubeSprite, xSprite, discordSprite, friendsSprite;

    [SerializeField]
    private Color telegramColor, youTubeColor, xColor, discordColor;

    [SerializeField]
    private TextMeshProUGUI headerText, captionText;

    [SerializeField]
    private Image iconImage;

    public void ShowCaption(string caption)
    {
        captionText.text = caption;
    }

    public void ShowHeader(string iconCode)
    {
        switch (iconCode)
        {
            case "tg":
                headerText.text = "Telegram";
                break;
            case "yt":
                headerText.text = "YouTube";
                break;
            case "x":
                headerText.text = "X";
                break;
            case "discord":
                headerText.text = "Discord";
                break;
            case "friend":
                headerText.text = "Friends";
                break;
            default:
                headerText.text = "";
                break;
        }
    }

    public void ShowIcon(string iconCode)
    {
        switch (iconCode)
        {
            case "tg":
                iconImage.sprite = telegramSprite;
                iconImage.color = telegramColor;
                break;
            case "yt":
                iconImage.sprite = youTubeSprite;
                iconImage.color = youTubeColor;
                break;
            case "x":
                iconImage.sprite = xSprite;
                iconImage.color = xColor;
                break;
            case "discord":
                iconImage.sprite = discordSprite;
                iconImage.color = discordColor;
                break;
            case "friend":
                iconImage.sprite = friendsSprite;
                iconImage.color = Color.white;
                break;
            default:
                iconImage.sprite = null;
                iconImage.color = new Color(0f, 0f, 0f, 0f);
                break;
        }
    }
}
