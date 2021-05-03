using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

[ExecuteAlways]
public class MainTexManager : MonoBehaviour
{
    // Start is called before the first frame update

    private Camera mainCam; 
    public Camera camera0;
    public Camera camera1;
    public Camera camera2;

    public RawImage cam1;

    private CommandBuffer _depthHackBuf;
    [SerializeField]
    private Renderer _depthHackQuad;


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        RenderTexture cam0_rt = new RenderTexture(Screen.width, Screen.height, 24);
        Shader.SetGlobalTexture("_Cam0RT", cam0_rt);
        RenderTexture cam1_rt = new RenderTexture(Screen.width, Screen.height, 24);
        Shader.SetGlobalTexture("_Cam0RT", cam1_rt);
        RenderTexture cam2_rt = new RenderTexture(Screen.width, Screen.height, 24);
        Shader.SetGlobalTexture("_Cam0RT", cam2_rt);  

        // camera0.targetTexture = cam0_rt;
        // camera1.targetTexture = cam1_rt;
        // camera2.targetTexture = cam2_rt;

        _depthHackBuf = new CommandBuffer();
        _depthHackBuf.ClearRenderTarget(true, true, Color.black, 0);
        _depthHackBuf.name = "Fancy Depth Magic";
        // _depthHackBuf.DrawRenderer(_depthHackQuad, new Material(Shader.Find("Hidden/DepthHack")));

        cam1.texture = cam0_rt;

    }

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
        Color.green, 0.5f);

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
