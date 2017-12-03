using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    //Building Variables
    public string buildingName;
    public float buildingCreateTime;
    public int buildingHealth;
    public int buildingArmor;

    public virtual void doAction()
    {
        //Do action
        Debug.Log("Performing action of Building:" + buildingName);
    }
}
