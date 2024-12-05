using TMPro;
using UnityEngine;

public class AdsPopupView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI adsRemainText;

    [SerializeField]
    private TextMeshProUGUI usdtBalanceText;

    public void ShowAdsRemain(int count)
    {
        adsRemainText.text = $"{count}/20";
    }

    public void ShowUsdtBalance(float balance)
    {
        usdtBalanceText.text = $"{balance}";
    }
}
