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
    }
}
