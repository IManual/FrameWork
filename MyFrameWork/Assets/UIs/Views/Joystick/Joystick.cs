using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Joystick : ScrollRect
{

    [Tooltip("滑动半径"), SerializeField]
    public float radius = 0;

    public Transform direction;

    private CanvasGroup direcCanvasGroup;

    Vector2 pos;

    Vector2 outPut;

    Vector3 rotation = new Vector3(0, 0, 0);

    private Vector2 lastPosition;

    private Vector2 nowPosition;

    private Vector2 dragPosition;

    private bool isDragJostick;

    private bool isDrag;

    public Action<Vector2> OnMove;

    protected override void Start()
    {
        RectTransform rect = transform as RectTransform;
        lastPosition = new Vector2(rect.sizeDelta.x, rect.sizeDelta.y);

        if (radius == 0)
        {
            radius = rect.sizeDelta.x * 0.5f;
        }
        if (direction == null)
        {
            direction = transform.FindHard("direction");
        }
        direcCanvasGroup = direction.GetComponent<CanvasGroup>();
        direcCanvasGroup.alpha = 0;
    }

    float distance = 0;
    bool isClick = false;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            nowPosition = Input.mousePosition;
            distance = 0;
            isClick = true;
        }
        if (Input.GetMouseButton(0) && isClick)
        {
            //拿到最新点击坐标
            dragPosition = Input.mousePosition;
            distance = Vector2.Distance(nowPosition, dragPosition);
            if (distance > 0.02f || distance < -0.02f)
            {
                isDrag = true;
                isClick = false;
            }
        }
        if (isDrag)
        {
            //如果在屏幕上进行了滑动
            Debug.Log(nowPosition); //nowPosition以屏幕左下角为原点坐标
            transform.position = new Vector3(nowPosition.x, nowPosition.y, 0);
            
            isDrag = false;
        }
        if (isDragJostick)
        {
            //Debug.Log("[" + content.anchoredPosition.x + "," + content.anchoredPosition.y + "]");
            rotation.x = content.anchoredPosition.x;
            rotation.y = content.anchoredPosition.y;
            if (content.anchoredPosition.x > 0)
            {
                rotation.z = -Vector2.Angle(transform.up, rotation);
            }
            else
            {
                rotation.z = Vector2.Angle(transform.up, rotation);
            }
            rotation.x = 0;
            rotation.y = 0;
            direction.rotation = Quaternion.Euler(rotation);
            direcCanvasGroup.alpha = 1;

            if (OnMove != null)
            {
                OnMove(content.anchoredPosition);
            }
        }
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        isDragJostick = true;
        content.anchoredPosition = eventData.pressPosition;
        base.OnBeginDrag(eventData);

    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        pos = content.anchoredPosition;
        if (pos.magnitude > radius)
        {
            pos = pos.normalized * radius;
            SetContentAnchoredPosition(pos.normalized * radius);
        }
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        rotation = new Vector3(0, 0, 0);
        direcCanvasGroup.alpha = 0;
        base.OnEndDrag(eventData);
        isDragJostick = false;
        transform.position = new Vector2(lastPosition.x, lastPosition.y);
    }
}
