using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCam : MonoBehaviour
{
    static public GameObject POI;

    [Header("Set in inspector")]
    public Text uitTimer; //Timer text

    [Header("Set dynamically")]
    public float camZ;
    public float timer = 0.0f;
    public int seconds;
    public int minutes;


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

        timer += Time.deltaTime;

        seconds = (int)(timer % 60);
        minutes = (int)(timer / 60);

        UpdateGUI();
    }

    void UpdateGUI()
    {
        string timerText = string.Format("{0:0}:{1:00}", minutes, seconds);

        uitTimer.text = "Time: "+timerText;
    }
}
