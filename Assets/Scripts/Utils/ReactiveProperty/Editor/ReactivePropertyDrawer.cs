using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ReactiveProperty<>))]
public class ReactivePropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Находим поле _value внутри ReactiveProperty
        SerializedProperty valueProperty = property.FindPropertyRelative("_value");

        if (valueProperty != null)
        {
            // Разделяем область на две части: поле и кнопку "Notify"
            Rect fieldRect = new Rect(position.x, position.y, position.width - 60, position.height);
            Rect buttonRect = new Rect(position.x + position.width - 55, position.y, 55, position.height);

            EditorGUI.BeginChangeCheck();

            // Рисуем поле для значения
            EditorGUI.PropertyField(fieldRect, valueProperty, label, true);

            // Проверяем, было ли изменено значение
            if (EditorGUI.EndChangeCheck())
            {
                NotifyChange(property);
            }

            // Кнопка "Notify"
            if (GUI.Button(buttonRect, "Notify"))
            {
                NotifyChange(property);
            }
        }
        else
        {
            EditorGUI.LabelField(position, label.text, "Unable to display");
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        SerializedProperty valueProperty = property.FindPropertyRelative("_value");

        if (valueProperty == null)
        {
            return 20f;
        }
        return EditorGUI.GetPropertyHeight(valueProperty, label, true);
    }

    private void NotifyChange(SerializedProperty property)
    {
        // Получаем объект ReactiveProperty через SerializedProperty
        property.serializedObject.ApplyModifiedProperties();

        object targetObject = GetTargetObjectOfProperty(property);
        if (targetObject != null)
        {
            var notifyMethod = targetObject.GetType().GetMethod("Notify");
            notifyMethod?.Invoke(targetObject, null);
        }
    }

    private object GetTargetObjectOfProperty(SerializedProperty prop)
    {
        if (prop == null) return null;

        string[] path = prop.propertyPath.Replace(".Array.data[", "[").Split('.');
        object obj = prop.serializedObject.targetObject;

        foreach (string part in path)
        {
            if (part.Contains("["))
            {
                string fieldName = part.Substring(0, part.IndexOf("["));
                int index = int.Parse(part.Substring(part.IndexOf("[")).Replace("[", "").Replace("]", ""));
                obj = GetValue(obj, fieldName, index);
            }
            else
            {
                obj = GetValue(obj, part);
            }
        }
        return obj;
    }

    private object GetValue(object source, string name, int index = -1)
    {
        if (source == null) return null;

        var type = source.GetType();
        var field = type.GetField(name, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
        var property = type.GetProperty(name, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

        object value = field?.GetValue(source) ?? property?.GetValue(source);

        if (index >= 0 && value is System.Collections.IEnumerable enumerable)
        {
            var enumerator = enumerable.GetEnumerator();
            for (int i = 0; i <= index; i++)
            {
                if (!enumerator.MoveNext()) return null;
            }
            return enumerator.Current;
        }
        return value;
    }


}
