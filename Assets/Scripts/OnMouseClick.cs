using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class OnMouseClick : MonoBehaviour
{
    bool ledOn = false;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        MeshCollider mc = gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;
    }
    
    // Update is called once per frame
    void Update()
    {
        // On left mouse click 
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform != null)
                {
                    // Print name of clicked object
                    PrintName(hit.transform.gameObject);

                    // Toggle LED light
                    if (ledOn)
                    {
                        hit.transform.gameObject.transform.GetChild(0).GetComponent<Light>().enabled = false;
                        ledOn = false;
                    }
                    else
                    {
                        hit.transform.gameObject.transform.GetChild(0).GetComponent<Light>().enabled = true;
                        ledOn = true;
                    }
                    
                }
            }
        }
    }

    private void PrintName(GameObject go)
    {
        print(go.name);
    }

}
