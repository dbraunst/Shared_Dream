using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraSync : MonoBehaviour
{
    // Start is called before the first frame update

    public Camera camera2;
    public Camera camera3;
    public Camera camera4;

    public float offsetDistance;
    private float cam2Offset_z;
    private float cam3Offset_z;
    private float cam4Offset_z;

    private Vector3 cam2Offset;
    private Vector3 cam3Offset;
    private Vector3 cam4Offset;

    void Start()
    {
        cam2Offset_z = offsetDistance;
        cam3Offset_z = offsetDistance * 2.0f;
        cam4Offset_z = offsetDistance * 3.0f;


        cam2Offset = new Vector3(0, 0, cam2Offset_z);
        cam3Offset = new Vector3(0, 0, cam3Offset_z);
        cam4Offset = new Vector3(0, 0, cam4Offset_z);

        Application.onBeforeRender += callOnBeforeRender;
    }

    private void callOnBeforeRender()
    {
        if (this.gameObject != null)
        {
            if (camera2 != null)
                updateCameraPos(camera2, cam2Offset);

            if (camera3 != null)
                updateCameraPos(camera3, cam3Offset);

            if (camera4 != null)    
                updateCameraPos(camera4, cam4Offset);
        }
    }

    void updateCameraPos(Camera _camera, Vector3 _offset) {
        if (_camera != null)
        {
            _camera.transform.localPosition = this.transform.localPosition + _offset;
            _camera.transform.rotation = this.transform.rotation;
        }
    }

}
