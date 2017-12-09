using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHotkeyManager : MonoBehaviour {

    public static UnitHotkeyManager instance;

    public int listCount = 7;
    public List<List<GameObject>> unitLists;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(transform.gameObject);
    }

    void Start()
    {
        unitLists = new List<List<GameObject>>();
        for (int i = 0; i < listCount; i++)
        {
            List<GameObject> list = new List<GameObject>();
            unitLists.Add(list);
        }
    }
}
