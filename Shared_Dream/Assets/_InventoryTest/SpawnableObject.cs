using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableObject : MonoBehaviour
{
    public string _name;

    public bool isCollected = false;

    public PlayerInventoryDisplay inventory_owner;



    //Called upon spawn from PlayerInventoryDisplay.cs
    public void attachToInventoryOwner(PlayerInventoryDisplay owner)
    {
        inventory_owner = owner;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.Log("Raycast from SpawnableObject.cs");

            if(Physics.Raycast(ray, out hit)) {
                Debug.Log("Hit! " + hit.transform.name + "; " + this.transform.name);
                if (hit.transform.name == this.transform.name){
                    Debug.Log("This Object, " + this.transform.name + " was collected!");

                    OnCollected();
                }
            }
        }
    }

    void OnCollected()
    {
        isCollected = true;
        this.gameObject.SetActive(false);
        inventory_owner.updateUIOnCollection();
    }
}
