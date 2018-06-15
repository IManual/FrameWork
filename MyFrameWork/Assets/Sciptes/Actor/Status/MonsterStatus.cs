using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStatus : ActorStatus
{
    public override void Death()
    {
        base.Death();
        Destroy(this.gameObject, 3f);
    }
}
