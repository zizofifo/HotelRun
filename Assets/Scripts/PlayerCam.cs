﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Camera))]
public class PlayerCam : MonoBehaviour
{
    [Header("Set in inspector")]
    public GameObject POI;
    public Movement player;
    public TextMeshProUGUI uitTimer; //Timer text
    public TextMeshProUGUI tmpSodas;
    public Keycap sodaKeyCap;

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

        tmpSodas.text = "Sodas: " + string.Format("{0:#0}", player.sodaCans);

        sodaKeyCap.SetEnableAppearance(!player.isMotivated);

        uitTimer.text = "Time: "+timerText;
    }
}
