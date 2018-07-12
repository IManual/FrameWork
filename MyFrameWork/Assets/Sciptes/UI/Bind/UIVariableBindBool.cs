using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public abstract class UIVariableBindBool : UIVariableBind
{
    public enum BooleanLogic
    {
        And,
        Or
    }

    public enum CompareModeEnum
    {
        Less,
        LessEqual,
        Equal,
        Great,
        GreatEqual
    }

    /// <summary>
    ///单个绑定的bool变量 
    /// </summary>
    [Serializable]
    private class BoolVariable
    {
        [SerializeField]
        private string variableName;

        [SerializeField]
        private CompareModeEnum compareMode = CompareModeEnum.Equal;

        [SerializeField]
        private int referenceInt;

        [SerializeField]
        private float referenceFloat;

        [SerializeField]
        private bool reverse;

        [CompilerGenerated]
        public UIVariable variable;

        public string GetVariableName()
        {
            return variableName;
        }

        [CompilerGenerated]
        public UIVariable GetVariable()
        {
            return variable;
        }

        [CompilerGenerated]
        public void SetVariable(UIVariable uIVariable)
        {
            variable = uIVariable;
        }

        /// <summary>
        /// 获取当前bool值
        /// </summary>
        /// <returns></returns>
        public bool GetBoolean()
        {
            if (GetVariable() == null)
            {
                return false;
            }
            if (GetVariable().Type == UIVariableType.Boolean)
            {
                bool flag = GetVariable().GetBoolean();
                if (reverse)
                {
                    flag = !flag;
                }
                return flag;
            }
            if (GetVariable().Type == UIVariableType.Interger)
            {
                int integer = GetVariable().GetInteger();
                bool flag2 = false;
                switch (compareMode)
                {
                    case CompareModeEnum.Less:
                        flag2 = (integer < referenceInt);
                        break;
                    case CompareModeEnum.LessEqual:
                        flag2 = (integer <= referenceInt);
                        break;
                    case CompareModeEnum.Equal:
                        flag2 = (integer == referenceInt);
                        break;
                    case CompareModeEnum.Great:
                        flag2 = (integer > referenceInt);
                        break;
                    case CompareModeEnum.GreatEqual:
                        flag2 = (integer >= referenceInt);
                        break;
                }
                if (reverse)
                {
                    flag2 = !flag2;
                }
                return flag2;
            }
            if (GetVariable().Type == UIVariableType.Float)
            {
                float @float = GetVariable().GetFloat();
                bool flag3 = false;
                switch (compareMode)
                {
                    case CompareModeEnum.Less:
                        flag3 = (@float < referenceInt);
                        break;
                    case CompareModeEnum.LessEqual:
                        flag3 = (@float <= referenceInt);
                        break;
                    case CompareModeEnum.Equal:
                        flag3 = (@float == referenceInt);
                        break;
                    case CompareModeEnum.Great:
                        flag3 = (@float > referenceInt);
                        break;
                    case CompareModeEnum.GreatEqual:
                        flag3 = (@float >= referenceInt);
                        break;
                }
                if (reverse)
                {
                    flag3 = !flag3;
                }
                return flag3;
            }
            Debug.LogErrorFormat("Variable {0} type is {1}, does not support this variable type.",
                GetVariable().Name,
                GetVariable().Type
                );
            return false;
        }
    }

    [SerializeField, Tooltip("The boolean logic.")]
    private BooleanLogic booleanLogic;

    [SerializeField, Tooltip("The variables for calculate the boolean value.")]
    private BoolVariable[] variables;

    public new UIVariable FindVariable(string name)
    {
        return base.FindVariable(name);
    }

    /// <summary>
    /// 判断物体是否显示
    /// </summary>
    /// <returns></returns>
    protected bool GetResult()
    {
        if (variables == null)
        {
            return false;
        }
        if(booleanLogic == BooleanLogic.And)
        {
            bool flag = true;
            for (int i = 0; i < variables.Length; i++)
            {
                BoolVariable val = variables[i];
                if (val != null)
                {
                    flag = (flag && val.GetBoolean());
                }
            }
            return flag;
        }
        bool flag2 = false;
        for (int i = 0; i < variables.Length; i++)
        {
            BoolVariable val2 = variables[i];
            if (val2 != null)
            {
                flag2 = (flag2 || val2.GetBoolean());
            }
        }
        return flag2;
    }

    protected abstract void OnValueChanged();

    public override void BindVariables()
    {
        if (variables != null)
        {
            for (int i = 0; i < variables.Length; i++)
            {
                BoolVariable tmp = variables[i];
                if (!string.IsNullOrEmpty(tmp.GetVariableName()))
                {
                    tmp.SetVariable(FindVariable(tmp.GetVariableName()));
                    if (tmp.GetVariable() == null)
                    {
                        Debug.LogErrorFormat("{0} can not find a variable {1}", name, tmp.GetVariableName());
                    }
                    else
                    {
                        tmp.GetVariable().OnValueInitialized += new Action(this.OnValueChanged);
                        tmp.GetVariable().OnValueChanged += new Action(this.OnValueChanged);
                        tmp.GetVariable().AddBind(this);
                    }
                }
            }      
        }
        this.OnValueChanged();
    }

    public override void UnbindVariables()
    {
        if(variables != null)
        {
            for (int i = 0; i < variables.Length; i++)
            {
                BoolVariable tmp = variables[i];
                if (tmp.GetVariable() != null)
                {
                    tmp.GetVariable().OnValueInitialized -= new Action(this.OnValueChanged);
                    tmp.GetVariable().OnValueChanged -= new Action(this.OnValueChanged);
                    tmp.GetVariable().RemoveBind(this);
                    tmp.SetVariable(null);
                }
            }
        }
    }
}
