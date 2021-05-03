using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastThroughWindow : MonoBehaviour
{
    int time_layermask_1 = 1 << 8;
    int time_layermask_2 = 1 << 9;
    int time_layermask_3 = 1 << 10;

    public Vector3 timeline_1_offset;
    public Camera timeline_1_camera;


    RaycastHit hit;
    RaycastHit[] hits;

    public Vector3 click_worldpos;
    public Vector3 viewspace;

    public Vector3 click_offsetWorldPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse Clicked");

            // Generate a ray and the world position of the click
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            click_worldpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Will need to use RayCastAll for overlapping layers to check that hit 
            //   is succesfful in two windows
            if (Physics.Raycast(ray, out hit, 100.0f, time_layermask_1))
            {
                // If it's a Time 1 window:
                if(hit.collider.gameObject.tag == "Timeline_1_window");
                {
                    Debug.Log("Hit Time 1 Window!");
                    
                    // Take our click in worldspace and add the X-offset of the second camera
                    click_offsetWorldPos = click_worldpos + timeline_1_offset;

                    // Generate a new raycast at originating from screen position on same vector as original ray
                    Ray interactRay = new Ray(click_offsetWorldPos, (hit.point - Camera.main.ScreenToWorldPoint(Input.mousePosition)));
                    Debug.DrawRay(click_offsetWorldPos, Vector3.Scale((hit.point - Camera.main.ScreenToWorldPoint(Input.mousePosition)), 
                        new Vector3(4.0f, 4.0f, 4.0f)), Color.cyan, 1.5f);

                    // if we're in contact, check to see if it's interactable . Spelling lol.
                    if (Physics.Raycast(interactRay, out hit, 1000.0f, time_layermask_1)){

                        Debug.Log("Hit Time 1 Something");
                        if (hit.collider.gameObject.tag == "Timeline_1_interactible"){
                            
                            // DO Stuff

                            // Debug generating sphere to visualize raycast hit
                            Debug.Log("Hit Timeline 1 Interactible");
                            GameObject newSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                            newSphere.transform.localScale.Set(0.1f, 0.1f, 0.1f);
                            newSphere.transform.position = hit.point;
                            GameObject.Destroy(newSphere, 1.5f);
                        }
                    }
                }
            }
        }
    }

    public IEnumerator destroyAfterWait(GameObject _obj, float seconds)
    {
        while (true){
            yield return new WaitForSeconds(seconds);
            Destroy(_obj);
        }
    }

    
}
