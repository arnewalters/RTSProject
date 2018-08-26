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
        string path = "Assets/Maps/" + newFileNameInputField.text + "map" + ".txt";
        StreamWriter writer = new StreamWriter(path);
        writer.Write(data);
        Debug.Log("Saved Map with string length = " + data.Length);
    }

    public void LoadMapButtonPressed(string fileName)
    {
        string path = "Assets/Resources/" + fileName + ".txt";
        StreamReader reader = new StreamReader(path);
        List<string> dataList = new List<string>();
        while (!reader.EndOfStream)
        {
            dataList.Add(reader.ReadLine());
        }
        mapEditorController.CreateMapFromData(dataList.ToArray());
        reader.Close();
    }
}
