using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapEditorController : MonoBehaviour {

    //Public UI Colors
    public Color activeTabColor;
    public Color inactiveTabColor;

    //Map Editor UI
    private GameObject mapTabGO;
    private GameObject modelsTabGO;
    private Button mapTabButton;
    private Button modelsTabButton;
    private GameObject mapPanel;
    private GameObject modelsPanel;
    private InputField widthTextfield;
    private InputField lengthTextfield;
    private Button generateMapButton;

    //Map Settings
    public int mapSizeWidth = 10;
    public int mapSizeLength = 10;
    public GameObject mapGridTile;

    //Prefabs to choose from
    public List<GameObject> tiles;
    public List<GameObject> rampTiles;
    public List<GameObject> prefabs;

    //Map
    public List<GameObject> mapTiles;
    public List<GameObject> mapObjects;

    void Start()
    {
        //Get UI Elements
        mapTabGO = GameObject.FindGameObjectWithTag("mapTabButton");
        modelsTabGO = GameObject.FindGameObjectWithTag("modelsTabButton");
        mapTabButton = mapTabGO.GetComponent<Button>();
        modelsTabButton = modelsTabGO.GetComponent<Button>();
        mapPanel = GameObject.FindGameObjectWithTag("mapPanel");
        modelsPanel = GameObject.FindGameObjectWithTag("modelsPanel");
        widthTextfield = GameObject.FindGameObjectWithTag("widthTextfield").GetComponent<InputField>();
        lengthTextfield = GameObject.FindGameObjectWithTag("lengthTextfield").GetComponent<InputField>();
        generateMapButton = GameObject.FindGameObjectWithTag("generateMapButton").GetComponent<Button>();

        widthTextfield.contentType = InputField.ContentType.IntegerNumber;
        lengthTextfield.contentType = InputField.ContentType.IntegerNumber;

        mapTabButton.onClick.AddListener(ChangeTabToMap);
        modelsTabButton.onClick.AddListener(ChangeTabToModels);
        widthTextfield.onValueChanged.AddListener(SetMapWidth);
        lengthTextfield.onValueChanged.AddListener(SetMapLength);
        generateMapButton.onClick.AddListener(CreateNewMap);

        this.ChangeTabToMap();
    }

    public void SetMapWidth(string newWidth)
    {
        if (CheckForNumeric(newWidth))
        {
            mapSizeWidth = StringToInt(newWidth);
            return;
        }
        OpenWarningWrongInput();
    }

    public void SetMapLength(string newLength)
    {
        if(CheckForNumeric(newLength))
        {
            mapSizeLength = StringToInt(newLength);
            return;
        }
        OpenWarningWrongInput();
    }

    public void ChangeTabToMap()
    {
        mapPanel.SetActive(true);
        modelsPanel.SetActive(false);
        mapTabGO.GetComponent<Image>().color = activeTabColor;
        modelsTabGO.GetComponent<Image>().color = inactiveTabColor;
    }

    public void ChangeTabToModels()
    {
        mapPanel.SetActive(false);
        modelsPanel.SetActive(true);
        mapTabGO.GetComponent<Image>().color = inactiveTabColor;
        modelsTabGO.GetComponent<Image>().color = activeTabColor;
    }

    public bool CheckForNumeric(string stringToCheck)
    {
        int number;
        return int.TryParse(stringToCheck, out number);
    }

    public int StringToInt(string toInt)
    {
        int number;
        int.TryParse(toInt, out number);
        return number;
    }

    public void OpenWarningWrongInput()
    {
        //TODO IMPLEMENT
    }

    public void CreateNewMap()
    {
        //TODO ADD TO PARENT TO CLEAR ROWS IF LESS THAN BEFORE
        Debug.Log("Generating Map..");
        bool colored = false;
        for(int index = 0; index < mapSizeWidth; index++)
        {
            for(int bindex = 0; bindex < mapSizeLength; bindex++)
            {
                GameObject tile = Instantiate(mapGridTile, new Vector3(index * 5, 0, bindex * 5), new Quaternion(0, 0, 0, 0));
                colored = !colored;
                if (colored) {
                    tile.GetComponent<MeshRenderer>().material.color = inactiveTabColor;
                }
            }
        }
        Debug.Log("Done generating map!");
    }

    public string GetSaveableData()
    {
        string data = "";
        foreach(GameObject GO in this.mapTiles)
        {
            data += GO.GetComponent<SaveableObject>().ToSaveableDataLine(0,0,0) + "\r\n";
        }
        foreach (GameObject GO in this.mapObjects)
        {
            data += GO.GetComponent<SaveableObject>().ToSaveableDataLine(0,0,0) + "\r\n";
        }
        return data;
    }

    public void CreateObjectFromData(string data)
    {

    }
}
