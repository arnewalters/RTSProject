using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapEditorController : MonoBehaviour {

    //Map Editor UI
    private InputField widthTextfield;
    private InputField lengthTextfield;

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

        widthTextfield.contentType = InputField.ContentType.IntegerNumber;
        lengthTextfield.contentType = InputField.ContentType.IntegerNumber;

        widthTextfield.onValueChanged.AddListener(SetMapWidth);
        lengthTextfield.onValueChanged.AddListener(SetMapLength);
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
        for(int index = 0; index < mapSizeWidth; index++)
        {
            for(int bindex = 0; bindex < mapSizeLength; bindex++)
            {

            }
        }
    }
}
