using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglyLinkedTeleporter : MonoBehaviour
{
    public GameObject destination;

    [Header("Settings for Rival")]
    public HorizontalDirection rivalDirectionAtDestination = HorizontalDirection.Left;
    public bool rivalCanGoThere = true;

    [Header("Internal State")]
    private bool teleportKeyPressed = false;

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
        switch (other.gameObject.tag)
        {
            case "Player":
                if (teleportKeyPressed)
                {
                    MoveObjectViaStairs(other.gameObject);
                }
                break;
            case "Rival":
                if (rivalCanGoThere)
                {
                    MoveObjectViaStairs(other.gameObject);
                }
                break;
        }
    }

    void MoveObjectViaStairs(GameObject objectToMove)
    {
        Debug.Log("Teleporting...");
        Vector3 teleportPosition = destination.transform.position;
        teleportPosition.z = objectToMove.transform.position.z;
        objectToMove.transform.position = teleportPosition;
    }
}
