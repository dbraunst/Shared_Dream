using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerInventoryDisplay : MonoBehaviour
{

    //This is set from InventoryManager in AssignObjectsToPlayers()
    public List<GameObject> objectsToCollect;

    public int inventoryText_Xpos_Offset;
    public int inventoryText_Ypos_Offset;

    public GameObject inventoryItemText_Prefab;

    private GameObject[] objectsToCollect_textMesh;

    public GameObject playerInventoryCanvas;

    private RectTransform headerReferenceRect;

    // Start is called before the first frame update
    void Start()
    {
        IntializeInventoryDisplay();
    }

    public void IntializeInventoryDisplay(){
    
        // Attach Canvas child object to variable
        if (this.transform.childCount > 0) {
            playerInventoryCanvas = this.transform.GetComponentInChildren<Canvas>().gameObject;
        } else {
            Debug.Log("Warning! PlayerInventoryDisplay.cs\n\tGameObject" + this.name + "has no Canvas attached");
        }

        // Get rect transform of child Player_Inventory_Header object, set variable
        Transform t = playerInventoryCanvas.transform.Find("Player_Inventory_Header");
        if (t.GetComponent<RectTransform>() != null)
        {
            headerReferenceRect = t.GetComponent<RectTransform>();
            Debug.Log ("Found header!");
        } else {
            Debug.Log ("Error! PlayerInventoryDisplay.cs\n\tInventory Display Header not found in GameObject:" + this.name);
        }


        Debug.Log(this.transform.name + " has " + objectsToCollect.Count + "objects to Collect");
        foreach (GameObject inventoryObject in objectsToCollect)
        {
            inventoryObject.GetComponent<SpawnableObject>().attachToInventoryOwner(this);
        }

        SetInventoryTextMesh();
    }

    // This is called from SpawnableObject.cs to update when it's collected.
    public void updateUIOnCollection(){
        setInventoryStyles();
    }

    void SetInventoryTextMesh(){

        // Get our header position in screen space
        float headerYPos = headerReferenceRect.rect.position.y;
        
        // Create text mesh objects for each object in 'objectsToCollect' and attach to Canvas
        objectsToCollect_textMesh = new GameObject[objectsToCollect.Count];
        for (int i = 0; i < objectsToCollect.Count; i++)
        {
            // Instantiate using canvas as base transform
            objectsToCollect_textMesh[i] = Instantiate(inventoryItemText_Prefab, playerInventoryCanvas.transform.position, 
                Quaternion.identity, playerInventoryCanvas.transform);
        }

        for (int i = 0; i < objectsToCollect_textMesh.Length; i++)
        {
            // Add a lil hyphen to the beginning of the object name so it looks like a list
            objectsToCollect_textMesh[i].GetComponent<TextMeshProUGUI>().text = "- " + 
                objectsToCollect[i].GetComponent<SpawnableObject>()._name;

            objectsToCollect_textMesh[i].GetComponent<RectTransform>().anchorMin = new Vector2(0.15f, (0.60f - (0.15f * i)));
            objectsToCollect_textMesh[i].GetComponent<RectTransform>().anchorMax = new Vector2(0.85f, (0.75f - (0.15f * i)));
            objectsToCollect_textMesh[i].GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
        }
    }

    void setInventoryStyles(){
        for (int i = 0; i < objectsToCollect.Count; i++)
        {
            if (objectsToCollect[i].GetComponent<SpawnableObject>().isCollected == true)
            {
                objectsToCollect_textMesh[i].GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
            }
        }
    }
}
