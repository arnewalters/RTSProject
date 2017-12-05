using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapEditorController : MonoBehaviour {

    //Map Editor UI
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

    void Start()
    {
        widthTextfield = GameObject.FindGameObjectWithTag("widthTextfield").GetComponent<InputField>();
        lengthTextfield = GameObject.FindGameObjectWithTag("lengthTextfield").GetComponent<InputField>();
        generateMapButton = GameObject.FindGameObjectWithTag("generateMapButton").GetComponent<Button>();

        widthTextfield.contentType = InputField.ContentType.IntegerNumber;
        lengthTextfield.contentType = InputField.ContentType.IntegerNumber;

        widthTextfield.onValueChanged.AddListener(SetMapWidth);
        lengthTextfield.onValueChanged.AddListener(SetMapLength);
        generateMapButton.onClick.AddListener(CreateNewMap);
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
        Debug.Log("Generating Map..");
        for(int index = 0; index < mapSizeWidth; index++)
        {
            for(int bindex = 0; bindex < mapSizeLength; bindex++)
            {
                Instantiate(mapGridTile, new Vector3(index * 5, 0, bindex * 5), new Quaternion(0, 0, 0, 0));
            }
        }
        Debug.Log("Done generating map!");
    }
}
