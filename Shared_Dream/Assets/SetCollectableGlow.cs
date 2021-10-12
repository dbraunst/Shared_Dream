using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCollectableGlow : MonoBehaviour
{   
    public Light light;

    public float distanceToPlayer;

    public float radius = 2.0f;

    public float maxLightDistance = 0.5f;

    public float rampDist;

    public float m_value;
    public float m_value_scaled;
    
    // Start is called before the first frame update
    void Start()
    {
        light = this.gameObject.GetComponent<Light>();
        rampDist = radius - maxLightDistance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")){
            light.enabled = true;
            light.color = Color.HSVToRGB(0.17f, 0.62f, 0.0f); 
            Debug.Log("Enter");
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player")){
            distanceToPlayer = Vector3.Distance(this.transform.position, other.transform.position);

            m_value = (distanceToPlayer - 0.5f) >= 0 ? (rampDist - (distanceToPlayer - 0.5f) / rampDist) : 0.0f;

            m_value_scaled = m_value * 0.15f;

            light.color = Color.HSVToRGB(0.17f, 0.62f, m_value_scaled);
            Debug.Log("Stay");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")){
            light.color = Color.HSVToRGB(0.17f, 0.62f, 0.0f);
            
            light.enabled = false; 
        }
    }
}
