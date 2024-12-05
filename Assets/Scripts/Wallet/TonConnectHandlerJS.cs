using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using IngameDebugConsole;
using System;

public class TonConnectHandlerJS : Singleton<TonConnectHandlerJS>
{
    [DllImport("__Internal")]
    private static extern void connectWallet();
    [DllImport("__Internal")]
    private static extern void disconnectWallet();
    [DllImport("__Internal")]
    private static extern void checkRestoreConnection();

    [SerializeField]
    private TonConnectView tonConnectView;

    private void Start()
    {
        DebugLogConsole.AddCommand("connect", "Connect Wallet", OpenTonConnect);
        DebugLogConsole.AddCommand("disconnect", "Disconnect Wallet", DisconnectTonConnect);

#if !UNITY_EDITOR
        checkRestoreConnection();
#endif
    }

    public void OpenTonConnect()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        connectWallet();
#else
        Debug.LogWarning("Ton Connect only works in WebGL");
#endif
    }

    public void DisconnectTonConnect()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        disconnectWallet();
#else
        Debug.LogWarning("Ton Connect only works in WebGL");
#endif
    }

    public void OnTonConnectFeedback(string feedback)
    {
        Debug.Log("Ton Connect feedback: " + feedback);

        try
        {
            Wallet wallet = JsonUtility.FromJson<Wallet>(feedback);
            string userFriendlyAddress = TonWalletAddressConverter.ToUserFriendlyAddress(wallet.address);
            tonConnectView.SetWalletAddress(userFriendlyAddress);
            tonConnectView.SetButtonActive(false);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }
    }

    public void OnTonDisconnectFeedback(string feedback)
    {
        Debug.Log("Ton Disconnect feedback: " + feedback);
        tonConnectView.SetButtonActive(true);
    }

    public void OnTonError(string error)
    {
        Debug.LogError("Ton Error: " + error);
    }

    [System.Serializable]
    private struct Wallet
    {
        public string address;
    }
}
