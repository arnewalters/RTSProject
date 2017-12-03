using UnityEngine;

public class Unit : MonoBehaviour
{
    //Unit Variables
    public string unitName = "New Unit";
    public float unitCreateTime;
    public int unitHealth;
    public int unitArmor;
    public int unitDamage;
    public float unitAttackSpeed;
    public float unitWalkSpeed;
    public bool unitCanFly;

    public virtual void doAction()
    {
        //Do an action
        Debug.Log("Performing action of:" + unitName);
    }

    public virtual void doWalkToPoint(Vector3 destination)
    {
        //Do walk
        Debug.Log("Performing walking of:" + unitName);
    }
}
