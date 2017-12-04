using UnityEngine;

public class CameraController : MonoBehaviour {

    //SETTINGS
    public bool dragViewByKeys = true;
    public bool dragViewByMouse = true;

    //BOUNDARYS
    private Vector2 dragLimit = new Vector2(50, 50); //The y-Value is used as boundarie for the new Position z-Value!
    private float minScrollHeight = 10f;
    private float maxScrollHeight = 40f;

    //MOVEMENTS
    public float dragSpeed = 10f;
    public float scrollBorderWidth = 10f;

    //SCROLLING
    public float scrollSpeed = 10f;

    void Update () {
        //The new Position gets affected by every input and is set 
        //After the Update is done so every frame.
        Vector3 newPosition = this.transform.position;
        //Modification for X and Z movement -> left,right and forward,backward
        #region Movement
        if ((Input.GetKey(KeyManager.instance.moveCameraUpKey) && dragViewByKeys) || (Input.mousePosition.y >= Screen.height - scrollBorderWidth && dragViewByMouse)) {
            // += Z -> Forward
            newPosition.z += dragSpeed * Time.deltaTime;
        }
        if ((Input.GetKey(KeyManager.instance.moveCameraDownKey) && dragViewByKeys) || (Input.mousePosition.y <= scrollBorderWidth && dragViewByMouse))
        {
            // -= Z -> Backward
            newPosition.z -= dragSpeed * Time.deltaTime;
        }
        if ((Input.GetKey(KeyManager.instance.moveCameraLeftKey) && dragViewByKeys) || (Input.mousePosition.x <= scrollBorderWidth && dragViewByMouse))
        {
            // -= X -> Left
            newPosition.x -= dragSpeed * Time.deltaTime;
        }
        if ((Input.GetKey(KeyManager.instance.moveCameraRightKey) && dragViewByKeys) || (Input.mousePosition.x >= Screen.width - scrollBorderWidth && dragViewByMouse))
        {
            // += X -> Right
            newPosition.x += dragSpeed * Time.deltaTime;
        }

        newPosition.z = Mathf.Clamp(newPosition.z, -dragLimit.y, dragLimit.y);
        newPosition.x = Mathf.Clamp(newPosition.x, -dragLimit.x, dragLimit.x);
        #endregion
        //Modification for Y movement -> up and down
        #region Scrolling
        if (Input.GetAxis("Mouse ScrollWheel") < 0) {
            //Scroll down
            newPosition.y -= scrollSpeed * Time.deltaTime; 
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0) {
            newPosition.y += scrollSpeed * Time.deltaTime;
        }

        newPosition.y = Mathf.Clamp(newPosition.y, minScrollHeight, maxScrollHeight);
        #endregion
        //Applying the new changes
        this.transform.position = newPosition;
    }
}
