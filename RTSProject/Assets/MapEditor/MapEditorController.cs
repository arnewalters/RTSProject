using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapEditorController : MonoBehaviour {

    //Map Editor UI
    private TextField widthTextfield;
    private TextField lengthTextfield;

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
        widthTextfield = GameObject.FindGameObjectWithTag("widthTextfield").GetComponent<TextField>();
        lengthTextfield = GameObject.FindGameObjectWithTag("lengthTextfield").GetComponent<TextField>();

        widthTextfield.onTextChanged.AddListener(SetMapWidth);
        lengthTextfield.onTextChanged.AddListener(SetMapLength);
    }

    public void SetMapWidth(string newWidth)
    {
        if (CheckForNumeric(newLength))
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

    public int StringToInt(string toInt)
    {
        //TODO RETURN INTEGER VALUE OF STRING
        return 20;
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
