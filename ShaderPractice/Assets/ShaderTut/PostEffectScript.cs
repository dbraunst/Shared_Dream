using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PostEffectScript : MonoBehaviour
{

    public Material mat;
    
    void OnRenderImage( RenderTexture src, RenderTexture dest)
    {
        // src is fully rendered scene, normally sent to monitor
        // intercepting this to do things. 


        // Graphics.Blit(src, dest);
        // Can be overridden to 
        Graphics.Blit(src, dest, mat);
    }
}
