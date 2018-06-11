using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;


public enum CheckerType
{
    // UI相关
    UIAtlas,                                        // UI图集
    UITexture,                                      // UI纹理贴图
}

class AssetsCheckConfig
{
    // 排除列表文件夹
    public static string ExcludeDir = Path.Combine(Application.dataPath, "../AssetsCheck/Exclude");
    // 输出文件夹
    public static string OutputDir = Path.Combine(Application.dataPath, "../AssetsCheck");
}
