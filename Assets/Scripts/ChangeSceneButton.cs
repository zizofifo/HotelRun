using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneButton : MonoBehaviour
{
    [SerializeField]
    public string targetSceneName;

    public void OnClicked()
    {
        // Catch possible Null Exception or let Unity move on?
        SceneManager.LoadScene(targetSceneName);
    }
}
