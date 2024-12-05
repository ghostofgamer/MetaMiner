using MetaMiners.Network.Core.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;

namespace MetaMiners.Network.Core
{
    [CustomEditor(typeof(BackendService), true)]
    public class BackendServiceEditor : Editor
    {
        private BackendService backendService;
        private List<MethodInfo> requestMethods = new List<MethodInfo>();

        public override void OnInspectorGUI()
        {
            backendService = (BackendService)target;

            InitializeRequestMethods();

            DrawDefaultInspector();

            EditorGUILayout.LabelField("API Requests:", EditorStyles.boldLabel);

            if (requestMethods.Count == 0)
            {
                EditorGUILayout.LabelField("No API request methods found.");
                return;
            }

            // Отображение каждого метода запроса
            foreach (var method in requestMethods)
            {
                var attribute = method.GetCustomAttribute<RequestEndpointAttribute>();
                if (attribute == null) continue;

                EditorGUILayout.LabelField($"- {method.Name} ({attribute.Method} {attribute.Endpoint})", EditorStyles.boldLabel);
            }
        }

        private void InitializeRequestMethods()
        {
            // Находим все методы с атрибутом RequestEndpointAttribute
            requestMethods = backendService.GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(m => m.GetCustomAttribute<RequestEndpointAttribute>() != null)
                .ToList();
        }
    }
}