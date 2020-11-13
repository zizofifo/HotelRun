﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowd5 : MonoBehaviour
{
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        float distance = Vector3.Distance(pos, Movement.Player.transform.position);

        if (distance <= 3)
        {
            Movement.Player.inCrowd = true;
        }

        else
        {
            Movement.Player.inCrowd = false;
        }
    }
}