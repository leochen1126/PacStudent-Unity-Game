using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
    public void ExitToStartScene()
    {
        SceneManager.LoadScene("StartScene"); 
    }
}
