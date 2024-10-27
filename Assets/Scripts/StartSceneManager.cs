using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    public void LoadLevel1()
    {
        SceneManager.LoadScene("SampleScene"); 
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("DesignIterationScene");  
    }
}
