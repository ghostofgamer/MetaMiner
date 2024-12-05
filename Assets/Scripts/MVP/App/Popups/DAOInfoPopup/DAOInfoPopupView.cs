using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DAOInfoPopupView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI nameText;

    [SerializeField]
    private TextMeshProUGUI peoplesCountText;

    [SerializeField]
    private TextMeshProUGUI percentSummaryBalanceText;

    [SerializeField]
    private Image daoPictureImage;

    [SerializeField]
    private AspectRatioFitter daoPictureFitter;

    public void ShowDAOPicture(Sprite daoPicture)
    {
        if (daoPicture == null) return;

        daoPictureImage.sprite = daoPicture;
        daoPictureFitter.aspectMode = AspectRatioFitter.AspectMode.EnvelopeParent;
        daoPictureFitter.aspectRatio = daoPicture.rect.width / daoPicture.rect.height;
    }

    public void ShowDAO(DAOState dao)
    {
        nameText.text = dao.Name;
        peoplesCountText.text = $"{dao.PeopleCount} peoples";

        if (string.IsNullOrWhiteSpace(dao.PercentSummaryBalance))
            percentSummaryBalanceText.text = $"0% of the game";
        else
            percentSummaryBalanceText.text = $"{dao.PercentSummaryBalance}% of the game";
    }
}
