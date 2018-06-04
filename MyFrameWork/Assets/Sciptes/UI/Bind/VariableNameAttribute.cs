using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableNameAttribute : PropertyAttribute
{
    private int i;

    public VariableNameAttribute()
    {
        this.i = 31;
    }

    public VariableNameAttribute(UIVariableType t1)
    {
        this.i = 1 << (int)t1;
    }

    public VariableNameAttribute(UIVariableType t1, UIVariableType t2)
    {
        this.i = (1 << (int)t1 | 1 << (int)t2);
    }

    public VariableNameAttribute(UIVariableType t1, UIVariableType t2, UIVariableType t3)
    {
        this.i = (1 << (int)t1 | 1 << (int)t2 | 1 << (int) t3);
    }

    public VariableNameAttribute(UIVariableType t1, UIVariableType t2, UIVariableType t3, UIVariableType t4)
    {
        this.i = (1 << (int)t1 | 1 << (int)t2 | 1 << (int)t3 | i << (int)t4);
    }
}
