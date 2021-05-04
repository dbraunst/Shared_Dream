using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    //
    public int sceneToLoad;

    public LoadSceneMode loadSceneMode;
    
    //Awake is awake
    void Start()
    {
        // Adds in the second scene at the same time 
        if (loadSceneMode == LoadSceneMode.Additive)
        {
            SceneManager.LoadScene(sceneToLoad, loadSceneMode);
        }
    }

    public void LoadNextSceneSingle()
    {
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
    }
}
