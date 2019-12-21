using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ColorPulse : MonoBehaviour
{
    SpriteRenderer sr;
    [SerializeField]
    float offset;
    [SerializeField]
    float scale=1;
    [SerializeField]
    float time_factor = 1;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float f = (Mathf.Sin(Time.time * time_factor) * scale + offset);
        sr.color = ColorHelper.toRGB(sr.color.toHSV() + (Vector4)Vector3.forward * f);
    }
}
