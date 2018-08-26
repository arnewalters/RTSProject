using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MapEditorController : MonoBehaviour {

    #region Colors
    public Color activeTabColor;
    public Color inactiveTabColor;
    #endregion
    #region UI
    private GameObject mapTabGO;
    private GameObject modelsTabGO;
    private Button mapTabButton;
    private Button modelsTabButton;
    private GameObject mapPanel;
    private GameObject modelsPanel;
    private InputField widthTextfield;
    private InputField lengthTextfield;
    private Button generateMapButton;
    #endregion
    #region Map Dimension
    public int mapSizeWidth = 10;
    public int mapSizeLength = 10;
    #endregion
    #region Map tiles,objects
    public GameObject selectedObject;
    public GameObject[,] mapTiles;
    public List<GameObject> mapObjects;
    #endregion
    #region tempObject
    private GameObject previewObject;
    #endregion
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
        generateMapButton.onClick.AddListener(GenerateNewMap);

        this.ChangeTabToMap();
    }

    private void FixedUpdate()
    {
        if (selectedObject && !selectedObject.GetComponent<SaveableObject>().isTile)
        {
            this.PutObject();
        }

    }

    private void PutObject()
    {
        if (!this.previewObject) {
            this.previewObject = GameObject.Instantiate(selectedObject);
            this.previewObject.layer = 2;
        } 
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.GetComponent<SaveableObject>())
            {
                this.previewObject.transform.position = new Vector3(hit.point.x, hit.point.y + 1, hit.point.z);
                this.previewObject.transform.rotation = hit.transform.rotation;
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    this.mapObjects.Add(GameObject.Instantiate(selectedObject, hit.point, hit.transform.rotation));
                }
            }
            else if (this.previewObject)
            {
                Destroy(this.previewObject);
            }
        }
        else if(this.previewObject)
        {
            Destroy(this.previewObject);
        }
    }

    #region Map Generation
    public void SetMapWidth(string newWidth)
    {
        if (MapHelper.CheckForNumeric(newWidth))
        {
            mapSizeWidth = MapHelper.StringToInt(newWidth);
            return;
        }
        OpenWarningWrongInput();
    }

    public void SetMapLength(string newLength)
    {
        if(MapHelper.CheckForNumeric(newLength))
        {
            mapSizeLength = MapHelper.StringToInt(newLength);
            return;
        }
        OpenWarningWrongInput();
    }

    public void GenerateNewMap()
    {
        Debug.Log("Generating Map..");
        if (!selectedObject.GetComponent<SaveableObject>().isTile) {
            Debug.Log("Choose a tile");
            return;
        }

        this.mapTiles = new GameObject[mapSizeWidth, mapSizeLength];
        bool colored = false;
        for (int index = 0; index < mapSizeWidth; index++)
        {
            for (int bindex = 0; bindex < mapSizeLength; bindex++)
            {
                GameObject tile = Instantiate(selectedObject, new Vector3(index * 5, 0, bindex * 5), new Quaternion(0, 0, 0, 0));
                colored = !colored;
                if (colored)
                {
                    tile.GetComponent<MeshRenderer>().material.color = inactiveTabColor;
                }
                this.mapTiles[index, bindex] = tile;
            }
        }
        Debug.Log("Done generating map!");
    }

    public void ClearMap()
    {
        Debug.Log("Clearing Map");
        foreach (GameObject go in mapObjects) {
            Destroy(go);
        }
        foreach (GameObject tile in mapTiles) {
            Destroy(tile);
        }
        Debug.Log("Done clearing map!");
    }
    #endregion

    #region UI Stuff
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

    public void OpenWarningWrongInput()
    {
        //TODO IMPLEMENT
    }
    #endregion

    #region Save and Load Map
    public string GetSaveableData()
    {
        Debug.Log("Getting saveable data");
        string data = "";
        for (int index = 0; index < mapSizeWidth; index++) {
            for (int bindex = 0; bindex < mapSizeLength; bindex++) {
                data += mapTiles[index, bindex].GetComponent<SaveableObject>().ToSaveableDataLine(index, bindex) + "\r\n";
            }
        }
        foreach (GameObject GO in this.mapObjects)
        {
            data += GO.GetComponent<SaveableObject>().ToSaveableDataLine(0,0) + "\r\n";
        }
        Debug.Log("Returning saveable data");
        return data;
    }

    public void CreateMapFromData(string[] data)
    {
        this.ClearMap();
        foreach (string dataLine in data) {
            List<string> splittedData = dataLine.Split('!').ToList();
            if (splittedData[0].Equals("true"))
            {
                splittedData.RemoveAt(0);
                GameObject tile = new SaveableObject().CreateTileFromData(splittedData);
            } else {
                splittedData.RemoveAt(0);
                GameObject obj = new SaveableObject().CreateObjectFromData(splittedData);
            }        
        }
    }
    #endregion
}
