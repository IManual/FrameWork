using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "FontMaker", menuName = "Nirvana/UI/FontMaker")]
public sealed class FontMaker : ScriptableObject
{
    [Serializable]
    private class Font
    {
        [SerializeField]
        private string fontName;

        [SerializeField]
        private FontMaker.Glyphs[] glyphs;

        public string GetFontName()
        {
            return this.fontName;
        }

        public FontMaker.Glyphs[] GetGlyphs()
        {
            return this.glyphs;
        }
    }

    [Serializable]
    private class Glyphs
    {
        [SerializeField]
        private int code;

        [SerializeField]
        private Texture2D image;

        public int GetCode()
        {
            return this.code;

        }

        public Texture2D GetTexture()
        {
            return this.image;
        }
    }

    [SerializeField]
    private string atlasName;

    [SerializeField]
    private int padding;

    [SerializeField]
    private int maximumAtlasSize = 2048;

    [SerializeField]
    private FontMaker.Font[] fonts;

    internal void Build()
    {
        using (ProgressIndicator progressIndicator = new ProgressIndicator("Make Font"))
        {

            this.MakeFont(progressIndicator);

        }
    }

    private void MakeFont(ProgressIndicator progressIndicator)
    {
        progressIndicator.Show("Process font images...");
        List<Texture2D> list = new List<Texture2D>();
        FontMaker.Font[] array = this.fonts;
        for (int i = 0; i < array.Length; i++)
        {
            FontMaker.Font font = array[i];
            FontMaker.Glyphs[] array2 = font.GetGlyphs();
            for (int j = 0; j < array2.Length; j++)
            {
                FontMaker.Glyphs glyphs = array2[j];
                if (glyphs.GetTexture() == null)
                {
                    Debug.LogErrorFormat("The font {0} with graph: {1} is missing texture.", new object[]
                    {
                            font.GetFontName(),
                            glyphs.GetCode()
                    });
                }
                else
                {
                    string assetPath = AssetDatabase.GetAssetPath(glyphs.GetTexture());
                    TextureImporter textureImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;
                    if (textureImporter != null)
                    {
                        bool flag = false;
                        if (!textureImporter.isReadable)
                        {
                            textureImporter.isReadable = true;
                            flag = true;
                        }
                        if (textureImporter.textureCompression != TextureImporterCompression.Uncompressed)
                        {
                            textureImporter.textureCompression = TextureImporterCompression.Uncompressed;
                            flag = true;
                        }
                        if (textureImporter.mipmapEnabled)
                        {
                            textureImporter.mipmapEnabled = false;
                            flag = true;
                        }
                        if (textureImporter.npotScale != TextureImporterNPOTScale.None)
                        {
                            textureImporter.npotScale = TextureImporterNPOTScale.None;
                            flag = true;
                        }
                        if (flag)
                        {
                            textureImporter.SaveAndReimport();
                        }
                    }
                    list.Add(glyphs.GetTexture());
                }
            }
        }
        progressIndicator.SetProgress(0.25f);
        progressIndicator.Show("Build font atlas...");
        Texture2D texture2D = new Texture2D(0, 0, TextureFormat.ARGB32, false);
        texture2D.name = "Font Atlas";
        Rect[] array3 = texture2D.PackTextures(list.ToArray(), this.padding, this.maximumAtlasSize, false);
        string assetPath2 = AssetDatabase.GetAssetPath(this);
        string directoryName = Path.GetDirectoryName(assetPath2);
        string text = this.atlasName;
        if (!text.EndsWith(".png") || !text.EndsWith(".PNG"))
        {
            text += ".png";
        }
        string text2 = Path.Combine(directoryName, text);
        byte[] array4 = ImageConversion.EncodeToPNG(texture2D);
        FileStream fileStream = File.OpenWrite(text2);
        fileStream.Write(array4, 0, array4.Length);
        fileStream.Close();
        AssetDatabase.Refresh();
        TextureImporter textureImporter2 = AssetImporter.GetAtPath(text2) as TextureImporter;
        if (textureImporter2 != null)
        {
            bool flag2 = false;
            if (textureImporter2.isReadable)
            {
                textureImporter2.isReadable = false;
                flag2 = true;
            }
            if (!textureImporter2.alphaIsTransparency)
            {
                textureImporter2.alphaIsTransparency = true;
                flag2 = true;
            }
            if (flag2)
            {
                textureImporter2.SaveAndReimport();
            }
        }
        Texture mainTexture = AssetDatabase.LoadAssetAtPath<Texture>(text2);
        progressIndicator.SetProgress(0.35f);
        progressIndicator.Show("Build font material...");
        string path = this.atlasName + ".mat";
        string text3 = Path.Combine(directoryName, path);
        Material material = AssetDatabase.LoadAssetAtPath<Material>(text3);
        if (material == null)
        {
            Shader shader = Shader.Find("Transparent/Diffuse");
            material = new Material(shader);
            AssetDatabase.CreateAsset(material, text3);
        }
        material.mainTexture = mainTexture;
        EditorUtility.SetDirty(material);
        AssetDatabase.SaveAssets();
        progressIndicator.SetProgress(0.7f);
        progressIndicator.Show("Build font settings...");
        int num = 0;
        FontMaker.Font[] array5 = this.fonts;
        for (int k = 0; k < array5.Length; k++)
        {
            FontMaker.Font font2 = array5[k];
            FontMaker.Glyphs[] array6 = font2.GetGlyphs();
            if (array6.Length != 0)
            {
                string text4 = font2.GetFontName();
                if (!text4.EndsWith(".fontsettings"))
                {
                    text4 += ".fontsettings";
                }
                string text5 = Path.Combine(directoryName, text4);
                UnityEngine.Font font = AssetDatabase.LoadAssetAtPath<UnityEngine.Font>(text5);
                if (font == null)
                {
                    font = new UnityEngine.Font();
                    AssetDatabase.CreateAsset(font, text5);
                }
                float num2 = 0f;
                CharacterInfo[] array7 = new CharacterInfo[array6.Length];
                for (int l = 0; l < array6.Length; l++)
                {
                    FontMaker.Glyphs glyphs2 = array6[l];
                    Texture2D texture2D2 = glyphs2.GetTexture();
                    Rect rect = array3[num++];
                    if (num2 < (float)texture2D2.height)
                    {
                        num2 = (float)texture2D2.height;
                    }
                    CharacterInfo characterInfo = default(CharacterInfo);
                    characterInfo.index = glyphs2.GetCode();
                    float x = rect.x;
                    float y = rect.y;
                    float width = rect.width;
                    float height = rect.height;
                    characterInfo.uvBottomLeft = new Vector2(x, y);
                    characterInfo.uvBottomRight = new Vector2(x + width, y);
                    characterInfo.uvTopLeft = new Vector2(x, y + height);
                    characterInfo.uvTopRight = new Vector2(x + width, y + height);
                    characterInfo.minX = 0;
                    characterInfo.minY = -texture2D2.height;
                    characterInfo.maxX = texture2D2.width;
                    characterInfo.maxY = 0;
                    characterInfo.advance = texture2D2.width;
                    array7[l] = characterInfo;
                }
                font.characterInfo = array7;
                font.material = material;
                SerializedObject serializedObject = new SerializedObject(font);
                serializedObject.Update();
                SerializedProperty serializedProperty = serializedObject.FindProperty("m_LineSpacing");
                serializedProperty.floatValue = num2;
                serializedObject.ApplyModifiedPropertiesWithoutUndo();
                EditorUtility.SetDirty(font);
            }
        }
        progressIndicator.SetProgress(0.95f);
        progressIndicator.Show("Save and refresh assets.");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
