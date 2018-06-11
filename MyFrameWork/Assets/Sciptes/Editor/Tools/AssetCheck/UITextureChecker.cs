using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

//检查UI纹理图的合理性及导入的设置
public class UITextureChecker : BaseChecker
{
    //指定要检查的文件夹
    private string[] checkDirs = { "Asset/UIs/Icon"};

    //指定单张纹理的大小
    private int maxSpriteWidth = 512;
    private int maxSpriteHeight = 256;

    public override string GetErrorDesc()
    {
        return string.Format("每个UI上占用的纹理不能超过{0}x{1}", this.maxSpriteWidth, this.maxSpriteHeight);
    }

    protected override void OnCheck()
    {
        string[] guids = AssetDatabase.FindAssets("t:texture", checkDirs);
        foreach (var guid in guids)
        {
            var asset_path = AssetDatabase.GUIDToAssetPath(guid);
            var texture = AssetDatabase.LoadAssetAtPath<Texture>(asset_path);
            TextureImporter importer = AssetImporter.GetAtPath(asset_path) as TextureImporter;

            CheckItem item = new CheckItem();
            item.asset = asset_path;

            int flag = 0;
            flag = this.CheckSize(texture, importer, ref item) ? flag : (flag << 1) | 1;
            flag = this.CheckPackingTag(texture, importer, ref item) ? flag : (flag << 1) | 1;
            flag = this.CheckMipMaps(texture, importer, ref item) ? flag : (flag << 1) | 1;
            flag = this.CheckRadable(texture, importer, ref item) ? flag : (flag << 1) | 1;

            if (flag > 0)
            {
                this.outputList.Add(item);
            }
        }
    }

    //检查纹理是否被设置成了Readable
    private bool CheckRadable(Texture texture, TextureImporter importer, ref CheckItem item)
    {
        item.isReadable = importer.isReadable;
        return !item.isReadable;
    }

    private bool CheckMipMaps(Texture texture, TextureImporter importer, ref CheckItem item)
    {
        item.isMipMaps = importer.mipmapEnabled;
        return !item.isMipMaps;
    }

    //检查纹理贴图是否有packingTag
    private bool CheckPackingTag(Texture texture, TextureImporter importer, ref CheckItem item)
    {
        item.isPackingTag = !string.IsNullOrEmpty(importer.spritePackingTag);
        return item.isPackingTag; 
    }

    //检查纹理的大小
    private bool CheckSize(Texture texture, TextureImporter importer, ref CheckItem item)
    {
        item.width = texture.width;
        item.height = texture.height;
        item.isVailidSize = item.width * item.height < this.maxSpriteWidth * this.maxSpriteHeight;
        return item.isVailidSize;
    }

    struct CheckItem : ICheckItem
    {
        public string asset;
        public int width;
        public int height;
        public bool isVailidSize;
        public bool isPackingTag;
        public bool isMipMaps;
        public bool isReadable;

        public string MainKey
        {
            get { return this.asset; }
        }

        public StringBuilder Output()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(asset);
            if (!isVailidSize) builder.Append(string.Format("   size={0}x{1}", width, height));
            if (!isPackingTag) builder.Append("   packing=false");
            if (isMipMaps) builder.Append("   mipmaps=true");
            if (isReadable) builder.Append("   readable=true");

            return builder;
        }
    }
}
