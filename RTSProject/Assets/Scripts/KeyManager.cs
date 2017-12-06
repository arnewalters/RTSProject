using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour {

    public static KeyManager instance = null;

    //CAMERA MOVEMENT KEYS
    public KeyCode moveCameraUpKey = KeyCode.UpArrow;
    public KeyCode moveCameraDownKey = KeyCode.DownArrow;
    public KeyCode moveCameraLeftKey = KeyCode.LeftArrow;
    public KeyCode moveCameraRightKey = KeyCode.RightArrow;

    //NORMAL KEYS
    public KeyCode leftClickKey = KeyCode.Mouse0;
    public KeyCode rightClickKey = KeyCode.Mouse1;
    public KeyCode stopActionKey = KeyCode.S;
    public KeyCode attackActionKey = KeyCode.A;
    public KeyCode shiftKey = KeyCode.LeftShift;

    //BUILD HOTKEY
    public KeyCode basicBuildKey = KeyCode.B;

    //BUILDINGS HOTKEYS
    public KeyCode buildBaseKey = KeyCode.B;
    public KeyCode buildBarrackKey = KeyCode.S;
    public KeyCode buildSupplyHouse = KeyCode.E;

    //UNIT STORE HOTKEYS
    public KeyCode setUnitHotkey = KeyCode.LeftControl;
    public List<KeyCode> unitListHotkeys;

    void Awake()
    {
        unitListHotkeys = new List<KeyCode>();
        unitListHotkeys.Add(KeyCode.Alpha1);
        unitListHotkeys.Add(KeyCode.Alpha2);
        unitListHotkeys.Add(KeyCode.Alpha3);
        unitListHotkeys.Add(KeyCode.Alpha4);
        unitListHotkeys.Add(KeyCode.Alpha5);
        unitListHotkeys.Add(KeyCode.Alpha6);
        unitListHotkeys.Add(KeyCode.Alpha7);

        #region Validate KeyManager instance
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(transform.gameObject);
        #endregion 
    }
}
