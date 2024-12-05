using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace MetaMiners.Network.Core.Data
{
    public class PersistentDataStorageProvider : BaseDataStorageProvider
    {
        private static Dictionary<string, Dictionary<string, string>> sharedHeaders = new Dictionary<string, Dictionary<string, string>>();
        private static Dictionary<string, Dictionary<string, object>> sharedData = new Dictionary<string, Dictionary<string, object>>();
        private string storageKey;

        public override void Initialize(string prefix = "general")
        {
            storageKey = $"{prefix}-persistent-data-storage";

            // Инициализация только при первом вызове с данным префиксом
            if (!sharedHeaders.ContainsKey(storageKey) || !sharedData.ContainsKey(storageKey))
            {
                LoadDataFromPlayerPrefs();
            }
        }

        private void LoadDataFromPlayerPrefs()
        {
            if (!PlayerPrefs.HasKey(storageKey))
            {
                sharedHeaders[storageKey] = new Dictionary<string, string>();
                sharedData[storageKey] = new Dictionary<string, object>();
                return;
            }

            string json = PlayerPrefs.GetString(storageKey);
            var loadedData = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(json);

            if (loadedData != null)
            {
                if (loadedData.TryGetValue("Headers", out var headers))
                {
                    sharedHeaders[storageKey] = headers.ToDictionary(entry => entry.Key, entry => entry.Value.ToString());
                }

                if (loadedData.TryGetValue("GetProfileResponse", out var data))
                {
                    sharedData[storageKey] = data;
                }
            }
            else
            {
                sharedHeaders[storageKey] = new Dictionary<string, string>();
                sharedData[storageKey] = new Dictionary<string, object>();
            }
        }

        private void SaveDataToPlayerPrefs()
        {
            var dataToSave = new
            {
                Headers = sharedHeaders[storageKey],
                Data = sharedData[storageKey]
            };

            string json = JsonConvert.SerializeObject(dataToSave, Formatting.Indented);
            PlayerPrefs.SetString(storageKey, json);
            PlayerPrefs.Save();
        }

        // Методы работы с заголовками
        public override void SaveHeader(string key, string value)
        {
            sharedHeaders[storageKey][key] = value;
            SaveDataToPlayerPrefs();
        }

        public override void RemoveHeader(string key)
        {
            sharedHeaders[storageKey].Remove(key);
            SaveDataToPlayerPrefs();
        }

        public override bool TryGetHeader(string key, out string value)
        {
            value = sharedHeaders[storageKey].ContainsKey(key) ? sharedHeaders[storageKey][key] : null;
            return sharedHeaders[storageKey].ContainsKey(key);
        }

        public override bool IsHeaderExist(string key)
        {
            return sharedHeaders[storageKey].ContainsKey(key);
        }

        // Методы работы с данными
        public override void SaveData(string key, object value)
        {
            sharedData[storageKey][key] = value;
            SaveDataToPlayerPrefs();
        }

        public override void RemoveData(string key)
        {
            sharedData[storageKey].Remove(key);
            SaveDataToPlayerPrefs();
        }

        public override bool TryGetData<T>(string key, out T value)
        {
            if (sharedData[storageKey].ContainsKey(key) && sharedData[storageKey][key] is T)
            {
                value = (T)sharedData[storageKey][key];
                return true;
            }

            value = default;
            return false;
        }

        public override bool IsDataExist(string key)
        {
            return sharedData[storageKey].ContainsKey(key);
        }
    }
}
