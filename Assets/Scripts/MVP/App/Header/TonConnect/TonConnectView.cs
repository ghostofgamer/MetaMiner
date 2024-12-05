using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TonConnectView : MonoBehaviour
{
    [SerializeField]
    private GameObject connectWalletButton, walletAddressButton;

    [SerializeField]
    private TextMeshProUGUI walletAddress;

    private void Start()
    {
        SetButtonActive(true);
    }

    public void SetButtonActive(bool active)
    {
        connectWalletButton.SetActive(active);
        walletAddressButton.SetActive(!active);
    }

    public void SetWalletAddress(string wallet)
    {
        walletAddress.text = ProcessWalletAddress(wallet);
    }

    private string ProcessWalletAddress(string address)
    {
        if (address.Length < 8) return address;

        string firstFourChars = address[..6];
        string lastFourChars = address[^5..];

        return firstFourChars + "..." + lastFourChars;
    }
    
    public void OpenPopup()
    {
        AppPresenter.Instance.PopupsPresenter.ShowPopup(PopupsModel.Popups.Wallet);
    }

    public void ConnectTon()
    {
        TonConnectHandlerJS.Instance.OpenTonConnect();
    }
}
