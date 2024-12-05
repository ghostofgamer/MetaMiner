using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskItemView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI captionText;

    [SerializeField]
    private TextMeshProUGUI rewardText;

    [SerializeField]
    private Image iconImage;

    [SerializeField]
    private Toggle checkboxToggle;

    [SerializeField]
    private Button button;

    [SerializeField]
    private Sprite telegramSprite, youTubeSprite, xSprite, discordSprite, friendsSprite;

    [SerializeField]
    private Color telegramColor, youTubeColor, xColor, discordColor;

    public void ShowCaption(string caption)
    {
        captionText.text = caption;
    }

    public void ShowReward(string reward)
    {
        rewardText.text = reward;
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

    public void ShowIsCompleted(bool isCompleted)
    {
        checkboxToggle.isOn = isCompleted;

        button.interactable = !isCompleted;
    }
}
