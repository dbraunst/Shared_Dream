using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class GenerateRenderTextureFromXRSettings : MonoBehaviour
{
    // Start is called before the first frame update

    public ExposeEyeTransforms settings;
    public Camera camera2;
    public Material timeWindow1URP;

    public RenderTexture generatedRenderTexture;
    void Start()
    {
        // settings = this.gameObject.GetComponent<ExposeEyeTransforms>();
        StartCoroutine(wait());
    }

    // Update is called once per frame
    void Update()
    {
        // Graphics.SetRenderTarget(generatedRenderTexture, 0, CubemapFace.Unknown, 0);
        // camera2.Render();
        // Graphics.SetRenderTarget(generatedRenderTexture, 0, CubemapFace.Unknown, 1);
        // camera2.Render();
    }
    void DrawTextures(){
        UnityEngine.XR.XRSettings.eyeTextureResolutionScale = 2.0f;
        
        RenderTextureDescriptor d = XRSettings.eyeTextureDesc;
        
        Debug.Log("xr settings: " + XRSettings.eyeTextureDesc.height + " " + XRSettings.eyeTextureDesc.width + " "
                        + XRSettings.renderViewportScale + " " + XRSettings.eyeTextureHeight + " " + XRSettings.eyeTextureWidth);
        d.width  = (int)(XRSettings.eyeTextureWidth);
        d.height = (int)(XRSettings.eyeTextureHeight);
        // d.width = Screen.width;
        // d.height = Screen.height;
        d.vrUsage = VRTextureUsage.OneEye;

        Debug.Log("H/W: " + d.height + d.width);

        generatedRenderTexture = new RenderTexture(d);
        generatedRenderTexture.dimension = UnityEngine.Rendering.TextureDimension.Tex2D;

        Debug.Log("desc settings: " + d.height + d.width);
        camera2.targetTexture = generatedRenderTexture;

//         Graphics.SetRenderTarget(generatedRenderTexture, 0, CubemapFace.Unknown, 0);
//         camera2.Render();
// Graphics.SetRenderTarget(generatedRenderTexture, 0, CubemapFace.Unknown, 1);
//         camera2.Render();

        Debug.Log("Generated Tex");
        timeWindow1URP.SetTexture("_BaseMap", generatedRenderTexture);
    }
    IEnumerator wait(){
        yield return new WaitForSeconds(2.0f);
        DrawTextures();

    }
}
