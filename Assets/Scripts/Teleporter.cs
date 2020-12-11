using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Teleporter : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject sight1;
    public GameObject sight2;

    public Vector3 sight1pos;
    public Vector3 sight2pos;

    [Header("Set Dynamically")]
    public float distance1;
    public float distance2;

    public float rivalDistance1;
    public float rivalDistance2;

    public HorizontalDirection rivalDirectionAtDestination2 = HorizontalDirection.Left;

    //private Vector2 mousePos;

    void Awake()
    {
        sight1pos = sight1.transform.position;
        sight2pos = sight2.transform.position;
    }

    //Update is called once per frame
    void Update()
    {
        /*if (input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            sight.transform.position = new Vector2(mousePos.x, mousePos.y);
        }*/

        distance1 = Vector3.Distance(sight1pos, Movement.Player.transform.position);
        distance2 = Vector3.Distance(sight2pos, Movement.Player.transform.position);

        rivalDistance1 = Vector3.Distance(sight1pos, RivalMovement.Rival.transform.position);
        rivalDistance2 = Vector3.Distance(sight2pos, RivalMovement.Rival.transform.position);

        /*if (Movement.Player.canWarp == true && Input.GetKeyDown(KeyCode.W))
        {
            Invoke("DoTeleportation", 2f);
        }*/
        
        if (distance1 <= 5 || distance2 <= 5)
        {
            if (!Movement.Player.canWarp)
            {
                Movement.Player.canWarp = true;
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                Movement.Player.canWarp = false;
                Movement.Player.isBeingWarped = true;
                Invoke("DoTeleportation", 2f);
            }
        }
        else
        {
            if (!Movement.Player.isBeingWarped)
            {
                Movement.Player.canWarp = false;
            }
        }

        if (rivalDistance1 <= 5)
        {
            Invoke("DoTeleportationRival", 2f);
        }

    }

    /*void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Stairwell: Enter trigger with " + other.gameObject.tag);

        switch(other.gameObject.tag)
        {
            case "Player":
                Movement.Player.canWarp = true;
                break;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Stairwell: Exit trigger with " + other.gameObject.tag);

        switch (other.gameObject.tag)
        {
            case "Player":
                Movement.Player.canWarp = false;
                break;
        }
    }*/

    public void DoTeleportation()
    {
        Vector3 newfloorposition;


        if (distance2 < distance1)
        {
            newfloorposition = new Vector3(sight1.transform.position.x, sight1.transform.position.y, Movement.Player.transform.position.z);
        }

        else
        {
            newfloorposition = new Vector3(sight2.transform.position.x, sight2.transform.position.y, Movement.Player.transform.position.z);
        }

        Movement.Player.transform.position = newfloorposition;
        Movement.Player.rb.velocity = new Vector3(0,0,0);
        Movement.Player.canWarp = true;
        Movement.Player.isBeingWarped = false;
    }

    public void DoTeleportationRival()
    {
        Vector3 newfloorposition;

        newfloorposition = new Vector3(sight2.transform.position.x, sight2.transform.position.y, RivalMovement.Rival.transform.position.z);

        RivalMovement.Rival.transform.position = newfloorposition;
        RivalMovement.Rival.currentDirection = rivalDirectionAtDestination2;
    }
}
