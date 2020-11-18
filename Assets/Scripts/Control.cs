using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Control : MonoBehaviour{
    public void Play(){
        SceneManager.LoadScene("Stage01Scene");
    }

    public void Quit(){
        SceneManager.LoadScene("TitleScreenScene");
        //Application.Quit();
        //Debug.Log("You have successfully quit the game!");
    }
}