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
        settings = this.gameObject.GetComponent<ExposeEyeTransforms>();
        generatedRenderTexture = new RenderTexture(XRSettings.eyeTextureDesc);
        camera2.targetTexture = generatedRenderTexture;
        
    }

    // Update is called once per frame
    void Update()
    {
        timeWindow1URP.SetTexture("_BaseMap", generatedRenderTexture);
    }
}
