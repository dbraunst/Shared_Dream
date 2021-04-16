using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CameraSync : MonoBehaviour
{
    // Start is called before the first frame update

    public Camera redCamera;
    public Camera greenCamera;
    public Camera blueCamera;

    public float offsetDistance;
    private float redCameraOffset_x;
    private float greenCameraOffset_x;
    private float blueCameraOffset_x;

    private Vector3 redCamOffset;
    private Vector3 greenCamOffset;
    private Vector3 blueCamOffset;

    void Start()
    {
        redCameraOffset_x = offsetDistance;
        greenCameraOffset_x = offsetDistance * 2.0f;
        blueCameraOffset_x = offsetDistance * 3.0f;


        redCamOffset = new Vector3(redCameraOffset_x, 0, 0);
        greenCamOffset = new Vector3(greenCameraOffset_x, 0, 0);
        blueCamOffset = new Vector3(blueCameraOffset_x, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        updateCameraPos(redCamera, redCamOffset);
        updateCameraPos(greenCamera, greenCamOffset);
        updateCameraPos(blueCamera, blueCamOffset);
    }

    void updateCameraPos(Camera _camera, Vector3 _offset) {
        _camera.transform.position = this.transform.position + _offset;
        _camera.transform.rotation = this.transform.rotation;
    }

}
