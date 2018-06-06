using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UITest : BaseBehaviour {

    protected override void Awake()
    {
        GameObject go =  AssetLoaderManager.LoadAsset<GameObject>("uis/views/mainui", "MainView");
        Instantiate(go);
    }

    protected override void Start()
    {
    }
}

