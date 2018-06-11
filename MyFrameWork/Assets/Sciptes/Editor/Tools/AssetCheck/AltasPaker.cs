using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

// 打包图集，输出文件到工程下的UIAltas下
public class AtlasPacker : EditorWindow
{
    public struct PackItem
    {
        public string packTag;
        public List<string> assets;
    }

    // 指定要检查的文件夹
    private string[] checkDirs = new string[1];

    private string fileName = "";

    private int maxWidth = 2048;
    private int maxHeight = 2048;

    private Object targetFile;

    [MenuItem("自定义工具/资源检查/打包图集")]
    public static void ShowAltasPacker()
    {
        EditorWindow window = EditorWindow.GetWindow(typeof(AtlasPacker));
        window.titleContent = new GUIContent("AltasPacker");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("StartPackAll"))
        {
            checkDirs[0] = "Assets/UIs";
            this.StartPack();
        }

        EditorGUILayout.Space();
        targetFile = EditorGUILayout.ObjectField("添加文件:", targetFile, typeof(Object), true) as Object;

        if (GUILayout.Button("StartPackByFile"))
        {
            if (null == targetFile)
            {
                this.ShowNotification(new GUIContent("请选择正确的文件夹!"));
                return;
            }

            checkDirs[0] = AssetDatabase.GetAssetPath(targetFile);
            if (Directory.Exists(checkDirs[0]))
            {
                this.ShowNotification(new GUIContent("请选择正确的文件夹!"));
            }

            this.StartPack();
        }
    }

    private void StartPack()
    {
        Debug.Log("Start pack");

        Dictionary<string, List<string>> texturesDic = new Dictionary<string, List<string>>();
        this.GetPackTextureDic(texturesDic);
        this.ExcutePackTextures(texturesDic);

        Debug.Log("pack complete");
    }

    private void GetPackTextureDic(Dictionary<string, List<string>> dic)
    {
        string[] guides = AssetDatabase.FindAssets("t:texture2d", checkDirs);
        foreach (var guid in guides)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
            if (string.IsNullOrEmpty(importer.spritePackingTag))
            {
                continue;
            }

            List<string> list;
            if (!dic.TryGetValue(importer.spritePackingTag, out list))
            {
                list = new List<string>();
                dic.Add(importer.spritePackingTag, list);
            }

            list.Add(path);
        }
    }

    private void ExcutePackTextures(Dictionary<string, List<string>> dic)
    {
        Queue<PackItem> pack_queue = new Queue<PackItem>();
        foreach (var item in dic)
        {
            PackItem pack_item = new PackItem();
            pack_item.packTag = item.Key;
            pack_item.assets = item.Value;
            pack_queue.Enqueue(pack_item);
        }

        StepPackTextures(pack_queue);
    }

    private void StepPackTextures(Queue<PackItem> packQueue)
    {
        if (packQueue.Count <= 0)
        {
            Debug.Log("Pack Complete");
            return;
        }

        PackItem pack_item = packQueue.Dequeue();
        List<string> path_list = pack_item.assets;
        List<Texture2D> texture_list = new List<Texture2D>();

        for (int i = 0; i < path_list.Count; i++)
        {
            TextureImporter importer = AssetImporter.GetAtPath(path_list[i]) as TextureImporter;
            Texture2D texture2d = AssetDatabase.LoadAssetAtPath<Texture2D>(path_list[i]);
            if (null == importer || null == texture2d)
            {
                continue;
            }

            importer.hideFlags = HideFlags.NotEditable;
            importer.isReadable = true;
            importer.textureCompression = TextureImporterCompression.Uncompressed;
            importer.SaveAndReimport();
            texture_list.Add(texture2d);
        }

        try
        {
            // 打包图集并保存
            if (texture_list.Count > 1) // 大于1才打图集
            {
                Debug.Log("pack: " + pack_item.packTag);
                Texture2D altas = new Texture2D(maxWidth, maxHeight, TextureFormat.RGBA32, false);
                altas.PackTextures(texture_list.ToArray(), 0, 2048);
                this.SaveAltasTexture(pack_item.packTag, altas);
            }
        }
        catch (System.Exception)
        {
            Debug.Log("pack fail: " + pack_item.packTag);
            throw;
        }
        finally
        {
            for (int i = 0; i < path_list.Count; i++)
            {
                TextureImporter importer = AssetImporter.GetAtPath(path_list[i]) as TextureImporter;
                importer.isReadable = false;                 // 重设isReadable为false
                importer.hideFlags = HideFlags.None;
                importer.textureCompression = TextureImporterCompression.Compressed;
                importer.SaveAndReimport();
            }

            StepPackTextures(packQueue);
        }
    }

    private void SaveAltasTexture(string packtag, Texture2D altas)
    {
        string dir = Path.Combine(Application.dataPath, "../UIAltas");
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        string name = packtag.Replace("/", "_");

        var files = Directory.GetFiles(dir);
        foreach (var file in files)
        {
            FileInfo file_info = new FileInfo(file);

            string file_name = file_info.Name;
            file_name = file_name.Substring(file_name.IndexOf("_") + 1).TrimEnd(".png".ToCharArray());
            if (file_name == name)
            {
                File.Delete(file);
            }
        }

        byte[] bytes = altas.EncodeToPNG();
        File.WriteAllBytes(Path.Combine(dir, string.Format("{0}x{1}_{2}.png", altas.width, altas.height, name)), bytes);
    }
}
