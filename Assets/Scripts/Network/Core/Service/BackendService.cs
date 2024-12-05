using MetaMiners.Network.Core.Data;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace MetaMiners.Network.Core
{
    public abstract class BackendService : MonoBehaviour
    {
        public enum RequestMethod
        {
            GET,
            POST,
            PUT,
            DELETE
        }

        [SerializeField]
        private bool doNotDestroyOnLoad = false;

        [SerializeField]
        protected BackendSettings settings;

        public BaseDataStorageProvider DataStorageProvider { get; private set; } = new PersistentDataStorageProvider();

        protected virtual void Awake()
        {
            if (settings == null)
            {
                Debug.LogError("BackendSettings not found. Please create and assign it.");
            }

            if (doNotDestroyOnLoad) DontDestroyOnLoad(this);

            DataStorageProvider.Initialize();
        }

        protected void CreateOwnDataStorage(string prefix)
        {
            DataStorageProvider = new PersistentDataStorageProvider();
            DataStorageProvider.Initialize(prefix);
        }

        protected void SetCustomDataStorage(BaseDataStorageProvider customStorageDataProvider)
        {
            DataStorageProvider = customStorageDataProvider;
        }

        protected IEnumerator SendRequest<T>(string endpoint, RequestMethod method, Action<T> callback = null, Action<Exception> callbackError = null, Dictionary<string, string> headers = null, object body = null)
        {
            string url = settings.baseUrl + endpoint;
            UnityWebRequest request = new UnityWebRequest(url, method.ToString());

            // Установка тела запроса, если оно необходимо
            if (body != null && (method == RequestMethod.POST || method == RequestMethod.PUT))
            {
                try
                {
                    byte[] bodyRaw = new byte[] { };

                    if (body is string bodyString)
                    {
                        // Если `body` это строка, конвертируем ее в байты напрямую
                        bodyRaw = Encoding.UTF8.GetBytes(bodyString);
                    }
                    else
                    {
                        // Если `body` это объект, сериализуем его в JSON и затем конвертируем в байты
                        string jsonBody = JsonConvert.SerializeObject(body);
                        bodyRaw = Encoding.UTF8.GetBytes(jsonBody);
                    }

                    request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                    request.SetRequestHeader("Content-Type", "application/json");
                }
                catch (Exception ex)
                {
                    callbackError?.Invoke(ex);
                }
            }

            // Установка заголовков, если они переданы
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.SetRequestHeader(header.Key, header.Value);
                }
            }

            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                //Debug.Log(request.downloadHandler.text);
                T result = JsonConvert.DeserializeObject<T>(request.downloadHandler.text);
                callback?.Invoke(result);
            }
            else
            {
                callbackError?.Invoke(new HttpRequestException($"Data: {request.downloadHandler?.text}\nRequest failed: {request.error}"));
            }
        }
    }
}