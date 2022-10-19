using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISlimeBehavior
{
    void Enter(Slime slime);
    void Exit(Slime slime);
    void Update(Slime slime);
    void FixedUpdate(Slime slime);
}
