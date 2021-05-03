using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickThrough : MonoBehaviour
{
    // Start is called before the first frame update

    public Camera mainCam; 
    public Camera camera1;
    public Camera camera2;

    void Start()
    {
        mainCam = GetComponent<Camera>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetMouseButton(0))
            {return;}

        RaycastHit hit_0;
        RaycastHit hit_1;
        RaycastHit hit_2;

        if (Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out hit_0))
        {}
        
        if (Physics.Raycast(camera1.ScreenPointToRay(Input.mousePosition), out hit_1))
        {}

        if (Physics.Raycast(camera2.ScreenPointToRay(Input.mousePosition), out hit_2))
        {
            // UnityEngine.Debug.Log("Raycast hit: " + hit_2.transform.name);
            // if (hit_2.transform.gameObject.layer == 12)
            // {
            UnityEngine.Debug.Log("Cam 2 Hit!" + hit_2.transform.name);
            // }
        }   
        UnityEngine.Debug.Log("Younk");
        Ray mainCamRay = mainCam.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(mainCamRay.origin, mainCamRay.direction * 50.0f, 
        Color.green, 2.5f);

        Ray cam1ray = camera1.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(cam1ray.origin, cam1ray.direction * 50.0f, 
        Color.red, 0.5f);

        // Renderer rend = hit_0.transform.GetComponent<Renderer>();
        // MeshCollider meshCollider = hit_0.collider as MeshCollider;


        // UnityEngine.Debug.Log(rend.material.GetColor("white"));
        // // rend.material.shader.maximumLOD

        // if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshCollider == null)
        //     return;

        // Texture2D tex = rend.material.mainTexture as Texture2D;
        // Vector2 pixelUV = hit_0.textureCoord;

    }
}
