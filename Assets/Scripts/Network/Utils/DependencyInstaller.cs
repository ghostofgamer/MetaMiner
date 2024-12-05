#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.PackageManager.Requests;
using UnityEditor.PackageManager;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MetaMiners.Network.Utils
{
    [InitializeOnLoad]
    public class DependencyInstaller
    {
        // ������ ������������ ��� ���������
        private static readonly List<string> Dependencies = new List<string>
    {
        "https://github.com/jilleJr/Newtonsoft.Json-for-Unity.git#upm"
        // �������� ������ ����������� �� ��������
    };

        private static readonly List<string> PackageNames = new List<string>
    {
        "jillejr.newtonsoft.json-for-unity"
        // �������� ����� ������ ������������
    };

        private static AddRequest addRequest;
        private static int currentDependencyIndex = 0;
        private static bool installingInProgress = false;
        private static bool manualInstalling = false;

        static DependencyInstaller()
        {
            currentDependencyIndex = 0;
            installingInProgress = false;
            // �������� �������� � ��������� ������������ ��� �������� �������
            AutoInstallDependencies();
        }

        public static void AutoInstallDependencies()
        {
            currentDependencyIndex = 0;
            manualInstalling = false;
            InstallNextDependency();
        }

        [MenuItem("Tools/MetaMiners Network/Install Dependencies")]
        public static void InstallDependencies()
        {
            currentDependencyIndex = 0;
            manualInstalling = true;
            InstallNextDependency();
        }

        private static void InstallNextDependency()
        {
            if (currentDependencyIndex >= Dependencies.Count)
            {
                if (installingInProgress || manualInstalling)
                    Debug.Log("All dependencies are installed.");
                installingInProgress = false;
                return;
            }

            string packageName = PackageNames[currentDependencyIndex];
            if (IsPackageInstalled(packageName))
            {
                if (manualInstalling)
                    Debug.Log($"{packageName} is already installed.");
                ++currentDependencyIndex;
                InstallNextDependency(); // ��������� � ��������� �����������
                return;
            }

            installingInProgress = true;
            Debug.Log($"Installing {packageName} from {Dependencies[currentDependencyIndex]}...");
            addRequest = Client.Add(Dependencies[currentDependencyIndex]); // �������� ��������� ������� �����������
            EditorApplication.update += Progress;
        }


        private static void Progress()
        {
            if (addRequest.IsCompleted)
            {
                if (addRequest.Status == StatusCode.Success)
                {
                    Debug.Log($"{addRequest.Result.name} installed successfully.");
                }
                else if (addRequest.Status >= StatusCode.Failure)
                {
                    Debug.LogError($"Failed to install {addRequest.Result.name}: {addRequest.Error.message}");
                }

                EditorApplication.update -= Progress; // ��������� ���������� ���������� ����� ���������� ���������
                currentDependencyIndex++;
                InstallNextDependency(); // ������������� ��������� �����������
            }
        }

        private static bool IsPackageInstalled(string packageName)
        {
            var listRequest = Client.List(true); // ���������� ������ �� ��������� ������ ������������� �������
            while (!listRequest.IsCompleted)
            {
                // �������� ���������� �������
            }

            if (listRequest.Status == StatusCode.Failure)
            {
                Debug.LogError($"Failed to list packages: {listRequest.Error.message}");
                return false;
            }

            // ���������, ���������� �� ����� � ��������� ������
            return listRequest.Result.Any(package => package.name == packageName);
        }
    }
}
#endif