using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeReturningBehavior : ISlimeBehavior
{
    void ISlimeBehavior.Enter(Slime slime)
    {

    }

    void ISlimeBehavior.Exit(Slime slime)
    {

    }

    void ISlimeBehavior.Update(Slime slime)
    {
        if (slime.currentApple != null)
            //Placing apple to apple holder
            slime.currentApple.transform.position = slime.appleHolder.transform.position;
    }

    void ISlimeBehavior.FixedUpdate(Slime slime)
    {
        if (slime.currentApple != null)
        {
            slime.currentApple.GetComponent<Rigidbody>().position = slime.appleHolder.transform.position;

            slime.body.velocity = (slime.campfire.transform.position - slime.transform.position).normalized * slime.speed;
            slime.transform.LookAt(slime.campfire.transform.position);
        }
    }
}
