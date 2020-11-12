using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceMachine : MonoBehaviour
{
    static public IceMachine IM;

    [Header("Set Dynamically")]
    public bool isCrossed = false;
    //butts
    
    void Awake()
    {
        if (IM == null)
        {
            IM = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        float distance = Vector3.Distance(pos, Movement.M.transform.position);

        if (distance <= 3)
        {
            isCrossed = true;
        }

        else
        {
            isCrossed = false;
        }

    }
}
