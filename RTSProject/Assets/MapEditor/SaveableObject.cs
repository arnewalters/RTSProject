using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveableObject : MonoBehaviour {

    public string objectID;
    public bool isTile;

    public string ToSaveableDataLine(int arrayPosX, int arrayPosY, int arrayPosZ)
    {
        string saveableData;
        saveableData = isTile ? "true" : "false" + "!" 
                     + objectID + "!" 
                     + this.transform.position.x + "!" 
                     + this.transform.position.y + "!" 
                     + this.transform.rotation.x + "!"
                     + this.transform.rotation.y + "!"
                     + this.transform.rotation.z + "!";
        if (isTile)
        {
            saveableData += arrayPosX + "!" + arrayPosY + "!" + arrayPosZ;
        }
        return saveableData;
    }
}
