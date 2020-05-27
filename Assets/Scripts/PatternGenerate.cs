using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class PatternGenerate : MonoBehaviour
{
    private const int CUBESIZE = 64;

    // Stored pattern table
    List<string> pattern = new List<string>();
    UInt16[] ledValuesHex = {0xAD30, 0xB038, 0xDA21, 0x4BAC};

    // Control light sources
    private Light[] lights;
    //public GameObject lightParent;

    // Path to pattern.h
    string path = "pattern.h";
    

    // Start is called before the first frame update
    void Start()
    {
        // Create patterh.h
        if (!File.Exists(path))
        {
            createPatternFile();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Generate code on Enter Press
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //UInt16 test = 0xAD40;
            Debug.Log("Led status: ");
            readLedValues();
            generatePattern();
        }
    }

    void createPatternFile()
    {
        // Create pattern.h
        File.Create(path).Dispose();

        // Fill with the blank template
        pattern = File.ReadAllLines(path).ToList();
        pattern.Add("#ifndef __PATTERN_H__");
        pattern.Add("#define __PATTERN_H__\n");
        pattern.Add("// Includes");
        pattern.Add("//---------------------------------");
        pattern.Add("#include <stdint.h>        // Use uint_t");
        pattern.Add("#include <avr/pgmspace.h>  // Store patterns in program memory\n");
        pattern.Add("// Pattern that LED cube will display");
        pattern.Add("//--------------------------------- ");
        pattern.Add("const PROGMEM uint16_t pattern_table[][5] = {\n");
        pattern.Add("};");
        pattern.Add("#endif");
        File.WriteAllLines(path, pattern);
    }

    void generatePattern()
    {
        // Make list of patterns
        if (File.Exists(path))
        {
            pattern = File.ReadAllLines(path).ToList();
            pattern.Remove("#endif");
            pattern.Remove("};");

            pattern.Add("    {0x" + ledValuesHex[0].ToString("X") + ", 0x" + ledValuesHex[1].ToString("X") + ", 0x" + ledValuesHex[2].ToString("X") + ", 0x" + ledValuesHex[3].ToString("X") + ", 10},");
            pattern.Add("};");
            pattern.Add("#endif");
            File.WriteAllLines(path, pattern);
        }
        else
            createPatternFile();
    }

    void readLedValues()
    {
        UInt16 ledValueHex = 0;
        Array.Clear(ledValuesHex, 0, ledValuesHex.Length); // Clear array before every new reading
        
        // Iterate over every LED lightsource to find the values (on/off)
        for (int i = 0; i < CUBESIZE; i++)
        {
            // Check if LED is on or off
            if (gameObject.transform.GetChild(i).GetChild(0).GetComponent<Light>().enabled)
                ledValueHex += (byte)(1 << Math.Abs(i - CUBESIZE + 1)); // Bitshifts a '1' the correct order into a ushort variable
            else
                ledValueHex += (byte)(0 << Math.Abs(i - CUBESIZE + 1)); // Bitshifts a '0' the correct order into a ushort variable

            // Save hex value for UInt16 every 16th iteration (4 times total)
            if ((i + 1) % 16 == 0)
                ledValuesHex[((i + 1) / 16) - 1] = ledValueHex; // Save hex-value of pattern to array
            
            Debug.Log(ledValueHex.ToString("X"));
        }
    }
    void resetPattern() // Resets the generated pattern file
    { 

    }

    
}
