using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class JumpBar : MonoBehaviour
{
    Image canvas;
    [SerializeField]
    public float value = 0;
    // Start is called before the first frame update
    void Start()
    {
        canvas = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        canvas.fillAmount = value;
    }
}
