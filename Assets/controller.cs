using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Threading.Tasks;

public class controller : MonoBehaviour{
    // Vars public
    public Color recordingColour;
    public Color normalColour;
    public Text textField;
    public Text filePathTextField;
    public Text dateTextField;
    public Camera cam;

    // Vars private
    private System.DateTime currentTime;

    // Start is called before the first frame update
    void Start(){
        textField.text = "\n";
        currentTime = System.DateTime.Now;
        Debug.Log(currentTime);
        cam.backgroundColor = normalColour;
    }

    // Update is called once per frame
    void Update(){
        currentTime = System.DateTime.Now;
        textField.text = GetKey.textString;
        dateTextField.text = currentTime.ToString();

        // During recording
        if(GetKey.recording){
            cam.backgroundColor = recordingColour;
        } else {
            cam.backgroundColor = normalColour;
        }

    }

    public void startRecoding(){
        // Set bool
        GetKey.recording = true;
    }

    public void stopRecoding(){
        // Set bool
        GetKey.recording = false;

        // Save
        saveFile();
    }

    public void clearTextField(){
        GetKey.textString = "\n";
    }

    public void endApplication(){
        // Save
        saveFile();

        // Close
        Application.Quit();
    }

    public void saveFile(){
        // Write file to disk
        string fileName = "WKIP_"+ currentTime.ToString("yyyy_MM_dd_HHmmss") + ".txt";
        StreamWriter writer = new StreamWriter(fileName, true);
        writer.Write(GetKey.textString);
        writer.Close();

        // Show in debug.log
        Debug.Log("File saved: " + fileName);

        // Show in FilePathTextField
        string tempString = "Last file saved as: " + fileName;
        filePathTextField.text = tempString;
    }

}


