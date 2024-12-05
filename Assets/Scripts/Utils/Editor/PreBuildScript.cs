using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System;

public class PreBuildScript : IPreprocessBuildWithReport
{
    private const string LastBuildDateKey = "LastBuildDate";
    private const string DailyBuildNumberKey = "DailyBuildNumber";

    // Порядок выполнения, 0 — первым
    public int callbackOrder => 0;

    // Метод, который будет вызван перед сборкой
    public void OnPreprocessBuild(BuildReport report)
    {
        // Получаем текущую дату в формате "день.месяц.год" (с двумя цифрами года)
        string date = DateTime.Now.ToString("dd.MM.yy");
        int dailyBuildNumber = GetDailyBuildNumber(date);
        string version = $"dev_v{date}.{dailyBuildNumber}";

        // Отображаем окно с подтверждением перед билдом
        bool confirmBuild = EditorUtility.DisplayDialog(
            "Build Confirmation",
            $"Установить версию MetaMiners {version}?",
            "Да", "Нет"
        );

        if (confirmBuild)
        {
            // Устанавливаем версию
            SetVersion(version);
            IncrementDailyBuildNumber(date);
        }
    }

    private static int GetDailyBuildNumber(string currentDate)
    {
        string lastBuildDate = EditorPrefs.GetString(LastBuildDateKey, string.Empty);
        if (lastBuildDate == currentDate)
        {
            return EditorPrefs.GetInt(DailyBuildNumberKey, 1);
        }
        else
        {
            ResetDailyBuildNumber(currentDate);
            return 1;
        }
    }

    private static void ResetDailyBuildNumber(string currentDate)
    {
        EditorPrefs.SetString(LastBuildDateKey, currentDate);
        EditorPrefs.SetInt(DailyBuildNumberKey, 1);
    }

    private static void IncrementDailyBuildNumber(string currentDate)
    {
        int currentBuildNumber = GetDailyBuildNumber(currentDate);
        EditorPrefs.SetInt(DailyBuildNumberKey, currentBuildNumber + 1);
    }

    private static void SetVersion(string version)
    {
        PlayerSettings.bundleVersion = version;
        Debug.Log($"Установлена версия: {version}");
    }
}
