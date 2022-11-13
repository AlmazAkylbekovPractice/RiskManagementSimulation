using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;

public class SlimeChasingBehavior : ISlimeBehavior
{
    private Collider[] coins;
    private Vector3 direction;

    private float minDistance = Mathf.Infinity;

    void ISlimeBehavior.Enter(Slime slime)
    {
        //Clearing current coins bag lot
        slime.currentCoins = null;

        coins = Physics.OverlapSphere(slime.transform.position, slime.searchingRange, slime.coinsMask);

        foreach (Collider coin in coins)
        {
            if (!coin.GetComponent<Coins>().isChosen)
            {
                //Finding closest coins
                float distance = Vector3.Distance(slime.transform.position, coin.transform.position);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    slime.currentCoins = coin.gameObject;
                }
            }
        }

        slime.currentCoins.GetComponent<Coins>().isChosen = true;
        minDistance = Mathf.Infinity;

    }

    void ISlimeBehavior.Exit(Slime slime)
    {

    }

    void ISlimeBehavior.Update(Slime slime)
    {
        if (slime.currentCoins == null)
        {
            slime.SetBehaviorChasing();
        }

        if (Vector3.Distance(slime.transform.position, slime.currentCoins.transform.position) < 1f)
        {
            slime.SetBehaviorReturning();
        }
    }

    void ISlimeBehavior.FixedUpdate(Slime slime)
    {
        if (slime.currentCoins != null)
        {
            slime.body.velocity = (slime.currentCoins.transform.position - slime.transform.position).normalized * slime.speed;
            slime.transform.LookAt(slime.currentCoins.transform.position);
        }
    }
}
