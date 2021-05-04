using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerInventoryDisplay : MonoBehaviour
{

    public List<GameObject> objectsToCollect;

    public TextMeshProUGUI InventoryDisplayHeader;

    public int inventoryText_Xpos_Offset;
    public int inventoryText_Ypos_Offset;

    public GameObject inventoryItemText_Prefab;

    private GameObject[] objectsToCollect_text;

    public GameObject playerInventoryCanvas;

    private RectTransform headerReferenceRect;

    // Start is called before the first frame update
    void Start()
    {
        IntializeInventoryDisplay();
    }

    // Update is called once per frame
    void Update()
    {

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

        // Create text mesh objects for each and attach to Canvas
        objectsToCollect_text = new GameObject[objectsToCollect.Count];
        for (int i = 0; i < objectsToCollect.Count; i++)
        {
            objectsToCollect_text[i] = Instantiate(inventoryItemText_Prefab,new Vector3(0, 0, 0), Quaternion.identity, 
                playerInventoryCanvas.transform);
        }

        SetInventoryTextMesh();
    }

    // This is called from SpawnableObject.cs to update when it's collected.
    public void updateUIOnCollection(){
        setInventoryStyles();
    }

    void SetInventoryTextMesh(){
        float headerYPos = headerReferenceRect.rect.position.y;
        // Debug.Log(headerYPos);

        for (int i = 0; i < objectsToCollect_text.Length; i++)
        { 
            objectsToCollect_text[i].GetComponent<TextMeshProUGUI>().text = "Object #" + i.ToString();
            objectsToCollect_text[i].GetComponent<RectTransform>().
                SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, inventoryText_Xpos_Offset, 400);
            objectsToCollect_text[i].GetComponent<RectTransform>().
                SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 
                    (playerInventoryCanvas.GetComponent<RectTransform>().rect.height / 2) + 30 - (i * inventoryText_Ypos_Offset), 50);
            
        }
    }

    void setInventoryStyles(){
        for (int i = 0; i < objectsToCollect.Count; i++)
        {
            if (objectsToCollect[i].GetComponent<SpawnableObject>().isCollected == true)
            {
                objectsToCollect_text[i].GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
            }
        }
    }
}
