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
    

        if (this.transform.childCount > 0) {
            playerInventoryCanvas = this.transform.GetComponentInChildren<Canvas>().gameObject;
        } else {
            Debug.Log("Warning! PlayerInventoryDisplay.cs\n\tGameObject" + this.name + "has no Canvas attached");
        }

        // Get rect transform of child Player_Inventory_Header object
        Transform t = playerInventoryCanvas.transform.Find("Player_Inventory_Header");

        if (t.GetComponent<RectTransform>() != null)
        {
            headerReferenceRect = t.GetComponent<RectTransform>();
            Debug.Log ("Found header!");
        } else {
            Debug.Log ("Error! PlayerInventoryDisplay.cs\n\tInventory Display Header not found in GameObject:" + this.name);
        }

        Debug.Log("Objects to Collect: " + objectsToCollect.Count);
        objectsToCollect_text = new GameObject[objectsToCollect.Count];
        for (int i = 0; i < objectsToCollect.Count; i++)
        {
            objectsToCollect_text[i] = Instantiate(inventoryItemText_Prefab,new Vector3(0, 0, 0), Quaternion.identity, 
                playerInventoryCanvas.transform);
        }

        SetInventoryTextMesh();
    }

    void SetInventoryTextMesh(){
        float headerYPos = headerReferenceRect.rect.position.y;
        Debug.Log(headerYPos);

        for (int i = 0; i < objectsToCollect_text.Length; i++)
        { 
            // objectsToCollect_text[i].text = "Object #" + i.ToString();
            // objectsToCollect_text[i].gameObject.GetComponent<RectTransform>().
            //     anchoredPosition.Set(headerReferenceRect.anchoredPosition.x + inventoryText_Xpos_Offset,
            //                          headerReferenceRect.anchoredPosition.y + inventoryText_Ypos_Offset);
            objectsToCollect_text[i].GetComponent<TextMeshProUGUI>().text = "Object #" + i.ToString();
            objectsToCollect_text[i].GetComponent<RectTransform>().
                SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 100, 400);
            objectsToCollect_text[i].GetComponent<RectTransform>().
                SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 
                    (playerInventoryCanvas.GetComponent<RectTransform>().rect.height / 2) + 30 + (i * 60), 50);

                // position.Set(headerReferenceRect.anchoredPosition.x + inventoryText_Xpos_Offset,
                //                      headerReferenceRect.anchoredPosition.y + inventoryText_Ypos_Offset);
            
        }
    }

    void setInventoryStyles(){

    }

    void updateInventoryDisplay(){
         
    }

}
