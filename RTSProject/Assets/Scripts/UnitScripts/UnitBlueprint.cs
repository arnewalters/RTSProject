using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(NavMeshAgent))]
public class UnitBlueprint : SelectableObject
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
    public NavMeshAgent navAgent;
    public List<Vector3> destinations;
    public Vector3 currentDestination;
    private bool isWalking;
    private GameObject targetObject;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        navAgent.speed = unitWalkSpeed;
        
        if(CheckWalking())
        {
            double distanceToNextDestination = Vector3.Distance(transform.position, currentDestination);
            if (distanceToNextDestination < 1)
            {
                RemoveDestinationFromQueue(currentDestination);
                if (CheckWalking())
                {
                    currentDestination = destinations[0];
                    navAgent.destination = currentDestination;
                }   
            }
        }
    }

    public virtual void DoAction()
    {
        //Do an action
        Debug.Log("Performing action of:" + unitName);
    }

    public override void Select(bool selected)
    {
        //SELECTION STUFF
        base.Select(selected);
        GetComponentInChildren<Projector>().enabled = selected;
        if (GetComponentInChildren<Projector>() == null) Debug.Log("Projector not found");
    }

    public override void RightClickAction(Vector3 clickPosition, bool shiftActivated)
    {
        //DO RIGHT CLICK ACTION
        navAgent.isStopped = false;
        base.RightClickAction(clickPosition, shiftActivated);
        if(!shiftActivated)
        {
            destinations.Clear();
            destinations.Add(clickPosition);
            currentDestination = destinations[0];
        }
        else
        {
            destinations.Add(clickPosition);
            currentDestination = destinations[0];
        }
        navAgent.destination = currentDestination;
    }

    public override void RightClickAction(Transform target, bool shiftActivated)
    {
        //DO RIGHT CLICK ACTION
        navAgent.isStopped = false;
        base.RightClickAction(target, shiftActivated);
        if (!shiftActivated)
        {
            destinations.Clear();
            destinations.Add(target.position);
            currentDestination = destinations[0];
        }
        else
        {
            destinations.Add(target.position);
            currentDestination = destinations[0];
        }
        this.targetObject = target.gameObject;
        navAgent.destination = currentDestination;
    }

    public override void SKeyAction()
    {
        base.SKeyAction();
        if(CheckWalking())
        {
            navAgent.isStopped = true;
            destinations.Clear();
        }
    }

    private bool CheckWalking()
    {
        if (destinations.Count > 0) return true;
        return false;
    }

    private void RemoveDestinationFromQueue(Vector3 toDelete)
    {
        destinations.Remove(toDelete);
    }
}
