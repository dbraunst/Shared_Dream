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
        // desc = new RenderTextureDescriptor(XRSettings.eyeTextureHeight, XRSettings.eyeTextureHeight, 
        //     UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_UNorm, 24);
        UnityEngine.XR.XRSettings.eyeTextureResolutionScale = 2.0f;
        
        RenderTextureDescriptor d = XRSettings.eyeTextureDesc;
        
        Debug.Log("xr settings: " + XRSettings.eyeTextureDesc.height + " " + XRSettings.eyeTextureDesc.width + " "
                        + XRSettings.renderViewportScale + " " + XRSettings.eyeTextureHeight + " " + XRSettings.eyeTextureWidth);
        d.width  = (int)(XRSettings.eyeTextureWidth / 2.0f);
        d.height = (int)(XRSettings.eyeTextureHeight * 0.9f);

        generatedRenderTexture = new RenderTexture(d);

        Debug.Log("desc settings: " + d.height + d.width);
        camera2.targetTexture = generatedRenderTexture;
        Debug.Log("Generated Tex");
        timeWindow1URP.SetTexture("_BaseMap", generatedRenderTexture);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
