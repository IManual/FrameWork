using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;
using ConfigDic = System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, string>>;


    /// <summary>
    /// AI配置文件读取
    /// </summary>
public class AIResourceManager : ResourceManager
{
    private static ConfigDic map;
    static AIResourceManager()
    {
        map = new ConfigDic();
    }
    static string fileName = "AI_01";
    //加载配置文件
    public static ConfigDic BuildDic()
    {
        string strConfig = ReadConfig(fileName);
        //字符串读取器
        using (StringReader reader = new StringReader(strConfig))
        {
            string line;
            string mainKey = string.Empty;
            while ((line = reader.ReadLine()) != null)
            {                 
                //去除空白行
                if (string.IsNullOrEmpty(line))
                    continue;
                line.Trim();
                //解析
                if (line.StartsWith("["))
                {
                    line = line.TrimStart('[');
                    mainKey = line.TrimEnd(']');
                    map.Add(mainKey, new Dictionary<string, string>());
                }


                else if (!line.StartsWith("["))
                {
                    string[] keyValue = line.Split('>');
                    map[mainKey].Add(keyValue[0], keyValue[1]);
                }


            }
        }
        return map;
    }
}


