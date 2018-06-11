using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 检查UI纹理的打包图集大小，单个模块的UI图集不能过大
/// </summary>
public class UIAtlasChecker : BaseChecker
{
    //指定要检查的文件夹
    private string[] checkDirs = { "Assets/UIs/Icon" };

    //图集最大大小
    private int maxAltasWidth = 1024;
    private int maxAltasHeight = 1024;

    override public string GetErrorDesc()
    {
        return string.Format("打包的图集大小不能超过{0}x{1}", maxAltasWidth, maxAltasHeight);
    }

    protected override void OnCheck()
    {
        //filter(过滤器) t:texture type = texture  
        //找到所有的UI纹理贴图的GUID 资源唯一标识
        string[] guids = AssetDatabase.FindAssets("t:texture", checkDirs);
        Dictionary<string, int> dic = new Dictionary<string, int>();

        foreach (var guid in guids)
        {//通过GUID拿到资源完整路径
            var asset_path = AssetDatabase.GUIDToAssetPath(guid);
            var texture = AssetDatabase.LoadAssetAtPath<Texture>(asset_path);
            //获取packing_tags
            string packing_tag = (AssetImporter.GetAtPath(asset_path) as TextureImporter).spritePackingTag;

            if (string.IsNullOrEmpty(packing_tag))
            {
                continue;
            }
            if (!dic.ContainsKey(packing_tag))
            {
                dic.Add(packing_tag, 0);
            }
            dic[packing_tag] += Convert.ToInt32(texture.width * texture.height);
        }

        foreach (var key in dic)
        {
            if (key.Value > maxAltasWidth * maxAltasHeight)
            {
                CheckItem item = new CheckItem();
                item.packingTag = key.Key;
                item.altasSize = key.Value;
                this.outputList.Add(item);
            }
        }
    }

    struct CheckItem : ICheckItem
    {
        public string packingTag;

        public int altasSize;

        public string MainKey
        {
            get { return this.packingTag; }
        }

        public StringBuilder Output()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Format("{0}   size={1}x{2}",
                packingTag,
                1024,
                altasSize / 1024));
            return builder;
        }
    }
}
