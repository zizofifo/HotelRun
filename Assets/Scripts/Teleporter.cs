using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Teleporter : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject sight1;
    public GameObject sight2;
    

    [Header("Set Dynamically")]
    public float distance1;
    public float distance2;

    private Vector2 mousePos;

    //Update is called once per frame
    void Update()
    {
        /*if (input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            sight.transform.position = new Vector2(mousePos.x, mousePos.y);
        }*/

        Vector3 sight1pos = sight1.transform.position;
        Vector3 sight2pos = sight2.transform.position;

        distance1 = Vector3.Distance(sight1pos, Movement.Player.transform.position);
        distance2 = Vector3.Distance(sight2pos, Movement.Player.transform.position);

        if (distance1 <= 3 || distance2 <= 3)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                DoTeleportation();
            }
        }
    }

    public void DoTeleportation()
    {
        Vector3 newfloorposition;

        if (distance1 <= 3)
        {
            newfloorposition = new Vector3(sight2.transform.position.x, sight2.transform.position.y, Movement.Player.transform.position.z);
        }

        else
        {
            newfloorposition = new Vector3(sight1.transform.position.x, sight1.transform.position.y, Movement.Player.transform.position.z);
        }

        Movement.Player.transform.position = newfloorposition;
    }
}
