using MetaMiners.Network;
using System.Collections;
using System.Net;
using UnityEngine;

public class URLHandler : MonoBehaviour
{
    public void Start()
    {
        URLParameters.Instance.RegisterOnDone(ParseParameters);
    }

    private void ParseParameters(URLParameters parameters)
    {
        if (parameters.HashParameters.ContainsKey("tgWebAppData"))
        {
            string decodedUrl = WebUtility.UrlDecode(parameters.HashParameters["tgWebAppData"]);
            Debug.Log($"AuthData: {decodedUrl}");
            NetworkManager.Instance.SetAuthData(decodedUrl);
        } else
        {
            Debug.LogError("Hash parameter 'tgWebAppData' not exist");
        }
    }
}
