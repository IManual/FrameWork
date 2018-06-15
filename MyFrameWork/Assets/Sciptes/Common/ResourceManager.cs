using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public class ResourceManager 
{
    private static Dictionary<string, string> map;
    static ResourceManager()
    {
        map = new Dictionary<string,string>();
        LoadConfig();
    }
    //读取配置文件
    static string fileName="ResConfig";
    public static string ReadConfig(string fileName)
    {
        //Assets/StreamingAssets/ResConfig.txt" 
        string path = Application.streamingAssetsPath + "/" + fileName + ".txt";
        if (Application.platform != RuntimePlatform.Android)
        {//非安卓平台，路径需要添加 file://
            path = "file://" + path;
        }
        WWW www = new WWW(path);
        while (true)
        {
            //如果读取完成
            if (www.isDone)
                return www.text;
        }
    }
    //加载配置文件
    private static void LoadConfig()
    {
        string strConfig = ReadConfig(fileName);
        //字符串读取器
        using (StringReader reader = new StringReader(strConfig))
        {
            string line;
            while (!string.IsNullOrEmpty(line = reader.ReadLine()))
            {
                //解析
                string[] keyValue = line.Split('=');
                map.Add(keyValue[0], keyValue[1]);
            }
        }
    }
    public static T Load<T>(string resName) where T : Object
    {
        return Resources.Load<T>(map[resName]);
    }
}
