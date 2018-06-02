using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Sprites;
using UnityEngine.UI;

public sealed class CircleImage : Image
{
    [Range(4f, 360f), SerializeField]
    private int segmentCount = 36;

    [Range(-100f, 100f), SerializeField]
    private int fillPercent = 100;

    public int SegMentCount
    {
        get { return this.segmentCount; }
        set
        {
            if (this.segmentCount != value)
            {
                this.SegMentCount = value;
                this.SetVerticesDirty();

                //标记目标物体已改变。
                EditorUtility.SetDirty(base.transform);
            }
        }
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
        Vector2 pivot = this.rectTransform.pivot;
        Rect rect = this.rectTransform.rect;
        float num = -pivot.x * rect.width;
        float width = this.rectTransform.rect.width;
        float height = this.rectTransform.rect.height;
        Vector4 vector = (!(base.overrideSprite != null)) ? Vector4.zero : DataUtility.GetOuterUV(base.overrideSprite);
        float num2 = (vector.x + vector.z) * 0.5f;
        float num3 = (vector.y + vector.w) * 0.5f;
        float num4 = (vector.z - vector.x) / width;
        float num5 = (vector.w - vector.y) / height;
        float num6 = (float)this.fillPercent / 100f * 6.28318548f / (float)this.segmentCount;
        float num7 = 0f;
        Vector2 vector2 = Vector2.zero;
        for (int i = 0; i < this.segmentCount + 1; i++)
        {
            float num8 = Mathf.Cos(num7);
            float num9 = Mathf.Sin(num7);
            Vector2 vector3 = vector2;
            Vector2 vector4 = new Vector2(num * num8, num * num9);
            Vector2 zero = Vector2.zero;
            Vector2 zero2 = Vector2.zero;
            vector2 = vector4;
            Vector2 uv = new Vector2(vector3.x * num4 + num2, vector3.y * num5 + num3);
            Vector2 uv2 = new Vector2(vector4.x * num4 + num2, vector4.y * num5 + num3);
            Vector2 uv3 = new Vector2(zero.x * num4 + num2, zero.y * num5 + num3);
            Vector2 uv4 = new Vector2(zero2.x * num4 + num2, zero2.y * num5 + num3);
            UIVertex[] array = new UIVertex[]
            {
                new UIVertex
                {
                    color = this.color,
                    position = vector3,
                    uv0 = uv
                },
                new UIVertex
                {
                    color = this.color,
                    position = vector4,
                    uv0 = uv2
                },
                new UIVertex
                {
                    color = this.color,
                    position = zero,
                    uv0 = uv3
                },
                new UIVertex
                {
                    color = this.color,
                    position = zero2,
                    uv0 = uv4
                }
            };
            vh.AddUIVertexQuad(array);
            num7 += num6;
        }
    }
}
