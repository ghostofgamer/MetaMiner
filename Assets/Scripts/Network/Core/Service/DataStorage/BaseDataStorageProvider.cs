using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseDataStorageProvider
{
    public abstract void Initialize(string prefix = "general");
    public abstract void SaveHeader(string key, string value);
    public abstract bool IsHeaderExist(string key);
    public abstract bool TryGetHeader(string key, out string value);
    public abstract void RemoveHeader(string key);
    public abstract void SaveData(string key, object value);
    public abstract bool IsDataExist(string key);
    public abstract bool TryGetData<T>(string key, out T value);
    public abstract void RemoveData(string key);
}
