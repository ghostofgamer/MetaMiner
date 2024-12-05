using UnityEngine;

public class WalletPopupPresenter : MonoBehaviour
{
    [SerializeField]
    private WalletPopupModel model;

    [SerializeField]
    private WalletPopupView view;

    private void Awake()
    {
        model.USDTBalance.Subscribe(value => view.ShowUSDTBalance(value));
        model.MMCBalance.Subscribe(value => view.ShowMMCBalance(value));
    }

    public void SetUSDTBalance(float balance)
    {
        model.USDTBalance.SetValue(balance);
    }

    public void SetMMCBalance(int  balance)
    {
        model.MMCBalance.SetValue(balance);
    }
}
