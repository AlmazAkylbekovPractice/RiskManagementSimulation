using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeReturningBehavior : ISlimeBehavior
{
    void ISlimeBehavior.Enter(Slime slime)
    {
        slime.currentCoins.GetComponent<Coins>().isTaken = true;
        slime.stopLossBalance = GameManager.greenHouseBalance - GameManager.greenHouseBalance * (slime.riskManagementPercent / 100);
    }

    void ISlimeBehavior.Exit(Slime slime)
    {
        slime.currentCoins = null;
    }

    void ISlimeBehavior.Update(Slime slime)
    {
        if (slime.currentCoins != null)
            //Placing apple to apple holder
            slime.currentCoins.transform.position = slime.coinHolder.transform.position;



        //if (GameManager.greenHouseBalance <= slime.stopLossBalance)
        //{
        //    if (slime.currentCoins.GetComponent<Coins>().positiveDeal == false)
        //    {
        //        slime.currentCoins.GetComponent<Coins>().isChosen = false;
        //        slime.currentCoins.GetComponent<Coins>().isTaken = false;
        //        slime.currentCoins.GetComponent<Coins>().DeactivateCoins();
        //        slime.SetBehaviorChasing();
        //    }
        //}
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
