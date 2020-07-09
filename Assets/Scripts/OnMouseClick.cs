using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class OnMouseClick : MonoBehaviour
{
    bool ledOn = false;
    RaycastHit hit;

    // List to store the on or off status of LEDs
    List<bool> ledStatus = new List<bool>();
    Dictionary<string, bool> ledStatusDic = new Dictionary<string, bool>();

    // Start is called before the first frame update
    void Start()
    {
        MeshCollider mc = gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;

        // Initialize state of LEDs to list
        for (int i = 1; i <= 64; i++)
        {
            ledStatusDic.Add("led" + i, false);
        }

        foreach (KeyValuePair<string, bool> led in ledStatusDic)
        {
            //textBox3.Text += ("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            Console.WriteLine("Key = {0}, Value = {1}", led.Key, led.Value);
        }
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

                    // Save name of clicked LED
                    string clickedLed = hit.transform.gameObject.name;

                    // Save the status of the LED
                    bool ledStatus;

                    // Look up the clicked LEDs status in dictionary and save to ledStatus
                    ledStatusDic.TryGetValue(clickedLed, out ledStatus);

                    // Toggle LED light
                    if (ledStatus)
                    {
                        hit.transform.gameObject.transform.GetChild(0).GetComponent<Light>().enabled = false;
                        ledStatusDic[clickedLed] = false;
                          
                    }
                    else
                    {
                        hit.transform.gameObject.transform.GetChild(0).GetComponent<Light>().enabled = true;
                        ledStatusDic[clickedLed] = true;
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
