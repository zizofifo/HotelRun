using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowd : MonoBehaviour
{
    static public Crowd C;

    [Header("Set dynamically")]
    public bool inCrowd = false;

    void Awake()
    {
        if (C == null)
        {
            C = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        float distance = Vector3.Distance(pos, Movement.M.transform.position);

        if (distance <= 3)
        {
            inCrowd = true;
        }

        else
        {
            inCrowd = false;
        }
    }
}
