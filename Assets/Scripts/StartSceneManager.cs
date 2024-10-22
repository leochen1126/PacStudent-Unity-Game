using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    public void LoadLevel1()
    {
        SceneManager.LoadScene("Assessment3Scene");  // Replace with your Assessment 3 scene name
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("DesignIterationScene");  // Replace with your Design Iteration scene name
    }
}
