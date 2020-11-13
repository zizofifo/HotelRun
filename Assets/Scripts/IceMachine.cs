using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceMachine : MonoBehaviour
{
    //static public IceMachine IM;

    //[Header("Set Dynamically")]
    //public bool isCrossed = false;
    //butts
    
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = this.transform.position;
        float distance = Vector3.Distance(pos, Movement.Player.transform.position);

        if (distance <= 3)
        {
            Movement.Player.crossedIceMachine = true;
        }

        else
        {
            Movement.Player.crossedIceMachine = false;
        }

    }
}
