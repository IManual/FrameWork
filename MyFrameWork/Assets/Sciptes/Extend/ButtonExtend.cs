using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using XLua;

public static class ButtonExtend
{
    public static void SetClickListener(this Button self, UnityAction callback)
    {
        self.onClick.AddListener(callback);
    }
}
