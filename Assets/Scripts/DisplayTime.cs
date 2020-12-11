using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayTime : MonoBehaviour
{

    [Header("Set in inspector")]
    public Text endtimer;

    // Start is called before the first frame update
    void Start()
    {

        endtimer.text = "End time: " + Goal.endTime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
