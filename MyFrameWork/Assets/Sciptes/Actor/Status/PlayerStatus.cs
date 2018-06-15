using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : ActorStatus
{
    public override void Death()
    {
        base.Death();
        //GetComponent<CharacterInputController>().enabled = false;
        print("GameOver");
    }
}
