﻿using System.Collections;
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
        #region Force Right click action
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            Debug.Log("Right Click should be called once");
            if(!(selectedUnits.Count <= 0))
            {
                bool shift = Input.GetKey(KeyCode.LeftShift);
                Debug.Log("Shift was pressed ?" + shift);
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray, out hit))
                { 
                    Transform objectHit = hit.transform;
                    if (objectHit.tag == "Ground")
                    {
                        foreach (GameObject gameObject in selectedUnits)
                        {
                            gameObject.GetComponent<SelectableObject>().RightClickAction(hit.point, shift);
                        }
                    }
                    
                }
            }
        }
        #endregion
        #region Create Selection Rectangle And Call SelectFromRectangle
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            beginDragPoint = Input.mousePosition;
            isSelecting = true;
        }
        if (isSelecting){
            endDragPoint = Input.mousePosition;
            selectionRectangle = GetScreenRect(beginDragPoint, endDragPoint);
            AddUnitsToSelection();
            if (Input.GetKeyUp(KeyCode.Mouse0)){
                ResetSelectionRectangle();
            }
        }
        #endregion
    }

    #region Select and Deselect
    public void AddUnitsToSelection()
    {
        if (!isSelecting) return;
        
        Camera mainCamera = Camera.main;
        var viewportBounds = GetViewportBounds(mainCamera, beginDragPoint, endDragPoint);

        if (!Input.GetKey(KeyCode.LeftShift))
        {
            foreach(GameObject unit in selectedUnits)
            {
                //Selected Units getting deselected
                unit.GetComponent<SelectableObject>().Select(false);
            }
            //Clearing selected Units
            selectedUnits.Clear();
        }

        for(int index = 0; index < playerUnitHolder.mySelectables.Count; index++)
        {
            GameObject currentUnit = playerUnitHolder.mySelectables[index];
            SelectableObject unitScript = currentUnit.GetComponent<SelectableObject>();
            if (unitScript != null)
            { 
                if (viewportBounds.Contains(mainCamera.WorldToViewportPoint(currentUnit.transform.position)))
                {
                    //Unit is actually getting selected
                    selectedUnits.Add(currentUnit);
                    currentUnit.GetComponent<SelectableObject>().Select(true);
                }
            }
        }
    }

    private void ResetSelectionRectangle(){
        //Mainly resets the beginDragPointX so a new Selection can be done!
        isSelecting = false;
        selectionRectangle = new Rect(0, 0, 0, 0);
    }
    #endregion

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

    void OnGUI()
    {
        if (isSelecting)
        {
            GUI.Box(selectionRectangle, draggingTexture);
        }
    }
}
