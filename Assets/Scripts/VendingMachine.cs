using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachine : MonoBehaviour
{
    //static public VendingMachine VM;

    //[Header("Set dynamically")]
    //public bool inProximity = false;

    public int cansInitiallyInStock = 1;

    [SerializeField]
    private int cansInStock;

    /*
    void Awake()
    {

    }

    if

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        float distance = Vector3.Distance(pos, Movement.Player.transform.position);

        if (distance <= 3)
        {
            Movement.Player.PowerUp();
        }
    }
    */

    void Start()
    {
        cansInStock = cansInitiallyInStock;
    }

    public int Vend()
    {
        return (--cansInStock >= 0) ? 1 : 0;
    }
}
