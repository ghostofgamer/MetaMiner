﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class URLParameters : MonoBehaviour
{
    // set testíng data here for in-editor-use
    // href | hash | host | hostname | pathname | port | protocol | search
    public static string TestData = "|||||||";
    private static URLParameters m_Instance = null;
    public static URLParameters Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = (URLParameters)FindObjectOfType(typeof(URLParameters));
                if (m_Instance == null)
                    m_Instance = (new GameObject("URLParameters")).AddComponent<URLParameters>();
                m_Instance.gameObject.name = "URLParameters";
                //DontDestroyOnLoad(m_Instance.gameObject);
            }
            return m_Instance;
        }
    }

    private System.Action<URLParameters> m_OnDone = null;
    private System.Action<URLParameters> m_OnDoneOnce = null;

    private bool m_HaveInformation = false;
    private string m_RawData;
    private string m_Href;
    private string m_Hash;
    private string m_Host;
    private string m_Hostname;
    private string m_Pathname;
    private string m_Port;
    private string m_Protocol;
    private string m_Search;
    private Dictionary<string, string> m_SearchParams = new Dictionary<string, string>();
    private Dictionary<string, string> m_HashParams = new Dictionary<string, string>();

    public bool HaveInformation
    {
        get { return m_HaveInformation; }
    }
    public string RawData
    {
        get { return m_RawData; }
    }
    public string Href
    {
        get { return m_Href; }
    }
    public string Hash
    {
        get { return m_Hash; }
    }
    public string Host
    {
        get { return m_Host; }
    }
    public string Hostname
    {
        get { return m_Hostname; }
    }
    public string Pathname
    {
        get { return m_Pathname; }
    }
    public string Port
    {
        get { return m_Port; }
    }
    public string Protocol
    {
        get { return m_Protocol; }
    }
    public string Search
    {
        get { return m_Search; }
    }
    public IDictionary<string, string> SearchParameters
    {
        get { return m_SearchParams; }
    }
    public IDictionary<string, string> HashParameters
    {
        get { return m_HashParams; }
    }

    public void RegisterOnDone(System.Action<URLParameters> aCallback)
    {
        m_OnDone += aCallback;
        if (HaveInformation)
            aCallback(this);
    }

    public void RegisterOnceOnDone(System.Action<URLParameters> aCallback)
    {
        if (HaveInformation)
            aCallback(this);
        else
            m_OnDoneOnce += aCallback;
    }

    public void Request()
    {
        StartCoroutine(_Request());
    }

    private IEnumerator _Request()
    {
        m_HaveInformation = false;
#if UNITY_EDITOR
        yield return null;
        SetAddressComponents(TestData);
#elif UNITY_WEBPLAYER
        const string WebplayerCode = "GetUnity ().SendMessage ('{0}', 'SetAddressComponents', location.href +'|'+ location.hash +'|'+ location.host +'|'+ location.hostname +'|'+ location.pathname +'|'+ location.port +'|'+ location.protocol +'|'+ location.search);";
        Application.ExternalEval(string.Format(WebplayerCode, gameObject.name));
#elif UNITY_WEBGL
        const string WebGLCode = "SendMessage ('{0}', 'SetAddressComponents', location.href +'|'+ location.hash +'|'+ location.host +'|'+ location.hostname +'|'+ location.pathname +'|'+ location.port +'|'+ location.protocol +'|'+ location.search);";
        Application.ExternalEval(string.Format(WebGLCode, gameObject.name));
#endif
        yield break;
    }

    //void Awake()
    //{
    //    DontDestroyOnLoad(gameObject);
    //}

    public IEnumerator Start()
    {
        yield return null; // wait one frame to ensure all delegates are assigned.
        Request();
    }

    public void SetAddressComponents(string aData)
    {
        string[] parts = aData.Split('|');
        m_RawData = aData;
        m_Href = parts[0];
        m_Hash = parts[1];
        m_Host = parts[2];
        m_Hostname = parts[3];
        m_Pathname = parts[4];
        m_Port = parts[5];
        m_Protocol = parts[6];
        m_Search = parts[7];
        var tmp = m_Search.TrimStart('?');
        var data = tmp.Split('&');
        for (int i = 0; i < data.Length; i++)
        {
            var val = data[i].Split('=');
            if (val.Length != 2)
                continue;
            m_SearchParams[val[0]] = val[1];
            Debug.Log($"Parameter find: {val[0]} = {val[1]}");
        }
        tmp = m_Hash.TrimStart('#');
        data = tmp.Split('&');
        for (int i = 0; i < data.Length; i++)
        {
            var val = data[i].Split('=');
            if (val.Length != 2)
                continue;
            m_HashParams[val[0]] = val[1];
        }

        m_HaveInformation = true;
        if (m_OnDone != null)
            m_OnDone(this);
        if (m_OnDoneOnce != null)
        {
            m_OnDoneOnce(this);
            m_OnDoneOnce = null;
        }
    }
}


public static class IDictionaryExtension
{
    public static double GetDouble(this IDictionary<string, string> aDict, string aKey, double aDefault = 0d)
    {
        string tmp;
        if (aDict.TryGetValue(aKey, out tmp))
        {
            double val;
            if (double.TryParse(tmp, out val))
                return val;
        }
        return aDefault;
    }
    public static int GetInt(this IDictionary<string, string> aDict, string aKey, int aDefault = 0)
    {
        string tmp;
        if (aDict.TryGetValue(aKey, out tmp))
        {
            int val;
            if (int.TryParse(tmp, out val))
                return val;
        }
        return aDefault;
    }
}