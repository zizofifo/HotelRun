﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision obj)
    {
        switch (obj.gameObject.tag)
        {
            case "Player":
                SceneManager.LoadScene("VictoryScene");
                break;
            case "Rival":
                SceneManager.LoadScene("GameOverScene");
                break;
            default:
                break;
        }
    }
}
