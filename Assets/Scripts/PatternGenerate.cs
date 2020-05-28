using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using UnityEditor.PackageManager.Requests;
using System.Globalization;

public class PatternGenerate : MonoBehaviour
{
    public TMP_InputField inputField;

    private const int CUBESIZE = 64;
    private Light[] lights;
    ushort[] ledValuesHex = { 0, 0, 0, 0 };

    // Stored pattern table
    List<string> pattern = new List<string>();

    // Path to pattern.h
    string path = "pattern.h";
    

    // Start is called before the first frame update
    void Start()
    {
        //Adds a listener to the input field and invokes a method when the value changes.
        inputField.onValueChanged.AddListener(delegate { ValueChangeCheck(); });

        // Create patterh.h
        if (!File.Exists(path))
        {
            createPatternFile(path);
        }

        // Read file into input field
        ReadString(inputField);
       
    }

    // Update is called once per frame
    void Update()
    {
        // Generate code on Enter Press
        if (Input.GetKeyDown(KeyCode.Return))
        {
            readLedValues();
            generatePattern();

            // Protects ValueChangeCheck() from memory 
            ReadString(inputField);
           
        }
    }

    // Invoked when the value of the text field changes.
    public void ValueChangeCheck()
    {
        /*
        if (!readingFile) 
            WriteString(inputField);
        */
    }

    

    void createPatternFile(string path)
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
        pattern.Add("#include <avr/pgmspace.h>  // St3ore patterns in program memory\n");
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

            // Remove end of file
            pattern.Remove("#endif");
            pattern.Remove("};");

            // Add new pattern
            pattern.Add("    {0x" + ledValuesHex[0].ToString("X") + ", 0x" + ledValuesHex[1].ToString("X") + ", 0x" + ledValuesHex[2].ToString("X") + ", 0x" + ledValuesHex[3].ToString("X") + ", 10},");
            pattern.Add("};");
            pattern.Add("#endif");
            File.WriteAllLines(path, pattern);
        }
        else
            createPatternFile(path);
    }

    void readLedValues()
    {
        ushort ledValueHex = 0;
        //Array.Clear(ledValuesHex, 0, ledValuesHex.Length); // Clear array before every new reading
        int j = 0;

        // Iterate over every LED lightsource to find the values (on/off)
        for (int i = 0; i < CUBESIZE; i++)
        {

            // Check if LED is on or off
            if (gameObject.transform.GetChild(i).GetChild(0).GetComponent<Light>().enabled == true)
            {
                // Needed for correct calculation of bitshitft
                if (((j + 1) % 16) == 0)
                    j = 0;
                else
                    j++;

                //Debug.Log("LED " + i + " was on!");
                ledValueHex += (ushort)(1 << j); // Bitshifts a '1' the correct order into a ushort variable
            }
            else
            {
                ledValueHex += (ushort)(0 << j); // Bitshifts a '0' the correct order into a ushort variable
                //Debug.Log("LED " + i + " was off!");
            }

            // Save hex value for UInt16 every 16th iteration (4 times total)
            if ((i + 1) % 16 == 0)
            {
                ledValuesHex[((i + 1) / 16) - 1] = ledValueHex; // Save hex-value of pattern to array
                ledValueHex = 0;
            }
        }
    }

    [MenuItem("Tools/Write file")]
    static void WriteString(TMP_InputField inputField)
    {
        string path = "pattern.h";

        // Erase contents of file 
        System.IO.File.WriteAllText(path, string.Empty);

        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(inputField.text);
        writer.Close();

        //Re-import the file to update the reference in the editor
        AssetDatabase.ImportAsset(path);
        TextAsset asset = (TextAsset)Resources.Load("test");

        //Print the text from the file
        //Debug.Log(asset.text);
    }

    [MenuItem("Tools/Read file")]
    static void ReadString(TMP_InputField inputField)
    {
        string path = "pattern.h";

        //Read the text from file
        StreamReader reader = new StreamReader(path);
        inputField.text = reader.ReadToEnd();

        //Debug.Log(reader.ReadToEnd());

        reader.Close();
    }
    
}
