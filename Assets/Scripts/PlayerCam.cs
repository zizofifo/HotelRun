using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Camera))]
public class PlayerCam : MonoBehaviour
{
    [Header("Set in inspector")]
    public GameObject POI;

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
