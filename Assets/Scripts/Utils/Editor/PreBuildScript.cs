using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System;

public class PreBuildScript : IPreprocessBuildWithReport
{
    private const string LastBuildDateKey = "LastBuildDate";
    private const string DailyBuildNumberKey = "DailyBuildNumber";

    // ������� ����������, 0 � ������
    public int callbackOrder => 0;

    // �����, ������� ����� ������ ����� �������
    public void OnPreprocessBuild(BuildReport report)
    {
        // �������� ������� ���� � ������� "����.�����.���" (� ����� ������� ����)
        string date = DateTime.Now.ToString("dd.MM.yy");
        int dailyBuildNumber = GetDailyBuildNumber(date);
        string version = $"dev_v{date}.{dailyBuildNumber}";

        // ���������� ���� � �������������� ����� ������
        bool confirmBuild = EditorUtility.DisplayDialog(
            "Build Confirmation",
            $"���������� ������ MetaMiners {version}?",
            "��", "���"
        );

        if (confirmBuild)
        {
            // ������������� ������
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
        Debug.Log($"����������� ������: {version}");
    }
}
