using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHotkeyManager : MonoBehaviour {

    public static UnitHotkeyManager instance;

    public int listCount = 7;
    public List<List<GameObject>> unitLists;

    private void Awake()
    {
        unitLists = new List<List<GameObject>>();
        for(int i = 0; i < listCount; i++)
        {
            List<GameObject> list = new List<GameObject>();
            unitLists.Add(list);
        }
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
