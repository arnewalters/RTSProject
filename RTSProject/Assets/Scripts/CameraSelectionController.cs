using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSelectionController : MonoBehaviour {
    //Selection
    public List<GameObject> selectedUnits;
    public bool isSelecting;
    //Selection Box Settings
    private Texture2D draggingTexture;
    public Color draggingTextureColor;
    //Selection Box Values
    private Vector3 beginDragPoint;
    private Vector3 endDragPoint;
    private Rect selectionRectangle;
    //PlayerUnitHolder
    public PlayerUnitHolder playerUnitHolder;

    void Start()
    {
        playerUnitHolder = this.gameObject.GetComponent<PlayerUnitHolder>();
        //Setup Dragging Texture
        draggingTexture = new Texture2D(1, 1);
        draggingTexture.SetPixel(0, 0, draggingTextureColor);//Upper left pixel color
        draggingTexture.Apply();
    }

    void Update() {
        #region Create Selection Rectangle And Call SelectFromRectangle
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            beginDragPoint = Input.mousePosition;
            isSelecting = true;
        }
        if (isSelecting){
            endDragPoint = Input.mousePosition;
            selectionRectangle = GetScreenRect(beginDragPoint, endDragPoint);

            if (Input.GetKeyUp(KeyCode.Mouse0)){
                ResetSelectionRectangle();
            }
        }
        #endregion
    }

    public void AddUnitsToSelection()
    {
        if (!isSelecting) return;
        
        Camera mainCamera = Camera.main;
        var viewportBounds = GetViewportBounds(mainCamera, beginDragPoint, endDragPoint);

        if (!Input.GetKey(KeyCode.LeftShift))
        {
            selectedUnits.Clear();
        }

        for(int index = 0; index < playerUnitHolder.myUnits.Count; index++)
        {
            GameObject currentUnit = playerUnitHolder.myUnits[index];
            if (viewportBounds.Contains(mainCamera.WorldToViewportPoint(currentUnit.transform.position))){
                selectedUnits.Add(currentUnit);
            }
        }
    }

    private void ResetSelectionRectangle(){
        //Mainly resets the beginDragPointX so a new Selection can be done!
        isSelecting = false;
        selectionRectangle = new Rect(0, 0, 0, 0);
    }

    void OnGUI(){
        if(isSelecting){
            GUI.Box(selectionRectangle, draggingTexture);
        }
    }

    #region Rect Helper
    public static Rect GetScreenRect(Vector3 screenPosition1, Vector3 screenPosition2)
    {
        // Move origin from bottom left to top left
        screenPosition1.y = Screen.height - screenPosition1.y;
        screenPosition2.y = Screen.height - screenPosition2.y;
        // Calculate corners
        var topLeft = Vector3.Min(screenPosition1, screenPosition2);
        var bottomRight = Vector3.Max(screenPosition1, screenPosition2);
        // Create Rect
        return Rect.MinMaxRect(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);
    }

    public static Bounds GetViewportBounds(Camera camera, Vector3 screenPosition1, Vector3 screenPosition2)
    {
        var v1 = Camera.main.ScreenToViewportPoint(screenPosition1);
        var v2 = Camera.main.ScreenToViewportPoint(screenPosition2);
        var min = Vector3.Min(v1, v2);
        var max = Vector3.Max(v1, v2);
        min.z = camera.nearClipPlane;
        max.z = camera.farClipPlane;

        var bounds = new Bounds();
        bounds.SetMinMax(min, max);
        return bounds;
    }
    #endregion
}
