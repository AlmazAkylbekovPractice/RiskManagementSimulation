using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;

public class SlimeChasingBehavior : ISlimeBehavior
{
    private Collider[] coins;
    private Vector3 direction;

    void ISlimeBehavior.Enter(Slime slime)
    {
        coins = Physics.OverlapSphere(slime.transform.position, slime.searchingRange, slime.coinsMask);

        foreach (Collider coin in coins)
        {
            if (!coin.GetComponent<Coins>().isChosen)
            {
                slime.currentCoins = coin.gameObject;
                slime.currentCoins.GetComponent<Coins>().isChosen = true;

                break;
            }
        }
    }

    void ISlimeBehavior.Exit(Slime slime)
    {

    }

    void ISlimeBehavior.Update(Slime slime)
    {
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
