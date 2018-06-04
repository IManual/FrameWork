using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XLua;
using UnityEngine.Events;

public static class ToggleExtend
{
    public static void AddValueChangedListener(this Toggle self, UnityAction<bool> call)
    {
        self.onValueChanged.AddListener(call);
    }
}
