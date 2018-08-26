using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectableObjectsUI : MonoBehaviour {

    private GameObject listContent;
    public GameObject oneItemPanel;

	void Start () {
        listContent = this.transform.gameObject;
        SaveableObjects objects = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SaveableObjects>();
        foreach (SaveableObject so in objects.tiles)
        {
            this.NewPanelForSaveableObject(so);
        }
        foreach (SaveableObject so in objects.objects)
        {
            this.NewPanelForSaveableObject(so);
        }
	}

    private void NewPanelForSaveableObject(SaveableObject so)
    {
        GameObject newPanel = Instantiate(oneItemPanel);
        newPanel.GetComponent<SaveableObject>().icon = so.icon;
        newPanel.GetComponent<SaveableObject>().prefab = so.prefab;
        newPanel.GetComponent<SaveableObject>().prefabName = so.prefabName;
        newPanel.GetComponent<SaveableObject>().isTile = so.isTile;
        newPanel.GetComponent<SaveableObject>().objectID = so.objectID;
        foreach (Image img in newPanel.GetComponentsInChildren<Image>()) {
            if (img.transform.gameObject != this.gameObject) {
                img.sprite = so.icon;
            }
        }
        newPanel.GetComponentInChildren<Text>().text = so.prefabName;
        newPanel.GetComponent<Button>().onClick.AddListener(newPanel.GetComponent<SaveableObject>().SelectThisItem);
        newPanel.transform.SetParent(listContent.transform);
    }
}
