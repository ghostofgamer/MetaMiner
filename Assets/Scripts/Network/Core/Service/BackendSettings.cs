using UnityEngine;

namespace MetaMiners.Network.Core
{
    [CreateAssetMenu(fileName = "BackendSettings", menuName = "MetaMiners Network/Settings")]
    public class BackendSettings : ScriptableObject
    {
        [Header("Network Configuration")]
        public string baseUrl = "https://example-backend.com/"; // ������� URL API
        public string uniqueIdentifier = "example-project"; // ���������� ������������� (��������, ��� �������� ��� ��������������)
    }
}