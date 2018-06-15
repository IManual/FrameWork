using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FSMTrigger
{
    public FSMTriggerID triggerID;

    public FSMTrigger(FSMTriggerID triggerID)
    {
        this.triggerID = triggerID;
    }

    public abstract bool HandleTrigger(BaseFsm fsm);
}
