using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;

public class SlimeChasingBehavior : ISlimeBehavior
{
    private Collider[] apples;
    private Vector3 direction;

    void ISlimeBehavior.Enter(Slime slime)
    {
        apples = Physics.OverlapSphere(slime.transform.position, slime.searchingRange, slime.applesMask);

        foreach (Collider apple in apples)
        {
            if (!apple.GetComponent<Apple>().IsTaken)
            {
                slime.currentApple = apple.gameObject;
                slime.currentApple.GetComponent<Apple>().IsTaken = true;

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
        if (slime.currentApple != null)
        {
            slime.body.velocity = (slime.currentApple.transform.position - slime.transform.position).normalized * slime.speed;
            slime.transform.LookAt(slime.currentApple.transform.position);
        }
    }
}
