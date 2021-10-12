using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicSceneTransition : MonoBehaviour
{
    public GameObject door;
    public float doorOpenSpeed = 10.0f;
    public AnimationCurve doorOpenCurve, skylightCurve;
    public Quaternion doorStartRotation;
    public Quaternion doorEndRotation;

    public SceneController _sceneController;

    public bool beginTransition;

    public float timeElapsed = 0;
    // Start is called before the first frame update
    void Start()
    {
        doorStartRotation = Quaternion.identity;
        doorEndRotation = Quaternion.Euler(0, 105, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)){
            beginTransition = true;
        }
    }

    private void FixedUpdate() {
        if (!beginTransition){
            return;
        }
        else
        {
            if (door.transform.rotation != doorEndRotation)
            {
                timeElapsed += Time.deltaTime;
                door.transform.rotation = Quaternion.Slerp(doorStartRotation, doorEndRotation, 
                    doorOpenCurve.Evaluate(timeElapsed));

                RenderSettings.ambientIntensity = skylightCurve.Evaluate(timeElapsed);

            } else {
                Debug.Log("Finished!");
                _sceneController.LoadNextSceneSingle();
            }
        }
    }
}
