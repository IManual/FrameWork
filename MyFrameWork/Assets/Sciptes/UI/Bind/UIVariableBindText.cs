using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Nirvana/UI/Bind/Variable Bind Text"), RequireComponent(typeof(Text))]
public class UIVariableBindText : VariableBind {

    [Delayed, SerializeField, TextArea(2,100)]
    public string format;

    ////////////////[VariableName]
    private string[] paramBinds;

    private Text UIText;

    protected override void Awake()
    {
        TableToValue.RegistEvent((UIVariable variable) =>
        {
            this.UIText = GetComponent<Text>();

            this.variable = variable;

            if (this.format == "")
            {
                UIText.text = this.variable.value.ToString();
            }
            else
            {
                UIText.text = string.Format(this.format, this.variable.value);
            }
        }, this.variable.name);
    }
}
