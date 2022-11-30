using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDamageBehavior : ISlimeBehavior
{
    void ISlimeBehavior.Enter(Slime slime)
    {
        slime.DestorySlime();
    }

    void ISlimeBehavior.Exit(Slime slime)
    {

    }

    void ISlimeBehavior.Update(Slime slime)
    {

    }

    void ISlimeBehavior.FixedUpdate(Slime slime)
    {

    }
}
