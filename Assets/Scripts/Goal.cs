using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{

    static public int stageCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D obj)
    {
        switch (obj.gameObject.tag)
        {
            case "Player":

                Scene scene = SceneManager.GetActiveScene();
                string sceneName = scene.name;
                if (sceneName == "Stage01Scene")
                {
                    SceneManager.LoadScene("Stage02Scene");
                    break;
                }
                else if (sceneName == "Stage02Scene")
                {
                    SceneManager.LoadScene("Stage03Scene");
                    break;
                    
                }
                else
                {
                    SceneManager.LoadScene("VictoryScene");
                    break;
                }

            case "Rival":
                SceneManager.LoadScene("GameOverScene");
                break;
            default:
                break;
        }
    }
}
