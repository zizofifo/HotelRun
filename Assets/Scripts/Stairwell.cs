using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairwell : MonoBehaviour
{
    public GameObject destination;

    bool teleportKeyPressed = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        teleportKeyPressed = Input.GetKeyDown(KeyCode.W);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && teleportKeyPressed)
        {
            Teleport(other.gameObject);
        }
    }

    void Teleport(GameObject objectToTeleport)
    {
        Debug.Log("Teleporting...");
        Vector3 teleportPosition = destination.transform.position;
        teleportPosition.z = objectToTeleport.transform.position.z;
        objectToTeleport.transform.position = teleportPosition;
    }
}
