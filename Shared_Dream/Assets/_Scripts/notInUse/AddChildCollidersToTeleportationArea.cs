using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AddChildCollidersToTeleportationArea : MonoBehaviour
{

    public TeleportationArea tpArea;

    // public List<Collider> _colliders;

    public int numChildObjects;

    public Collider[] childColliders;
    // Start is called before the first frame update
    private void Awake()
    {
        tpArea = this.gameObject.GetComponent<TeleportationArea>();
        // _colliders = tpArea.colliders;
        numChildObjects = this.transform.childCount;

        childColliders = this.transform.GetComponentsInChildren<Collider>();

        foreach(Collider col in childColliders) {
            tpArea.colliders.Add(col);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
