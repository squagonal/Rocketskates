﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setTimeIntervel : MonoBehaviour
{
    [SerializeField]
    float time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale=time;
    }
}
