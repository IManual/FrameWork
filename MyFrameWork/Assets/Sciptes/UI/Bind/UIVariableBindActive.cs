using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIVariableBindActive : UIVariableBind
{
    public bool IsActive;

    public bool IsReverse;

    protected override void Awake()
    {
        //TableToValue.RegistEvent((UIVariable variable) =>
        //{
        //    this.variable = variable;
        //    IsActive = (Boolean)variable.value;
        //    gameObject.SetActive(IsReverse ? !IsReverse : IsActive);
        //}, this.variable.name);
    }

    public override void BindVariables()
    {
        throw new NotImplementedException();
    }

    protected override void OnDestroy()
    {
        
    }

    public override void UnbindVariables()
    {
        throw new NotImplementedException();
    }
}
