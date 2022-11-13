using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeReturningBehavior : ISlimeBehavior
{
    void ISlimeBehavior.Enter(Slime slime)
    {
        if (slime.currentCoins != null)
        {
            GameManager.instance.CountDeals(slime.slimeType);

            slime.currentCoins.GetComponent<Coins>().isTaken = true;
            slime.currentCoins.GetComponent<Coins>().slimeType = slime.slimeType;

            slime.stopLossBalance = GameManager.GetSlimeType(slime.slimeType) - GameManager.GetSlimeType(slime.slimeType) * (slime.riskManagementPercent / 100);
        }
        else
        {
            slime.SetBehaviorChasing();
        }
    }

    void ISlimeBehavior.Exit(Slime slime)
    {
        slime.currentCoins = null;
    }

    void ISlimeBehavior.Update(Slime slime)
    {
        if (slime.currentCoins == null)
        {
            slime.SetBehaviorChasing();
        }

        if (slime.currentCoins != null)
            //Placing apple to apple holder
            slime.currentCoins.transform.position = slime.coinHolder.transform.position;


        //Counting Stop Loss and losing bag
        if (GameManager.GetSlimeType(slime.slimeType) <= slime.stopLossBalance && slime.currentCoins.GetComponent<Coins>().positiveDeal == false)
        {
            slime.currentCoins.GetComponent<Coins>().isTaken = false;
            slime.SetBehaviorChasing();
        }

        if (Vector3.Distance(slime.transform.position, slime.bag.transform.position) < 1)
        {
            Slime.Destroy(slime.currentCoins);
            slime.SetBehaviorChasing();
        }
    }

    void ISlimeBehavior.FixedUpdate(Slime slime)
    {
        if (slime.currentCoins != null)
        {
            slime.currentCoins.GetComponent<Rigidbody>().position = slime.coinHolder.transform.position;
            slime.currentCoins.GetComponent<Rigidbody>().freezeRotation = true;
            slime.body.velocity = (slime.bag.transform.position - slime.transform.position).normalized * slime.speed;
            slime.transform.LookAt(slime.bag.transform.position);
        }
    }
}
