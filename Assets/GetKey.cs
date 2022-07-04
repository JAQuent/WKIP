using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetKey : MonoBehaviour{
    // Public vars
    public string currentKey;
    public static string textString;
    public static bool recording = false;
    public bool introMessageRemove = false;
    public List<string> axisNames;

    // Vars private
    private float scroll;
    private float refractoryPeriod = 1f;
    private bool axis1_refra = true;
    private float lastUsed1;
    private int numberOfAxes = 0;
    public List<float> lastUsed;
    public List<bool> axisRefra;

    void Start () {
        textString = "\nThis is WKIP, which will show you the Unity KeyCodes for the keys you pressed and axes you moved.";
        introMessageRemove = true;

        // Get number of axes
        numberOfAxes = axisNames.Count;
        Debug.Log("Axes " + numberOfAxes);

        // Create axes helper vars
        lastUsed = new List<float>(new float[numberOfAxes]);
        axisRefra = new List<bool>(new bool[numberOfAxes]);
     }

    void Update(){
        // Only if recording mode
        if(recording){
            // Remove the intro message once
            if(introMessageRemove){
                textString = "\n";
                introMessageRemove = false;
            }

            // Loop through buttons
            foreach(KeyCode kcode in System.Enum.GetValues(typeof(KeyCode))){
                if (Input.GetKeyDown(kcode)){
                    string tempString = "Input.GetKeyDown(KeyCode." + kcode.ToString() +")\n";
                    textString = textString + tempString;
                    Debug.Log(tempString);
                    currentKey = kcode.ToString();
                }
            }

            // Loop through axes
            for (int i = 0; i < numberOfAxes; i++){
                if(axisNames[i] == "Mouse ScrollWheel"){
                    // Special case
                    } else {
                        if (Mathf.Abs(Input.GetAxis(axisNames[i])) > 0.2){
                            // Is refractory period over?
                            if(axisRefra[i]){
                                string tempString = "Input.GetAxis(\"" + axisNames[i]+ "\")\n";
                                textString = textString + tempString;
                                Debug.Log("Mouse X");
                                lastUsed[i] = Time.time;
                                axisRefra[i] = false;
                            } else {
                                // Check if it is time to reset axisRefra[i]
                                if(Time.time - lastUsed[i] > refractoryPeriod){
                                     axisRefra[i] = true;
                            }
                        }
                    }
                }
            }

            scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll > 0f || scroll < 0f ){
                    Debug.Log("Mouse ScrollWheel");
            }  
        }
    }
}