using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spin : MonoBehaviour
{
    [SerializeField]
    Vector3 axis;

[SerializeField]
    float RotationRatio=1;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position,axis,Mathf.PI/RotationRatio);
    }
}
