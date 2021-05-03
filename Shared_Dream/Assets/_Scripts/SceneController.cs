using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        // Adds in the second scene at the same time 
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }
}
