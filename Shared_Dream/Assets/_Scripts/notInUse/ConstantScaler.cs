using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ConstantScaler : MonoBehaviour
{
    // Start is called before the first frame update

    public RenderTexture texture;
    public Material material;
    public Shader shader;

    public bool isRendererSet = false;

    Renderer rend;
    void Start()
    {
        rend = GetComponent<Renderer>();
        // Debug.Log("ConstantScaler.cs" + rend.material.mainTextureScale);

        // rend.material.mainTextureScale.Set(this.transform.localScale.x, this.transform.localScale.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isRendererSet)
            SetRenderer();

        rend.sharedMaterial.mainTextureScale.Set(this.transform.localScale.x, this.transform.localScale.y);
    }

    void SetRenderer(){
        rend = GetComponent<Renderer>();
        isRendererSet = true;
    }
}
