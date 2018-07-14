using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[AddComponentMenu("UI/Bind/Variable Bind Active")]
public class UIVariableBindActive : UIVariableBindBool
{
    public enum TransitionModeEnum
    {
        Instant,
        Fade
    }

    [SerializeField]
    private TransitionModeEnum transitionMode;

    [SerializeField]
    private float transitionTime = 0.1f;

    private CanvasGroup canvasGroup;

    protected override void OnValueChanged()
    {
        bool result = base.GetResult();
        if(transitionMode == TransitionModeEnum.Instant)
        {
            gameObject.SetActive(result);
        }
        else
        {
            if(canvasGroup == null)
            {//如果canvasGroup为空 找一次
                canvasGroup = gameObject.GetComponent<CanvasGroup>();
            }
            if (canvasGroup != null)
            {
                gameObject.SetActive(true);
                if (gameObject.activeInHierarchy)
                {
                    StopAllCoroutines();
                    if (result)
                    {
                        StartCoroutine(ShowOrHide(canvasGroup, 1f, true));
                    }
                    else
                    {
                        StartCoroutine(ShowOrHide(canvasGroup, 0f, false));
                    }
                }
                else
                {
                    gameObject.SetActive(result);
                }
            }
            else
            {
                gameObject.SetActive(result);
            }
        }
    }

    [DebuggerHidden]
    private IEnumerator ShowOrHide(CanvasGroup canvasGroup, float num, bool flag)
    {
        UIVariableBindActive bindActive = new UIVariableBindActive();
        //bindActive.canvasGroup = canvasGroup;
        //bindActive.transitionTime = num;
        //bindActive. = flag;

        return null;
    }
}
