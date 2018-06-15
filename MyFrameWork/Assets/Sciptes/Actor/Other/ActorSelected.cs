using System;
using UnityEngine;

public class ActorSelected : BaseBehaviour
{
    GameObject child;

    protected override void Awake()
    {
        child = transform.GetChild(0).gameObject;
        SetSelectedActive(false);
    }

    public void SetSelectedActive(bool state)
    {
        child.SetActive(state);
    }
}
