using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveableObject : MonoBehaviour {

    public Sprite icon;
    public string prefabName;
    public int objectID;
    public bool isTile;
    public GameObject prefab;

    public void SelectThisItem()
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MapEditorController>().selectedObject = this.prefab;
    }

    public string ToSaveableDataLine(int arrayPosX, int arrayPosY)
    {
        string saveableData;
        saveableData = isTile ? "true" : "false" + "!" 
                     + objectID + "!" 
                     + this.transform.position.x + "!" 
                     + this.transform.position.y + "!" 
                     + this.transform.position.z + "!"
                     + this.transform.rotation.x + "!"
                     + this.transform.rotation.y + "!"
                     + this.transform.rotation.z;
        if (isTile)
        {
            saveableData += "!" + arrayPosX + "!" + arrayPosY;
        }
        return saveableData;
    }

    public GameObject CreateTileFromData(List<string> data)
    {
        SaveableObjects objects = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SaveableObjects>();
        GameObject newTile =
            GameObject.Instantiate(objects.tiles[MapHelper.StringToInt(data[0])].prefab,
            new Vector3(MapHelper.StringToFloat(data[1]), MapHelper.StringToFloat(data[2]), MapHelper.StringToFloat(data[3])),
            new Quaternion(MapHelper.StringToFloat(data[4]), MapHelper.StringToFloat(data[5]), MapHelper.StringToFloat(data[6]), 0));
        return newTile;
    }

    public GameObject CreateObjectFromData(List<string> data)
    {
        SaveableObjects objects = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SaveableObjects>();
        GameObject newObject =
            GameObject.Instantiate(objects.objects[MapHelper.StringToInt(data[0])].prefab,
            new Vector3(MapHelper.StringToFloat(data[1]), MapHelper.StringToFloat(data[2]), MapHelper.StringToFloat(data[3])),
            new Quaternion(MapHelper.StringToFloat(data[4]), MapHelper.StringToFloat(data[5]), MapHelper.StringToFloat(data[6]), 0));
        return newObject;
    }
}
