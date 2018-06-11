using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;


    class ErrorItem
    {
        public int count;
        public string desc;
    }

public class AssetsChecker : EditorWindow
{
    private static Dictionary<CheckerType, BaseChecker> checkerDic = new Dictionary<CheckerType, BaseChecker>();
    private static Dictionary<string, ErrorItem> statisticsErrorDic = new Dictionary<string, ErrorItem>();
    private bool errorDirty = true;

    [MenuItem("自定义工具/资源检查/检查器列表", false, 110)]
    public static void ShowWindow()
    {
        EditorWindow window = EditorWindow.GetWindow(typeof(AssetsChecker));
        window.titleContent = new GUIContent("AssetsChecker");
    }

    private void OnGUI()
    {
        if (this.errorDirty)
        {
            this.errorDirty = false;
            this.RefreshErrorStatistics();
        }
        FieldInfo[] fields = typeof(CheckerType).GetFields();
        for (int i = 1; i < fields.Length; i++)
        {
            string checkerName = fields[i].ToString().Split(' ')[1];
            CheckerType checkerType = (CheckerType)fields[i].GetValue(null);
            BaseChecker checker = GetChecker(checkerType);
            checker.SetFileName(checkerName);

            GUILayout.BeginHorizontal();

            // 检查
            if (GUILayout.Button(checkerName + "Check"))
            {
                errorDirty = true;
                Debug.LogFormat("[AssetsChecker] Start Check {0}", checkerName);
                double start_time = EditorApplication.timeSinceStartup;
                checker.StartCheck();
                checker.Output();
                this.SaveCacheErrorCount(checkerName, checker.ErrorCount, checker.GetErrorDesc());

                Debug.LogFormat("[AssetsChecker] Check Complete, cost time{0}s, error count {1}",
                                Convert.ToInt32(EditorApplication.timeSinceStartup - start_time),
                                checker.ErrorCount);
            }

            // 修复
            if (this.GetCacheErrorCount(checkerName) > 0)
            {
                if (GUILayout.Button("Fix"))
                {
                    errorDirty = true;
                    checker.StartFix();
                }
            }

            GUILayout.EndHorizontal();
        }
    }

    // 读取本地存储的错误统计
    public static void ReadErrorStatisticsLines(out string[] lines)
    {
        string path = Path.Combine(AssetsCheckConfig.OutputDir, "ErrorStatistics.txt");
        if (File.Exists(path))
        {
            lines = File.ReadAllLines(path);
        }
        else
        {
            lines = new string[] { };
        }
    }

    // 更新错误统计
    private void RefreshErrorStatistics()
    {
        if (!Directory.Exists(AssetsCheckConfig.OutputDir))
        {
            Directory.CreateDirectory(AssetsCheckConfig.OutputDir);
        }

        if (!Directory.Exists(AssetsCheckConfig.ExcludeDir))
        {
            Directory.CreateDirectory(AssetsCheckConfig.ExcludeDir);
        }

        statisticsErrorDic.Clear();

        string[] lines;
        ReadErrorStatisticsLines(out lines);
        for (int i = 0; i < lines.Length; i++)
        {
            string[] ary = lines[i].Split(' ');
            if (ary.Length >= 3 && !string.IsNullOrEmpty(ary[0]) && Convert.ToInt32(ary[1]) > 0)
            {
                ErrorItem error_item = new ErrorItem();
                error_item.count = Convert.ToInt32(ary[1]);
                error_item.desc = ary[2];
                statisticsErrorDic.Add(ary[0], error_item);
            }
        }
    }

    // 保存错误数量
    private void SaveCacheErrorCount(string checkerName, int count, string desc)
    {
        ErrorItem error_item;
        if (!statisticsErrorDic.TryGetValue(checkerName, out error_item))
        {
            error_item = new ErrorItem();
            statisticsErrorDic.Add(checkerName, error_item);
        }

        error_item.count = count;
        error_item.desc = desc;

        this.WriteStatisticsError();
    }

    // 写在本地
    private void WriteStatisticsError()
    {
        StringBuilder builder = new StringBuilder();
        foreach (var item in statisticsErrorDic)
        {
            if (!string.IsNullOrEmpty(item.Key) && item.Value.count > 0)
            {
                builder.Append(string.Format("{0} {1} {2}\n", item.Key, item.Value.count, item.Value.desc));
            }
        }
        File.WriteAllText(Path.Combine(AssetsCheckConfig.OutputDir, "ErrorStatistics.txt"), builder.ToString());
    }

    // 获得每种检查缓存起来的错误数量
    private int GetCacheErrorCount(string checkerName)
    {
        if (statisticsErrorDic.ContainsKey(checkerName))
        {
            return statisticsErrorDic[checkerName].count;
        }

        return 0;
    }

    // 根据类型获得检查器
    public static BaseChecker GetChecker(CheckerType type)
    {
        BaseChecker checker;
        if (!checkerDic.TryGetValue(type, out checker))
        {
            // UI相关
            if (CheckerType.UIAtlas == type) checker = new UIAtlasChecker();
            if (CheckerType.UITexture == type) checker = new UITextureChecker();
        }

        return checker;
    }
}


