using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public SceneController _sceneController;

    void Start()
    {
        _sceneController = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _sceneController.LoadNextSceneSingle();
        }
    }
}
