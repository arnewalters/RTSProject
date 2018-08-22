using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class MapDataManager : MonoBehaviour {

    //UI Elements
    InputField newFileNameInputField;
    MapEditorController mapEditorController;

	void Start ()
    {
        mapEditorController = this.gameObject.GetComponent<MapEditorController>();
        newFileNameInputField = GameObject.FindGameObjectWithTag("newFileNameInputField").GetComponent<InputField>();
	}

    public void SaveMapButtonPressed()
    {
        string data = mapEditorController.GetSaveableData();

        string path = "Assets/Resources/" + newFileNameInputField.text + ".txt";
        StreamWriter writer = new StreamWriter(path);
        writer.Write(data);
    }

    public void LoadMapButtonPressed(string fileName)
    {
        string path = "Assets/Resources/" + fileName + ".txt";
        StreamReader reader = new StreamReader(path);

        while (!reader.EndOfStream)
        {
            string lineData = reader.ReadLine();
            mapEditorController.CreateObjectFromData(lineData);
        }
        
        reader.Close();
    }
}
