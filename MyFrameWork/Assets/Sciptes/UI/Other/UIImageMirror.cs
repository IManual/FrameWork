using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/Effects/Image Mirror"), RequireComponent(typeof(RectTransform))]
public class UIImageMirror : BaseMeshEffect
{
    public enum MirrorModeType
    {
        Horizontal,
        Vertical,
        Quad
    }

    [SerializeField, Tooltip("The mirror type")]
    private MirrorModeType mirrorMode;

    public MirrorModeType MirrorMode
    {
        get { return mirrorMode; }

        set
        {
            if (mirrorMode != value)
            {
                mirrorMode = value;
                if(base.graphic != null)
                {
                    base.graphic.SetVerticesDirty();
                }
            }
        }
    }

    private Image image;

    private Vector2 vector;

    public override void ModifyMesh(VertexHelper vh)
    {
        if (!this.IsActive() || vh.currentVertCount < 0)
        {
            return;
        }
        RectTransform rectTransform = base.transform as RectTransform;
        this.vector = rectTransform.rect.size;
        List<UIVertex> list = new List<UIVertex>(vh.currentVertCount);
        vh.GetUIVertexStream(list);
        //得到修改后增加的顶点
        List<UIVertex> list2 = this.ModifyVertices(list);
        if (list2 != null)
        {
            vh.Clear();
            vh.AddUIVertexTriangleStream(list2);
        }
        //list 为原数据 list2为镜像出来的数据 累加得到新图像
        vh.AddUIVertexTriangleStream(list);
    }

    private Image GetImage()
    {
        if (this.image == null)
        {
            this.image = base.GetComponent<Image>();
        }
        return this.image;
    }

    private List<UIVertex> ModifyVertices(List<UIVertex> list)
    {
        Image image = this.GetImage();
        if(image != null)
        {
            return this.GetVertex(list);
        }
        return list;
    }

    private List<UIVertex> GetVertex(List<UIVertex> list)
    {
        int num = 0;
        int count = list.Count;
        if(this.mirrorMode == UIImageMirror.MirrorModeType.Horizontal)
        {
            int num2 = list.Count * 2;
            if(list.Capacity < num2)
            {
                list.Capacity = num2;
            }
            this.Mirror(list, num, count, 0f, 0f, -this.vector.x / 2f, 0f, false);
            num = count;
            count = list.Count;
            this.Mirror(list, num, count, this.vector.x, 0f, -this.vector.x / 2f, 0f, true);
        }
        else if (this.mirrorMode == UIImageMirror.MirrorModeType.Vertical)
        {
            int num3 = list.Count * 2;
            if(list.Capacity < num3)
            {
                list.Capacity = num3; 
            }
            this.Mirror(list, num, count, 0f, 0f, 0f, this.vector.y / 2f, false);
            num = count;
            count = list.Count;
            this.Mirror(list, num, count, 0f, -this.vector.y, 0f, this.vector.y / 2f, true);
        }
        else
        {
            int num4 = list.Count * 4;
            if(list.Capacity < num4)
            {
                list.Capacity = num4;
            }
            this.Mirror(list, num, count, 0f, 0f, -this.vector.x / 2f, this.vector.y / 2f, false);
            num = count;
            count = list.Count;
            this.Mirror(list, num, count, this.vector.x, 0f, -this.vector.x / 2f, this.vector.y / 2f, false);
            num = count;
            count = list.Count;
            this.Mirror(list, num, count, 0f, -this.vector.y, -this.vector.x / 2f, this.vector.y / 2f, false);
            num = count;
            count = list.Count;
            this.Mirror(list, num, count, this.vector.x, -this.vector.y, -this.vector.x / 2f, this.vector.y / 2f, true);
        }
        return list;
    }

    private void Mirror(List<UIVertex> list, int num, int num2, float num3, float num4, float num5, float num6, bool flag = false)
    {
        int num7 = list.Count * 2;
        if (list.Capacity < num7)
        {
            list.Capacity = num7;
        }
        for (int i = num; i < num2; i++)
        {
            UIVertex uIVertex = list[i];
            if (!flag)
            {
                list.Add(uIVertex);
            }
            Vector3 position = uIVertex.position;
            int num8 = i % 6;
            switch (num8)
            {
                case 0:
                case 1:
                case 5:
                    position.x += num3;
                    break;
                case 2:
                case 3:
                case 4:
                    position.x += num5;
                    break;
            }
            switch (num8)
            {
                case 0:
                case 4:
                case 5:
                    position.y += num6;
                    break;
                case 1:
                case 2:
                case 3:
                    position.y += num4;
                    break;
            }
            uIVertex.position = position;
            list[i] = uIVertex;
        }
    }
}
