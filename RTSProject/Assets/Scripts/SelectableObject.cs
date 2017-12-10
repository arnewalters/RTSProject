using UnityEngine;

public class SelectableObject : MonoBehaviour {

    public bool isSelected;

    public virtual void Select(bool selected)
    {
        //selection stuff
        isSelected = selected;
    }

    public virtual void RightClickAction(Vector3 clickPosition, bool shiftActivated)
    {
        //right click action
        Debug.Log("Right click action for mouse position called");
    }

    public virtual void RightClickAction(Transform target, bool shiftActivated)
    {
        //right click action
        Debug.Log("Right click action for target called");
    }

    public virtual void SKeyAction()
    {
        //s key action
    }
}
