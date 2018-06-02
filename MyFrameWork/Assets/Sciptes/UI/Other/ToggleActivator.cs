using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ToggleActivator : BaseBehaviour
{
    private Toggle toggle;

    public GameObject[] actives;

    public GameObject[] deactive;

    protected override void OnEnable()
    {
        this.toggle = GetComponent<Toggle>();
        Active(this.toggle.isOn);
        Deactive(!this.toggle.isOn);
        this.toggle.onValueChanged.AddListener((state) =>
        {
            Active(state);
            Deactive(!state);
        });
    }

    public void Active(bool state)
    {
        for (int i = 0; i < actives.Length; i++)
        {
            if (actives[i].gameObject != null)
                actives[i].gameObject.SetActive(state);
        }
    }

    public void Deactive(bool state)
    {
        for (int i = 0; i < deactive.Length; i++)
        {
            if (deactive[i].gameObject != null)
                deactive[i].gameObject.SetActive(state);
        }
    }
}
