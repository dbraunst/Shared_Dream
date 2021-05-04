using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class ExposeEyeTransforms : MonoBehaviour
{
    public XRRig rig;

    public int eyeTexHeight;
    public int eyeTexWidth;

    public RenderTextureDescriptor eyeTexDesc;

    public int rtWidth;
    public int rtHeight;
    public int depthBits;

    
    // Start is called before the first frame update
    void Start()
    {
        eyeTexDesc.width = XRSettings.eyeTextureWidth;
        eyeTexDesc.height = XRSettings.eyeTextureHeight;
        eyeTexDesc = XRSettings.eyeTextureDesc;
        
        StartCoroutine(wait());
    }

    // Update is called once per frame
    void Update()
    {
        eyeTexHeight = XRSettings.eyeTextureHeight;
        eyeTexWidth = XRSettings.eyeTextureWidth;
        rtWidth = eyeTexDesc.height;
        rtHeight = eyeTexDesc.width;
        depthBits = eyeTexDesc.depthBufferBits;
    }

    void printDebugInfo(){
        
        Debug.Log("EyeTextHeight: " + XRSettings.eyeTextureHeight);
        Debug.Log("EyeTextWidth: " + XRSettings.eyeTextureWidth);
        // Debug.Log("Graphics Format:" + XRSettings.)
        
        // Tex2D and Tex2Darray
        Debug.Log("EyeTexDim: " + eyeTexDesc.dimension + XRSettings.deviceEyeTextureDimension);
    }

    IEnumerator wait(){
        yield return new WaitForSeconds(2.0f);
        printDebugInfo();

    }
}
