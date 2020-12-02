using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Teleporter : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject sight1;
    public GameObject sight2;

    Vector3 sight1pos;
    Vector3 sight2pos;

    [Header("Set Dynamically")]
    public float distance1;
    public float distance2;

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
        /*
        if (distance1 <= 5 || distance2 <= 5)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                DoTeleportation();
            }
        }*/


    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Stairwell: Enter collision with " + other.gameObject.tag);

        switch(other.gameObject.tag)
        {
            case "Player":
                if (Input.GetKeyDown(KeyCode.W))
                {
                    DoTeleportation();
                }
                break;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Stairwell: Exit collision with " + other.gameObject.tag);
    }

    public void DoTeleportation()
    {
        Vector3 newfloorposition;


        if (distance1 < distance2)
        {
            newfloorposition = new Vector3(sight2.transform.position.x, sight2.transform.position.y, Movement.Player.transform.position.z);
        }

        else
        {
            newfloorposition = new Vector3(sight1.transform.position.x, sight1.transform.position.y, Movement.Player.transform.position.z);
        }

        Movement.Player.transform.position = newfloorposition;
        Movement.Player.rb.velocity = new Vector3(0,0,0);
    }
}
