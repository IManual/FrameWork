using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Layout/Radial Layout Group")]
public sealed class RadialLayoutGroup : LayoutGroup
{
    [SerializeField]
    private float distance;

    [Range(0f, 360f), SerializeField]
    private float minAngle;

    [Range(0f, 360f), SerializeField]
    private float maxAngle;

    [Range(0f, 360f), SerializeField]
    private float startAngle;

    public override void CalculateLayoutInputVertical()
    {
 
    }

    public override void SetLayoutHorizontal()
    {
        this.Layout();
    }

    public override void SetLayoutVertical()
    {
        this.Layout();
    }

    protected override void OnValidate()
    {
        base.OnValidate();
        this.Layout();
    }

    private void Layout()
    {
        this.m_Tracker.Clear();
        if(base.transform.childCount == 0)
        {
            return;
        }

        float num = (this.maxAngle - this.minAngle) / (float)base.transform.childCount;
        for (int i = 0; i < base.transform.childCount; i++)
        {
            RectTransform rectTransform = (RectTransform)base.transform.GetChild(i);
            float num2 = this.startAngle + (float)i * num;
            Vector3 vector = new Vector3(Mathf.Cos(num2 * 0.0174532924f), Mathf.Sin(num2 * 0.0174532924f));
            rectTransform.localPosition = vector * this.distance;
            DrivenTransformProperties drivenTransformProperties = (DrivenTransformProperties)52998;
            this.m_Tracker.Add(this, rectTransform, drivenTransformProperties);
            rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
        }
    }
}
