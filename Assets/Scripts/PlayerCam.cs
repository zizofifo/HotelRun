using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    static public GameObject POI;

    [Header("Set dynamically")]
    public float camZ;


    void Start()
    {
        camZ = this.transform.position.z;    
    }

    
    void FixedUpdate()
    {
        if (POI == null) return;

        Vector3 pos = POI.transform.position;

        pos.z = camZ;

        transform.position = pos;
    }
}
