﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShield : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreLayerCollision(10,10);
        Physics.IgnoreLayerCollision(10, 12); // ignore powerups
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
