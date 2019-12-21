using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal interface IMovementHarness
{
    void setDirection(Vector2 direction);
    void Jump();
    
}
