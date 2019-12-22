using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendPiston : MonoBehaviour, IToggleable
{
    [SerializeField]
    Transform piston_head, extended_target, retracted_target;
    [SerializeField]
    float speed;

    public bool state { get; private set; }

    public void ToggleState()
    {
        state = !state;
    }
    private void Update()
    {
        if (state)
            piston_head.position = Vector3.Lerp(piston_head.position, extended_target.position, speed);
        else
            piston_head.position = Vector3.Lerp(piston_head.position, retracted_target.position, speed);
    }
}
