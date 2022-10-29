using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeReturningBehavior : ISlimeBehavior
{
    void ISlimeBehavior.Enter(Slime slime)
    {
        slime.currentCoins.GetComponent<Coins>().isTaken = true;
    }

    void ISlimeBehavior.Exit(Slime slime)
    {

    }

    void ISlimeBehavior.Update(Slime slime)
    {
        if (slime.currentCoins != null)
            //Placing apple to apple holder
            slime.currentCoins.transform.position = slime.coinHolder.transform.position;
    }

    void ISlimeBehavior.FixedUpdate(Slime slime)
    {
        if (slime.currentCoins != null)
        {
            slime.currentCoins.GetComponent<Rigidbody>().position = slime.coinHolder.transform.position;
            slime.currentCoins.GetComponent<Rigidbody>().freezeRotation = true;
            slime.body.velocity = (slime.campfire.transform.position - slime.transform.position).normalized * slime.speed;
            slime.transform.LookAt(slime.campfire.transform.position);
        }
    }
}
