using System.Runtime.InteropServices;
using UnityEngine;

public class AdsPopupPresenter : MonoBehaviour
{
    [DllImport("__Internal")]
    public static extern void showAds();

    [SerializeField]
    private AdsPopupModel model;

    [SerializeField]
    private AdsPopupView view;

    private void Awake()
    {
        model.AdsRemains.Subscribe(value => view.ShowAdsRemain(value));
        model.USDTBalance.Subscribe(value => view.ShowUsdtBalance(value));
    }

    public void SetAdsRemains(int value)
    {
        model.AdsRemains.Value = value;
    }

    public void SetBalanceUSDT(float value)
    {
        model.USDTBalance.Value = value;
    }

    public void WatchAds()
    {
        showAds();
    }
}
