using TMPro;
using UnityEngine;

public class WalletPopupView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI usdtBalanceText;

    [SerializeField]
    private TextMeshProUGUI mmcBalanceText;

    public void ShowUSDTBalance(float balance)
    {
        usdtBalanceText.text = $"{balance} USDT";
    }

    public void ShowMMCBalance(int balance)
    {
        mmcBalanceText.text = $"{balance} MMC";
    }
}
